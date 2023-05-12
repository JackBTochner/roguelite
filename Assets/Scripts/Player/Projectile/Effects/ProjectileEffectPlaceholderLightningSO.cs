using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlaceholderLightning", menuName = "Player/ProjectileEffect/PlaceholderLightning")]
public class ProjectileEffectPlaceholderLightningSO : ProjectileEffectSO
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
            Debug.Log("PlaceholderLightning!" + "Added extra damage: " + extraDamage + " to: " + hittable.gameObject.name);
            hittable.health -= extraDamage;
            hittable.OnHit.RemoveListener(OnHitPerformed);
        }
    }
}
