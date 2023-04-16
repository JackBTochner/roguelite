using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public float health;

    public float duration;
    public float intensity;

    public CameraShake camShake;

    public ObjectPooler objectPooler;

    public int poisonDamage;

 //   PlayerCharacter playerChar;

    virtual public void Start()
    {
  //      playerChar = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        objectPooler = ObjectPooler.Instance;
        camShake = Camera.main.gameObject.GetComponent<CameraShake>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackObject"))
        {
            other.gameObject.GetComponent<AttackObject>().Hit(gameObject);
            //Hit(0);
        }
    }

    virtual public void Hit(float knockback)
    {

    }
}
