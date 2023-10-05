using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TMPro;

public class DebugKeysMainMenu : MonoBehaviour
{
    private bool keyboardkeys = false;

    public TextMeshProUGUI gamePlaysText;
    // Start is called before the first frame update
    void Start()
    {
        keyboardkeys = false;
        gamePlaysText.gameObject.SetActive(false);
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

        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.U))
        {
            gamePlaysText.gameObject.SetActive(true);
            int currentGamePlays = PlayerPrefs.GetInt("GamePlays", 0);
            gamePlaysText.text = "Total game plays:" + currentGamePlays.ToString();
        }

        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.Y))
        {
            gamePlaysText.gameObject.SetActive(false);
        }

        if (keyboardkeys == true && Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.SetInt("GamePlays", 0);
            PlayerPrefs.Save();
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

    void RestartGame()
    {
        Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }
}
