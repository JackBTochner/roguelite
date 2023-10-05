using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameSceneSO _locationToLoad;
    [SerializeField] private bool _showLoadScreen = default;
    [SerializeField] private Button startButton;

    [Header("Broadcasting on")]
	[SerializeField] private LoadEventChannelSO _loadLocation = default;

    private const string GamePlays = "GamePlays";

    public void Start()
    {
        startButton.Select();
    }

    public void StartNewGame()
    {
        IncrementGamePlays();
        _loadLocation.RaiseEvent(_locationToLoad, _showLoadScreen);
    }

    private void ContinuePreviousGame()
    { 

    }

    private void IncrementGamePlays()
    {
        int currentGamePlays = PlayerPrefs.GetInt(GamePlays, 0);
        currentGamePlays++;
        PlayerPrefs.SetInt(GamePlays, currentGamePlays);
        PlayerPrefs.Save();
    }

}
