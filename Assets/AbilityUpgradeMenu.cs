using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class AbilityUpgradeMenu : MonoBehaviour
{
    // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public TransformAnchor playerTransformAnchor = default;
    public GameObject abilityUpgradeMenu;
    public float interactionDistance = 3.0f;
    public float maxRayDistance = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) <= interactionDistance)
            {
                Debug.Log("Player is close enough to interact");
                // Check for the space key being pressed
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("SPACE");
                    // Cast a ray from the camera through the player's forward direction
                    Ray ray = new Ray(playerTransformAnchor.Value.position, playerTransformAnchor.Value.forward);
                    RaycastHit hit;

                    // Draw the ray in the scene view for debugging
                    Debug.DrawRay(playerTransformAnchor.Value.position, playerTransformAnchor.Value.forward * maxRayDistance, Color.red);

                    // Check if the ray hits this object
                    if (Physics.Raycast(ray, out hit, maxRayDistance) && hit.transform == transform)
                    {
                        Debug.Log("Mouse on");
                        // Toggle the ability page visibility
                        abilityUpgradeMenu.SetActive(true);
                    }
                }
            }
        }
    }
}
