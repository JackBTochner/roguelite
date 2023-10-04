using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class DebugKeys : MonoBehaviour
{
    private bool keyboardkeys;
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
        }
        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.I))
        {
            if (_runManagerAnchor != null)
                _runManagerAnchor.Value.ReturnToHub();
        }
        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.U))
        {
            DialogueLua.SetVariable("PlayedConversations", "[]");
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Initialization");
        }
        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y");
        }
        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.T))
        {
            
        }
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
}
