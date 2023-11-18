using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueInfo : MonoBehaviour
{
    [SerializeField] private GameSceneSO _locationToLoad;
    [SerializeField] private bool _showLoadScreen = default;
    [SerializeField] private Button startButton;

    [Header("Broadcasting on")]
	[SerializeField] private LoadEventChannelSO _loadLocation = default;

    public void ContinueInfoButton()
    {
        _loadLocation.RaiseEvent(_locationToLoad, _showLoadScreen, true);
    }

}
