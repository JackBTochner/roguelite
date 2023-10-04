using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DebugKeysHub : MonoBehaviour
{
    private bool keyboardkeys = false;
    [SerializeField] private RunManagerAnchor _runManagerAnchor = default;
    // Start is called before the first frame update
    void Start()
    {
        keyboardkeys = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (keyboardkeys == false && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Start debuging");
            keyboardkeys = true;
            StartCoroutine(DebugWaitTime());
            StartCoroutine(Debugtime());
        }

        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("O");
            RestartGame();
        }

        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I");
            DialogueLua.SetVariable("PlayedConversations", "[]");
            SceneManager.LoadScene("Initialization");
        }

        //if (keyboardkeys == true && Input.GetKeyDown(KeyCode.U))
        //{
        //    Debug.Log("U");
        //    if (_runManagerAnchor != null)
        //        _runManagerAnchor.Value.ReturnToHub();
        //}
    }

    IEnumerator Debugtime()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("debuging ended");
        keyboardkeys = false;
    }

    IEnumerator DebugWaitTime()
    {
        yield return new WaitForSeconds(0.3f);
    }

    void RestartGame()
    {
        Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }
}