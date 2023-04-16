using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;

    public Animator playerAnim;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        playerAnim.SetTrigger("Attack");
    }
}
