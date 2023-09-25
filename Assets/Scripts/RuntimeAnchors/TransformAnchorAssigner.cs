using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnchorAssigner : MonoBehaviour
{
    public TransformAnchor anchorToAssignTo;

    public void OnEnable()
    {
        anchorToAssignTo.Provide(this.transform);
    }
}
