using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hittable : MonoBehaviour
{
    public float health;

    public float duration;
    public float intensity;

    public CameraShake camShake;

    public ObjectPooler objectPooler;

    public int poisonDamage;

    public UnityEvent OnHit = new UnityEvent();

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

    virtual public void Hit(float knockback, Vector3 direction)
    {
        OnHit.Invoke();
    }
}
