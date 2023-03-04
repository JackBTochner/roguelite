using UnityEngine;

/// <summary>
/// Base class for ScriptableObjects which need a public description field.
/// </summary>
public class DescriptionBaseSO : SerializableScriptableObject
{
    [TextArea] public string description;
}
