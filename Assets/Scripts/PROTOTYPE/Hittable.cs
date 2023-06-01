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

    public DamageEvent OnHit = new DamageEvent();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackObject"))
        {
            other.gameObject.GetComponent<AttackObject>().Hit(this);
        }
    }

    virtual public void Hit(DamageInfo damageInfo)
    {
        int critRoll = Random.Range(0, 100);
        bool isCrit = (critRoll <= damageInfo.CriticalData.Chance && !damageInfo.CriticalData.Ignore);
        float finalDamage =  isCrit ? damageInfo.CriticalData.Multiplier * damageInfo.Damage : damageInfo.Damage;
        GameObject finalHitMarker = isCrit ? critMarker : hitMarker;

        GameObject hitMarkerObj = Instantiate(finalHitMarker, transform.position, Quaternion.identity);
        hitMarkerObj.GetComponent<HitMarker>().Initialise(finalDamage);
        
        health -= finalDamage;
    
        OnHit.Invoke(damageInfo);
    }

    virtual public void Hit(DamageInfo damageInfo, GameObject hitMarkerOverride, GameObject critMarkerOverride)
    {
        int critRoll = Random.Range(0, 100);
        bool isCrit = (critRoll <= damageInfo.CriticalData.Chance && !damageInfo.CriticalData.Ignore);
        float finalDamage =  isCrit ? damageInfo.CriticalData.Multiplier * damageInfo.Damage : damageInfo.Damage;
        GameObject finalHitMarker = isCrit ? critMarkerOverride : hitMarkerOverride;

        GameObject hitMarkerObj = Instantiate(finalHitMarker, transform.position, Quaternion.identity);
        hitMarkerObj.GetComponent<HitMarker>().Initialise(finalDamage);
        
        health -= finalDamage;
    
        OnHit.Invoke(damageInfo);
    }
}

[System.Serializable]
public class DamageEvent : UnityEvent<DamageInfo>
{ 
}

[System.Serializable]
public class DamageInfo {
    public DamageInfo(DamageType damageType, float damage, CriticalData criticalData, float knockback, Vector3 direction)
    {
        this.DamageType = damageType;
        this.Damage = damage;
        this.CriticalData = criticalData;
        this.Knockback = knockback;
        this.Direction = direction;
    }

    private Vector3 _direction;

    public DamageType DamageType;
    public float Damage = 2;
    public CriticalData CriticalData = new CriticalData(50, 1.5f, false);
    public float Knockback = 1000;
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
}

[System.Serializable]
public struct CriticalData
{
    public CriticalData(float chance, float multiplier, bool ignore)
    {
        Chance = chance;
        Multiplier = multiplier;
        Ignore = ignore;
    }

    [Range(0, 100)]
    public float Chance;
    public float Multiplier;
    public bool Ignore;

    public override string ToString() => $"({Chance}, {Multiplier}, {Ignore})";
}

public enum DamageType
{ 
    Default,
    Dig,
    Projectile,
    Melee,
    Environmental,
    Poison
}
