using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEffectSO : DescriptionBaseSO
{
    public Sprite Icon;
    public abstract void Initialise(GameObject target);
}
