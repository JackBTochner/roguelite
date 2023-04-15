using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Exit : MonoBehaviour
{
    [HideInInspector] public bool loadsSceneByName;
    [HideInInspector] public string nextSceneName;
    public bool locked = false;
    public UnityEvent PlayerEnteredExit = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        if(!locked || other.tag == "Player")
        {
            ExitTriggered(other);
        }
    }
    public virtual void ExitTriggered(Collider other)
    {
        PlayerEnteredExit.Invoke();
        Debug.Log("ExitTriggered");
        if(nextSceneName != "")
        {
            StartCoroutine(LoadNextSceneAsync());
        } else
        {
            Debug.Log("No nextSceneName defined.");
        }
    }

    IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 offset = new(0,1,0);
        Gizmos.DrawWireCube(transform.position + offset, new(1,2,1));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Exit), true)]
public class ExitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Exit script = (Exit)target;
        script.loadsSceneByName = EditorGUILayout.Toggle("Load Scene by Name", script.loadsSceneByName);
        if(script.loadsSceneByName)
        {
            script.nextSceneName = EditorGUILayout.TextField("Next Scene Name", script.nextSceneName);
        }
    }
}
#endif