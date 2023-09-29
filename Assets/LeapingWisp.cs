//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Player;
//using Pathfinding; // Using "Pathfinding" for AIPath

//public class LeapingWisp : MonoBehaviour
//{
//    // Player
//    public TransformAnchor playerTransformAnchor = default;
//    // Jump Height
//    public float jumpHeight = 20.0f;
//    // In the air time, without jumping and dropping down.
//    public float airTime = 4.0f;
//    // Follow time
//    public float followTime = 3.0f;
//    // AI path, for getting values.
//    private AIPath aiPath;
//    // Jump time
//    public float jumpTime = 1.0f;
//    // Drop time
//    public float dropTime = 1.0f;
//    // Ground time
//    public float groundTime = 2.0f;
//    // Damage deal to player
//    public int damage = 50;
//    // Player controller
//    private CharacterController playerController;




//    public float pushForce = 30.0f;
//    // Start is called before the first frame update
//    void Start()
//    {

//        StartCoroutine(Jump());
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Check the player is spawn or not.
//        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
//        {
//            if (playerController == null)
//            {
//                playerController = playerTransformAnchor.Value.GetComponent<CharacterController>();
//            }
//            Vector3 direction = playerTransformAnchor.Value.position - transform.position;
//            direction.y = 0; 
//            transform.rotation = Quaternion.LookRotation(direction);
//        }
//    }

//    IEnumerator Jump()
//    {        
//        aiPath = GetComponent<AIPath>();
//        float orginalmaxSpeed = aiPath.maxSpeed;
//        aiPath.maxSpeed = 0.0f;
//        yield return new WaitForSeconds(1.0f);

//        float startY = transform.position.y;
//        float jumpY = startY + jumpHeight;

//        Rigidbody rb = GetComponent<Rigidbody>();

//        while (true)
//        {
//            rb.useGravity = false;
//            float timer = 0;
//            while (timer < jumpTime)
//            {
//                aiPath.maxSpeed = orginalmaxSpeed;
//                float t = timer / jumpTime;
//                float LeapingY = Mathf.Lerp(startY, jumpY, t);
//                transform.position = new Vector3(transform.position.x, LeapingY, transform.position.z);
//                timer += Time.deltaTime;
//                yield return null;
//            }

//            transform.position = new Vector3(transform.position.x, jumpY, transform.position.z);

//            float airTimer = 0;
//            while(airTimer < airTime)
//            {
//                if(airTimer >= followTime && airTimer <= 4.0f)
//                {
//                    aiPath.maxSpeed = 0.0f;
//                }
//                else if (airTimer < followTime)
//                {
//                    aiPath.maxSpeed = orginalmaxSpeed;
//                }
//                airTimer += Time.deltaTime;
//                yield return null;
//            }

//            timer = 0;
//            while (timer < dropTime)
//            {
//                rb.isKinematic = true;
//                aiPath.maxSpeed = 0.0f;
//                float t = timer / dropTime;
//                float LeapingY = Mathf.Lerp(jumpY, startY, t);
//                transform.position = new Vector3(transform.position.x, LeapingY, transform.position.z);
//                timer += Time.deltaTime;
//                yield return null;
//            }
//            rb.isKinematic = false;
//            transform.position = new Vector3(transform.position.x, startY, transform.position.z);

//            yield return new WaitForSeconds(groundTime);
//        }
//    }
//    void OnCollisionEnter(Collision collision)
//    {
//        Debug.Log("Collision detected with: " + collision.gameObject.name);
//        Rigidbody otherRb = collision.collider.GetComponent<Rigidbody>();
//        if (collision.gameObject.tag == "Player")
//        {
//            PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
//            DisablePlayerMovement();
//            Invoke("EnablePlayerMovement", 2.0f);
//            playerCharacter.TakeDamage(damage);
//        }
//        if (otherRb != null)
//        {
//            Vector3 direction = collision.transform.position - transform.position;
//            direction.y = 2f;  // no vertical push
//            otherRb.AddForce(direction.normalized * pushForce, ForceMode.Impulse);

//        }
//    }

//    private void DisablePlayerMovement() // Turn off the player controller.
//    {
//        playerController.GetComponent<PlayerMovement>().allowMovement = false;
//    }

//    private void EnablePlayerMovement() // Turn on the player controller.
//    {
//        playerController.enabled = true;
//        playerController.GetComponent<PlayerMovement>().allowMovement = true;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player; //Player
using Pathfinding; // Using "Pathfinding" for AIPath
using DG.Tweening; //Animation engine for Unity

public class LeapingWisp : MonoBehaviour
{
    public GameObject leapingWisp;
    public float jumpTime;
    public float jumpHeight;
    public TransformAnchor playerTransformAnchor = default;
    private AIPath aiPath;
    public float orginalmaxSpeed;
    private Quaternion originalRotation;
    private Tween currentDropTween;
    public bool  isDroping = false;
    private CharacterController playerController;
    public int damage = 50;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = 0.0f;
        StartCoroutine(delayJump());

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (playerController == null)
            {
                playerController = playerTransformAnchor.Value.GetComponent<CharacterController>();
            }
            Vector3 lookDirection = playerTransformAnchor.Value.position - transform.position;
            lookDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = targetRotation;
        }
    }

    private IEnumerator delayJump()
    {

        // Wait for 0.7 second
        yield return new WaitForSeconds(2.0f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        while (true)
        {
            aiPath = GetComponent<AIPath>();
            aiPath.maxSpeed = orginalmaxSpeed;
            Jump();
            yield return new WaitForSeconds(3f);
            aiPath.maxSpeed = 0.0f;
            yield return new WaitForSeconds(1f);
            Drop();
            //isDroping = false;
            yield return new WaitForSeconds(2f);
        }
    }

    private void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = false;
        Transform leapingWtransform = leapingWisp.transform;
        leapingWtransform.DOMove(leapingWtransform.position + new Vector3(0, jumpHeight, 0), jumpTime).SetEase(Ease.Linear);
    }

    private void Drop()
    {
        isDroping = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        Transform leapingWtransform = leapingWisp.transform;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        currentDropTween = leapingWtransform.DOMove(leapingWtransform.position - new Vector3(0, jumpHeight + 5, 0), jumpTime).SetEase(Ease.Linear);
    }



    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.tag == "Player" && isDroping == true)
        {
            if (currentDropTween != null)
            {
                currentDropTween.Kill();
            }

            PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
            Disableplayermovement();
            Invoke("EnablePlayerMovement", 2.0f);
            playerCharacter.TakeDamage(damage);

            // Force Code
            //Rigidbody hitRb = collision.gameObject.GetComponent<Rigidbody>();
            //if (hitRb != null)
            //{
            //    Vector3 pushDirection = collision.transform.position - transform.position;
            //    pushDirection.y = 0;
            //    pushDirection.Normalize();

            //    float pushForce = 500f;
            //    hitRb.AddForce(pushDirection * pushForce);
            //}
            //
        }         
        if (collision.gameObject.CompareTag("Ground") )
        {
            if (currentDropTween != null)
            {
                currentDropTween.Kill();
            }
        }
        if (collision.gameObject.CompareTag("Enemy") && isDroping == true)
        {
            //// Force Code
            //Rigidbody hitRb = collision.gameObject.GetComponent<Rigidbody>();
            //if (hitRb != null)
            //{
            //    Vector3 pushDirection = collision.transform.position - transform.position;
            //    pushDirection.y = 0;
            //    pushDirection.Normalize();

            //    float pushForce = 500f;
            //    hitRb.AddForce(pushDirection * pushForce);
            //}
            ////
        }
    }
    private void Disableplayermovement() // turn off the player controller.
    {
        playerController.GetComponent<PlayerMovement>().allowMovement = false;
    }

    private void Enableplayermovement() // turn on the player controller.
    {
        playerController.enabled = true;
        playerController.GetComponent<PlayerMovement>().allowMovement = true;
    }

}