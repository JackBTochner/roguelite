using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof (CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        
        [SerializeField] private InputReader inputReader = default;

        [SerializeField]
        private float moveSpeed = 2f;
        [SerializeField]
        private float acceleration = 10f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        [SerializeField]
        private float playerEyeHeight = 0.75f;
        [SerializeField]
        private float gravity = -15.0f;

        [SerializeField]
        private float gamepadDeadzone = 0.01f;

        [SerializeField]
        private float gamepadRotateSmoothing = 500f;

        [SerializeField]
        private bool isGamepad;
                
        [SerializeField]
        private TransformAnchor mainCamera = default;

        [SerializeField] private Transform aimTransform;
        [SerializeField] private Transform gfxTransform;
        [Tooltip("Angular speed in degrees per sec.")]
        [SerializeField] private float gfxRotateSpeed = 30;
        [SerializeField] private Animator gfxAnimator;
        Quaternion lookAt;

        private CharacterController controller;

        private Vector3 velocity;

        public float currentSpeed;
        public float _speed;
        private float _animationBlend;

        private Controls controls;

        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        public bool allowMovement = true;
        public bool allowRotation = true;

        public float dashSpeed = 15;
        public float dashTime = 0.25f;
        public float dashCoolTime = 1;
        public bool isDashing = false;
        public bool canDash = true;
        public Vector3 lastHorizontalVelocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }
        private void OnEnable()
        {
            inputReader.OnJumpPerformed += jump;
        }
        private void OnDisable()
        {
            inputReader.OnJumpPerformed -= jump;
        }
        public void Initialise()
        {
            // manager.playerInput.controlsChangedEvent.AddListener(OnDeviceChange);
            
        }
        private void jump()
        {
            // Test the jump key is working.
            //Debug.Log("Test: Jump Key working");

            if (canDash)
            {
                StartCoroutine(BeginDash());
            }
        }

        private IEnumerator BeginDash()
        {
            //invulnerable
            //Code base description for invulnerable:
            /*
             * so in this whole code, we will set invulnerable as true when we dash, 
             * then if the player has been trigger the takedamage function, 
             * this function will check the invulnerable status first, 
             * if is true take no damage, when the dash has gone for 0.2 second the 
             * "yield return new WaitForSeconds(0.2f);" will run so now after 0.2 second 
             * set invulnerable to false.
             */
            PlayerCharacter playerCharacter = GetComponent<PlayerCharacter>();
            playerCharacter.invulnerable = true;
            //
            allowMovement = false;
            allowRotation = false;
            isDashing = true;
            lastHorizontalVelocity = Vector3.ProjectOnPlane(controller.velocity, Vector3.up);
            float startTime = Time.time;

            while(Time.time < startTime + dashTime)
            {
                controller.Move(lastHorizontalVelocity.normalized * dashSpeed * Time.deltaTime);
                yield return null;
            }
            
            isDashing =false;
            allowMovement = true;
            allowRotation = true;
            lastHorizontalVelocity = Vector3.zero;
            StartCoroutine(DashCooldown());

            //invulnerable
            yield return new WaitForSeconds(0.2f);
            playerCharacter.invulnerable = false;
            //
        }

        IEnumerator DashCooldown()
        {
            canDash = false;
            yield return new WaitForSeconds(dashCoolTime);
            canDash = true;
        }
        private void Update()
        {
            ApplyGravity();
            if(allowMovement)
                HandleMovement();
            if(aimTransform && allowRotation)
                HandleRotation();
            currentSpeed = controller.velocity.magnitude;

        }

        private void ApplyGravity()
        {
            if (controller.isGrounded)
            {
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }
            }
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }
        }

        private void HandleMovement()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = moveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (inputReader.MoveComposite == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = inputReader.MoveComposite.magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * acceleration);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * acceleration);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(inputReader.MoveComposite.x, 0.0f, inputReader.MoveComposite.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (inputReader.MoveComposite != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  mainCamera.Value.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            if(gfxAnimator)
                gfxAnimator.SetFloat("Speed", (currentSpeed > 0) ? currentSpeed / moveSpeed : 0);
            //controller.Move(move * Time.deltaTime * moveSpeed);
        }

        private void HandleRotation()
        {
            
            if (isGamepad)
                GamepadLook();
            else
                MouseLook();
        }

        private void GamepadLook()
        {
            if ( Mathf.Abs(inputReader.Look.x) > gamepadDeadzone || Mathf.Abs(inputReader.Look.y) > gamepadDeadzone )
            {
                Vector3 playerDirection =
                    Vector3.right * inputReader.Look.x +
                    Vector3.forward * inputReader.Look.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRot = Quaternion.LookRotation(playerDirection, Vector3.up);
                    aimTransform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }

        private void MouseLook()
        {
            Ray ray = Camera.main.ScreenPointToRay(inputReader.MousePosition);
            Plane xzPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y + playerEyeHeight, 0));
            float rayDistance;

            if(xzPlane.Raycast(ray, out rayDistance))
            {
                Vector3 intersect = ray.GetPoint(rayDistance);
                Vector3 point = new(intersect.x, transform.position.y, intersect.z);
                aimTransform.LookAt(point);
            }
        }

        public void OnDeviceChange(PlayerInput pi)
        {
            isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
        }
    }
}
