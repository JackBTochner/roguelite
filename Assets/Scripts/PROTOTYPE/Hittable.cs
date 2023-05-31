using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hittable : MonoBehaviour
{
    //TODO: Camera shake
    public float health;

    public float duration;
    public float intensity;

    public int poisonDamage;

    public GameObject hitMarker = default;
    public GameObject critMarker = default;

    public UnityEvent OnHit = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackObject"))
        {
            other.gameObject.GetComponent<AttackObject>().Hit(this);
        }
    }

    virtual public void Hit(DamageType damageType, float damage, CriticalData criticalData, float knockback, Vector3 direction)
    {
        int critRoll = Random.Range(0, 100);
        bool isCrit = (critRoll <= criticalData.Chance && !criticalData.Ignore);
        float finalDamage =  isCrit ? criticalData.Multiplier * damage : damage;
        GameObject finalHitMarker = isCrit ? critMarker : hitMarker;

        GameObject hitMarkerObj = Instantiate(finalHitMarker, transform.position, Quaternion.identity);
        hitMarkerObj.GetComponent<HitMarker>().Initialise(finalDamage);
        
        health -= finalDamage;
    
        OnHit.Invoke();
    }

    virtual public void Hit(DamageType damageType, float damage, CriticalData criticalData, GameObject hitMarkerOverride, GameObject critMarkerOverride, float knockback, Vector3 direction)
    {
        int critRoll = Random.Range(0, 100);
        bool isCrit = (critRoll <= criticalData.Chance && !criticalData.Ignore);
        float finalDamage =  isCrit ? criticalData.Multiplier * damage : damage;
        GameObject finalHitMarker = isCrit ? critMarkerOverride : hitMarkerOverride;

        GameObject hitMarkerObj = Instantiate(finalHitMarker, transform.position, Quaternion.identity);
        hitMarkerObj.GetComponent<HitMarker>().Initialise(finalDamage);
        
        health -= finalDamage;
    
        OnHit.Invoke();
    }
}
