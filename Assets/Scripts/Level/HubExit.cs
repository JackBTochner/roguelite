using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HubExit : Exit
{
    private RunManager runManager;
    [SerializeField]
    private RunManagerAnchor _runManagerAnchor = default;
    
    void Start()
    { 
        if (_runManagerAnchor.isSet)
        {
            runManager = _runManagerAnchor.Value;
        }
    }

    public override void ExitTriggered(Collider other)
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        base.ExitTriggered(other);
        Debug.Log(other.name);
        runManager.StartNewRun();
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        if (_runManagerAnchor.isSet)
        {
            runManager = _runManagerAnchor.Value;
        }
    }
}