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
        private float playerSpeed = 5f;
        [SerializeField]
        private float playerEyeHeight = 0.75f;

        [SerializeField]
        private float gamepadDeadzone = 0.01f;

        [SerializeField]
        private float gamepadRotateSmoothing = 500f;

        [SerializeField]
        private bool isGamepad;
                
        [SerializeField] 
        private TransformAnchor mainCamera = default;

        private CharacterController controller;

        private Vector3 velocity;

        public float currentSpeed;

        private Controls controls;

        public bool allowMovement = true;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            inputReader.OnJumpPerformed += jump;
        }

        public void Initialise()
        {
            // manager.playerInput.controlsChangedEvent.AddListener(OnDeviceChange);
            
        }
        private void jump()
        {
            Debug.Log("Jump!");
        }

        private void Update()
        {
            ApplyGravity();
            if(allowMovement)
            HandleMovement();
            HandleRotation();
                currentSpeed = controller.velocity.magnitude;
        }

        private void ApplyGravity()
        {
            if (velocity.y > Physics.gravity.y)
            {
                velocity.y += Physics.gravity.y * Time.deltaTime;
            }
            controller.Move(velocity * Time.deltaTime);
        }

        private void HandleMovement()
        {
            Vector3 move;
            if (mainCamera.isSet)
            {
                Vector3 playerForward =
                    Vector3.ProjectOnPlane(mainCamera.Value.forward, Vector3.up);
                Vector3 playerRight =
                    Vector3.ProjectOnPlane(mainCamera.Value.right, Vector3.up);

                move =
                    playerForward.normalized * inputReader.MoveComposite.y +
                    playerRight.normalized * inputReader.MoveComposite.x;

            } else
            { 
                //No CameraManager exists in the scene, so the input is just used absolute in world-space
                Debug.LogWarning("No gameplay camera in the scene. Movement orientation will not be correct.");
                move = new Vector3(inputReader.MoveComposite.x, 0f, inputReader.MoveComposite.y);
            }
            controller.Move(move * Time.deltaTime * playerSpeed);
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
            if (
                Mathf.Abs(inputReader.Look.x) > gamepadDeadzone ||
                Mathf.Abs(inputReader.Look.y) > gamepadDeadzone
            )
            {
                Vector3 playerDirection =
                    Vector3.right * inputReader.Look.x +
                    Vector3.forward * inputReader.Look.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRot =
                        Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation =
                        Quaternion
                            .RotateTowards(transform.rotation,
                            newRot,
                            gamepadRotateSmoothing * Time.deltaTime);
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
                transform.LookAt(point);
            }
        }

        public void OnDeviceChange(PlayerInput pi)
        {
            isGamepad =
                pi.currentControlScheme.Equals("Gamepad") ? true : false;
        }
    }
}
