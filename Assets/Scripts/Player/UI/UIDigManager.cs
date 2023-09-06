using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDigManager : MonoBehaviour
{
	[SerializeField] private GameObject _digUIImage;

	[Header("Listening to")]
    [SerializeField] private BoolEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateDigUI;
	}
    private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateDigUI;
	}
    private void UpdateDigUI(bool isDigging)
    {
		if (isDigging)
		{
			_digUIImage.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		}
		else
		{
			_digUIImage.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
		}
    }
}
