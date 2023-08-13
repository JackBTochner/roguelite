using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySingleAudioClip : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;
    void Start(){
        source=GetComponent<AudioSource>();
    }
    public void PlayAudioClip()
    {
        source.PlayOneShot(clip,1);
    }
}
