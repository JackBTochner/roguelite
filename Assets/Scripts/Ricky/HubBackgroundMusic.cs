using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HubBackgroundMusic : MonoBehaviour
{
    // Attched the music in the inspector for this.
    private AudioSource hubbackgroundmusic;
    // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public TransformAnchor playerTransformAnchor = default;
    // The room we want to use for calculation
    // This is for calculation but not need anymore.
    //public GameObject storefront;

    private enum PlayerState
    {
        Far,
        Near
    }
    // Set a variable as Far.
    private PlayerState playerState = PlayerState.Far;
    //For checking action.
    private Coroutine volumeCoroutine;

    void Start()
    {
        // Get the AudioSource of this object.
        hubbackgroundmusic = GetComponent<AudioSource>();
        // At the start of the game we want to turn the background music off.
        hubbackgroundmusic.enabled = false;
        // Also the volume, we turn it louder after. The volume is 0f to 1f.
        hubbackgroundmusic.volume = 0;
    }

    void Update()
    {
        // To makesure we can get the player values.
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            // This comment code is for calculating the distance between room and player object, the bounds is for getting all the child object for an parent object. -
            // - so we can use this as one position. Then we can calculating the distance between this parent object and the player.

            //Bounds bounds = new Bounds(storefront.transform.position, Vector3.zero);
            //foreach (Transform child in storefront.transform)
            //{
            //    bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
            //}
            //float distance = Vector3.Distance(bounds.ClosestPoint(playerTransformAnchor.Value.position), playerTransformAnchor.Value.position);

            // If the player is far and hits the trigger point.
            if (playerTransformAnchor.Value.position.y <= 5.5 && playerState == PlayerState.Far)
            {
                // Changed to near
                playerState = PlayerState.Near;
                // pass in values.
                ChangeVolume(0.8f, 2.0f);
            }
            // If the player is near and hits the trigger point.
            else if (playerTransformAnchor.Value.position.y > 5.5 && playerState == PlayerState.Near)
            {
                // Changed to Far
                playerState = PlayerState.Far;
                // pass in values.
                ChangeVolume(0.1f, 2.0f);
            }
        }
    }
    // Use the pass in values, or pass it.
    private void ChangeVolume(float volumeWant, float duration)
    {
        // Check is there a action running or not.
        if (volumeCoroutine != null)
        {
            // Stop this action.
            StopCoroutine(volumeCoroutine);
        }
        // If there is no action, run this.
        volumeCoroutine = StartCoroutine(AdjustVolume(volumeWant, duration));
    }

    private IEnumerator AdjustVolume(float volumeWant, float duration)
    {
        // Time.time means when I press start the game the total time is been running
        float startTime = Time.time;
        // Get the current volume.
        float startVolume = hubbackgroundmusic.volume;
        // Make sure turn it on enable.
        hubbackgroundmusic.enabled = true;
        // While the total game time is less than the trigger time + the duration
        while (Time.time < startTime + duration)
        {
            // Calculates the amount of time that has elapsed since the volume change started.
            float t = (Time.time - startTime) / duration;
            // Set start Volume to volumeWant, with t timespeed.
            hubbackgroundmusic.volume = Mathf.Lerp(startVolume, volumeWant, t);
            //Pauses the execution of the coroutine until the next frame, allowing other game processes to run.
            yield return null;
        }
        // Set is as that volume.
        hubbackgroundmusic.volume = volumeWant;
    }
}