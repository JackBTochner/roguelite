using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class AttackObject : MonoBehaviour
{
    public DamageType damageType;

    public float damage = 2;

    public bool onlyHitEachObjOnce;

    public CriticalData criticalData = new CriticalData(50, 1.5f, false);

    public float knockback = 1000;


    public void Hit(Hittable hitReceiver)
    {
        if (hitReceiver != null)
        {
            Vector3 knockbackDir = Vector3.Normalize(hitReceiver.transform.position - this.transform.position);
            hitReceiver.Hit(damageType, damage, criticalData, knockback, knockbackDir);
        }
        hitReceiver = null;
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
