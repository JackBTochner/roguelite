using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;

    public Animator playerAnim;
    public Animator attackAnim;
    public PlaySingleAudioClip audioPlayer;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
			
        }
        else
        {
            
        }

    }

    void Attack()
    {
        playerAnim.SetTrigger("Attack");
        attackAnim.SetTrigger("Attack");
        audioPlayer.PlayAudioClip();
    
    }

    
}
