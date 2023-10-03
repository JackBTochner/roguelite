using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class JumpEnemy : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default;
    public float jumpForce = 20f; // Adjust this value to change the jump height
    private bool isJumping = false;
    private Rigidbody enemyRB;

    private CharacterController playerController;
    public OldEnemy enemyBase = default;
    private bool isAlive = true;
    public bool hasPlayer = false;
    public int damagedelt = 20;

    void OnEnable()
    {
        if (enemyBase)
        {
            enemyBase.OnEnemyDied.AddListener(EnemyDied);
        }

    }
    void OnDisable()
    {
        if (enemyBase)
        {
            enemyBase.OnEnemyDied.RemoveListener(EnemyDied);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<OldEnemy>();
        // Get the rigibody component of the current object "Jump Enemy".
        enemyRB = GetComponent<Rigidbody>();
        // Start the jumping coroutine
        StartCoroutine(waitForJump());
    }

    void EnemyDied(OldEnemy enemy)
    {
        Debug.Log("Spit out player");
        isAlive = false;
        GetComponent<BoxCollider>().enabled = false;
        EnablePlayerMovement();
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
            Vector3 lookDirection = new Vector3(playerTransformAnchor.Value.position.x, transform.position.y, playerTransformAnchor.Value.position.z );
        }
        
    }

    IEnumerator waitForJump()
    {
        while (true) 
        {
            // Wait the object is not jumping.
            // Wait isJumping = false.
            yield return new WaitUntil(() => !isJumping);

            // Wait 0.5f as second.
            yield return new WaitForSeconds(0.5f);

            // After the pause, force the object rigidbody to jump.
            enemyRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            // Set to ture, means the object is in the air.
            isJumping = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the object is on the ground, then set to false.
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }

        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (collision.gameObject.tag == "Player")
            {   // Get the playerController
                hasPlayer = true;
                

                // Turn of the player controller.
                DisablePlayerMovement();
                StartCoroutine(MovePlayerToEnemyPosition());
            }
        }
    }

    private IEnumerator MovePlayerToEnemyPosition()
    {
        // Disable player's CharacterController
        playerController.enabled = false;

        // Take damage
        PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
        playerCharacter.TakeDamage(damagedelt);

        // Time.time here is the time that the game playes.
        float startTime = Time.time;

        // This means the time now - the before loades, 
        while (Time.time - startTime < 1.5f && isAlive)
        {
            // Keep the player inside the enemy object.
            playerTransformAnchor.Value.position = transform.position;
            // pause until next fram. run once per frame
            yield return null;
        }

        //After the 1.5f. run the function.
        yield return StartCoroutine(getOutFromEnemy());
    }
    private IEnumerator getOutFromEnemy()
    {
        float Height = 2f;
        float Speed = 1f;

        // Current position
        Vector3 startPosition = playerTransformAnchor.Value.position;
        // Update position
        Vector3 endPosition = startPosition + new Vector3(0, Height, 0);

        // The current time
        float startTime = Time.time;

        // when y is lower that 2f
        while (playerTransformAnchor.Value.position.y < endPosition.y)
        {
            // Vector3.Lerp function to interpolate between the start and end positions. - 
            //  - This creates a smooth transition as the player moves from the start position to the end position.
            float t = (Time.time - startTime) / Speed;
            //The player position now, is the transition of start to end with the speed t
            playerTransformAnchor.Value.position = Vector3.Lerp(startPosition, endPosition, t);
            // pause until next fram. run once per frame
            yield return null;
        }
        hasPlayer = false;
        // Movement turn on.
        EnablePlayerMovement();
    }

    private void DisablePlayerMovement() // Turn off the player controller.
    {
        playerController.GetComponent<PlayerMovement>().allowMovement = false;
    }

    private void EnablePlayerMovement() // Turn on the player controller.
    {
        playerController.enabled = true;
        playerController.GetComponent<PlayerMovement>().allowMovement = true;
    }
}
