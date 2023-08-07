using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HubBackgroundMusic : MonoBehaviour
{
    private AudioSource hubbackgroundmusic;
    public TransformAnchor playerTransformAnchor = default;
    public GameObject storefront;

    private enum PlayerState
    {
        Far,
        Near
    }

    private PlayerState playerState = PlayerState.Far;
    private Coroutine volumeCoroutine;

    void Start()
    {
        hubbackgroundmusic = GetComponent<AudioSource>();
        hubbackgroundmusic.enabled = false;
        hubbackgroundmusic.volume = 0;
    }

    void Update()
    {
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            Bounds bounds = new Bounds(storefront.transform.position, Vector3.zero);
            foreach (Transform child in storefront.transform)
            {
                bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
            }
            float distance = Vector3.Distance(bounds.ClosestPoint(playerTransformAnchor.Value.position), playerTransformAnchor.Value.position);

            if (distance <= 3 && playerState == PlayerState.Far)
            {
                playerState = PlayerState.Near;
                ChangeVolume(0.8f, 2.0f);
            }
            else if (distance > 3 && playerState == PlayerState.Near)
            {
                playerState = PlayerState.Far;
                ChangeVolume(0.2f, 2.0f);
            }
        }
    }

    private void ChangeVolume(float targetVolume, float duration)
    {
        if (volumeCoroutine != null)
        {
            StopCoroutine(volumeCoroutine);
        }
        volumeCoroutine = StartCoroutine(AdjustVolume(targetVolume, duration));
    }

    private IEnumerator AdjustVolume(float targetVolume, float duration)
    {
        float startTime = Time.time;
        float startVolume = hubbackgroundmusic.volume;

        hubbackgroundmusic.enabled = true;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            hubbackgroundmusic.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null;
        }

        hubbackgroundmusic.volume = targetVolume;
    }
}