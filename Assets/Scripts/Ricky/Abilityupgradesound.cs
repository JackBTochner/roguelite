using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilityupgradesound : MonoBehaviour
{
    // Get the Audio
    public AudioSource audioSource;
    // The Audio time we want to play, haven't cut the audio yet, we using the first sound.
    public float musicduration = 3.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayMusic()
    {
        // Check the Audio is implemented.
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        audioSource.Play();
        // Run "StopMusic" after musicduration
        Invoke("StopMusic", musicduration);
    }

    private void StopMusic()
    {
        // Stop the music
        audioSource.Stop();
    }
}
