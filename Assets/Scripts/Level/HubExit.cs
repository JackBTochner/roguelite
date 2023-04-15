using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HubExit : Exit
{
    [SerializeField]
    private RunManagerAnchor _runManagerAnchor = default;
    
    void Start()
    { 
    }

    public override void ExitTriggered(Collider other)
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        base.ExitTriggered(other);
        Debug.Log(other.name);
        Debug.Log(_runManagerAnchor.Value);
        _runManagerAnchor.Value.StartNewRun();
    }

    void OnSceneChanged(Scene current, Scene next)
    {
    }
}