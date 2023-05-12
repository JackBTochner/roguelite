using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HubExit : MonoBehaviour
{
    [HideInInspector] public bool loadsSceneByName;
    [HideInInspector] public string nextSceneName;
    public bool locked = false;
    public UnityEvent PlayerEnteredExit = new UnityEvent();

    public Material lockedMat = default;
    public Material unlockedMat = default;
    public MeshRenderer lockIndicator = default;

    [SerializeField]
    private RunManagerAnchor _runManagerAnchor = default;
    
    void OnTriggerEnter(Collider other)
    {
        if(!locked && other.tag == "Player")
            ExitTriggered(other);
    }
    public void ExitTriggered(Collider other)
    {
        _runManagerAnchor.Value.StartNewRun();
    }
}