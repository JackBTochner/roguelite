using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilityupgradesound : MonoBehaviour
{
    public AudioSource audioSource;
    public float playDuration = 3.0f; 
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
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        if (audioSource.clip == null)
        {
            Debug.LogError("AudioClip is not assigned!");
            return;
        }
        Debug.Log("Trying to play music");
        audioSource.Play();
        Debug.Log("Music should be playing now");
        Invoke("StopMusic", playDuration);
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }
}
