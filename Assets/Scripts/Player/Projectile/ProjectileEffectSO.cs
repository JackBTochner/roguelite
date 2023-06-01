using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEffectSO : DescriptionBaseSO
{
    public Sprite Icon;

    public void Copy(ProjectileEffectSO from)
    {
        this.Icon = from.Icon;
    }

    public abstract void Initialise(GameObject target);
}
