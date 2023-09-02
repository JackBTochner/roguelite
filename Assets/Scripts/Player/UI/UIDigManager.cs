/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDigManager : MonoBehaviour
{
	[SerializeField] private DigSO _dig = default;
	[SerializeField] private Image _digUIImage;

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
    private void UpdateDigUITransparency()
    {
		if (_dig.GetIsDigging)
		{
			_digUIImage.color.a = 0.5f;
		}
		else
		{
			_digUIImage.color.a = 1f;
		}
    }
}*/
