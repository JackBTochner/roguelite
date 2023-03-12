using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameSceneSO _locationToLoad;
    [SerializeField] private bool _showLoadScreen = default;

    [Header("Broadcasting on")]
	[SerializeField] private LoadEventChannelSO _loadLocation = default;

    public void StartNewGame()
    {
        _loadLocation.RaiseEvent(_locationToLoad, _showLoadScreen);
    }

    private void ContinuePreviousGame()
    { 

    }
}
