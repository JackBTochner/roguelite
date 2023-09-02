using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDigManager : MonoBehaviour
{
	[SerializeField] private DigSO _dig = default;
	[SerializeField] private GameObject _digUIImage;

	[Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateDigUI;
		
		InitializeDigUI();
	}
    private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateDigUI;
	}
    private void InitializeDigUI()
    {
        UpdateDigUI();
    }
    private void UpdateDigUI()
    {
		bool isDiggingValue = _dig.GetIsDigging();
		if (isDiggingValue)
		{
			_digUIImage.GetComponent<CanvasRenderer>().SetAlpha(1f);
		}
		else
		{
			_digUIImage.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
		}
    }
}
