using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class MeleeWisp : MonoBehaviour
{
    // Player
    public TransformAnchor playerTransformAnchor = default;
    // When to trigger the attack
    public float interactionDistance = 0.5f;
    // The Empty object not the real sword, because we need this to rotate the sword.
    public GameObject Sword;
    // This is the real object
    public GameObject RealSword;
    // Get the rotation, and set a rotation.
    private Quaternion startRotation;
    private Quaternion endRotation;
    // Slash speed
    public float slashDuration = 0.5f;
    // Attack Cooldown
    private float attackCooldown = 1.5f;
    // Check the next attack time
    private float nextAttackTime = 0f;
    // Damage
    public int damage = 20;
    // Check slah
    public bool isslashing = false;
    // The player can take damage
    private bool takedamage = true;

    void Start()
    {
        // Get rotation
        startRotation = Sword.transform.localRotation;
        // Set rotation
        endRotation = startRotation * Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        //
        if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < 1.5)
        {
            //transform.LookAt(playerTransformAnchor.Value);
            Vector3 targetPosition = playerTransformAnchor.Value.position;
            targetPosition.y = transform.position.y; 
            transform.LookAt(targetPosition);
        }
        //

        // Check the player is spawn or not
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            // If in the trigger distance and the avaliable to do the next attack with 1.5 second.
            if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) <= interactionDistance && Time.time >= nextAttackTime)
            {
                // Do attack
                StartCoroutine(SlashRoutine());
                // Add time for checking next attack
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    IEnumerator SlashRoutine()
    {
        // Create a timer
        float timeatt = 0;
        // Smaller than the slashDuration
        while (timeatt < slashDuration)
        {
            isslashing = true;
            // Each Time in Time.deltaTime will be added in timeatt, the while loop is the whole animation of this Quanternion.Lerp
            Sword.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, timeatt / slashDuration);
            timeatt += Time.deltaTime;
            yield return null;
        }
        Sword.transform.localRotation = endRotation;

        timeatt = 0;
        while (timeatt < slashDuration)
        {
            isslashing = false;
            Sword.transform.localRotation = Quaternion.Lerp(endRotation, startRotation, timeatt / slashDuration);
            timeatt += Time.deltaTime;
            yield return null;
        }

        Sword.transform.localRotation = startRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isslashing == true && takedamage == true)
        {
            PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
            playerCharacter.TakeDamage(damage);
            //Player can take damage after 1 second
            StartCoroutine(DamageCooldown());
        }
    }
    IEnumerator DamageCooldown()
    {
        takedamage = false; 
        yield return new WaitForSeconds(1f);
        takedamage = true; 
    }
}
