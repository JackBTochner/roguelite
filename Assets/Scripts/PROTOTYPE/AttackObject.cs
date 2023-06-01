using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class AttackObject : MonoBehaviour
{
    public DamageInfo damageInfo = new DamageInfo(DamageType.Default, 2, new CriticalData(50, 1.5f, false), 1000, Vector3.zero);

    public void Hit(Hittable hitReceiver)
    {
        if (hitReceiver != null)
        {
            damageInfo.Direction = Vector3.Normalize(hitReceiver.transform.position - this.transform.position);
            hitReceiver.Hit(damageInfo);
        }
        hitReceiver = null;
    }
}

