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

    public void Start()
    {
        startButton.Select();
    }

    public void StartNewGame()
    {
        _loadLocation.RaiseEvent(_locationToLoad, _showLoadScreen);
    }

    private void ContinuePreviousGame()
    { 

    }
}
