using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class AbilityUpgradeMenu : MonoBehaviour
{
    // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public TransformAnchor playerTransformAnchor = default;
    // This is the object of the ability upgrade menu, which is a canvas.
    public GameObject abilityUpgradeMenu;
    // When the player distance is "3".
    public float interactionDistance = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check the player is spawn or not
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            // Check distance between player and the object.
            if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) <= interactionDistance)
            {
                Debug.Log("Player is close enough to interact");
                // Check for the space key being pressed
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("SPACE");
                    // Toggle the ability page visibility
                    abilityUpgradeMenu.SetActive(true);
                }
            }
        }
    }
    // Turn off the Menu when player press exit
    public void ExitbuttonOff()
    {
        abilityUpgradeMenu.SetActive(false);
    }
}
