using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HubBackgroundMusic : MonoBehaviour
{
    private AudioSource hubbackgroundmusic;
    // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public TransformAnchor playerTransformAnchor = default;

    public GameObject storefront;
    // Start is called before the first frame update
    void Start()
    {
        hubbackgroundmusic = GetComponent<AudioSource>(); // This will automaticlly get the AudioSource from "this" object.
        hubbackgroundmusic.enabled = false; // Turn the background music off at the start of the scene.
    }

    // Update is called once per frame
    void Update()
    {
        // Check this is not null.
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
        //{
        //    float distance = Vector3.Distance(playerTransformAnchor.Value.position, storefront.transform.position);
        //    if (distance < 3)
        //    {
        //        if (!hubbackgroundmusic.enabled)
        //        {
        //            hubbackgroundmusic.enabled = true;
        //        }

        //    }
        //    if (hubbackgroundmusic.enabled && distance > 5)
        //    {
        //        float volume = Mathf.Clamp(1 - ((distance - 3) / 2), 0.2f, 1);
        //        hubbackgroundmusic.volume = volume;
        //    }
        //}
            Transform parentTransform = storefront.transform;

            bool isWithinDistance = false;

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform childTransform = parentTransform.GetChild(i);
                float distance = Vector3.Distance(playerTransformAnchor.Value.position, childTransform.position);

                if (distance < 3)
                {
                    isWithinDistance = true;
                    break;
                }
            }

            if (isWithinDistance)
            {
                if (!hubbackgroundmusic.enabled)
                {
                    hubbackgroundmusic.enabled = true;
                }
            }
            else if (hubbackgroundmusic.enabled)
            {
                float distanceToStorefront = Vector3.Distance(playerTransformAnchor.Value.position, storefront.transform.position);
                float volume = Mathf.Clamp(1 - ((distanceToStorefront - 3) / 2), 0.2f, 1);
                hubbackgroundmusic.volume = volume;

                if (distanceToStorefront > 5)
                {
                    hubbackgroundmusic.enabled = false; // You may want to disable the music if the distance is too great
                }
            }
        }
    }
}
