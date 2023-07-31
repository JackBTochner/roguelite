using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlaceholderFire", menuName = "Player/ProjectileEffect/PlaceholderFire")]
public class ProjectileEffectPlaceholderFireSO : ProjectileEffectSO
{
    public float extraDamage = 5f;
    Hittable hittable = null;

    public override void Initialise(GameObject target)
    {
        hittable = target.GetComponent<Hittable>();
        if (hittable)
        {
            hittable.OnHit.AddListener(OnHitPerformed);
        }
    }

    public void OnHitPerformed(DamageInfo damageInfo)
    {
        if (hittable)
        {
            Debug.Log("PlaceholderFire!" + "Added extra damage: " + extraDamage + " to: " + hittable.gameObject.name);
            hittable.health -= extraDamage;
            hittable.OnHit.RemoveListener(OnHitPerformed);
        }
    }
}
