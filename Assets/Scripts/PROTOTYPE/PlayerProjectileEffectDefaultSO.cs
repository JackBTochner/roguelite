using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEffect", menuName = "Player/ProjectileEffect/DefaultEffect")]
public class PlayerProjectileEffectDefaultSO : PlayerProjectileEffectSO
{
    public override void Initialise(GameObject target)
    {
        Debug.Log("DefaultEffect Initialised");
    }
}
