using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ExtraDamageEffect", menuName = "Player/ProjectileEffect/ExtraDamageEffect")]
public class ProjectileEffectExtraDamageSO : PlayerProjectileEffectSO
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

    public void OnHitPerformed()
    {
        if (hittable)
        {
            Debug.Log("ProjectileEffectExtraDamage!" + "Added extra damage: " + extraDamage + " to: " + hittable.gameObject.name);
            hittable.health -= extraDamage;
            hittable.OnHit.RemoveListener(OnHitPerformed);
        }
    }
}
