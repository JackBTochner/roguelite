/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAttackManager : MonoBehaviour
{
	[SerializeField] private Image _attackUIImage;
	[SerializeField] private bool _toggle;

	[Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateAttackUI;
		
		InitializeAttackUI();
	}
    private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateAttackUI;
	}
    private void InitializeAttackUI()
    {
        UpdateAttackUI();
    }
    private void UpdateAttackUITransparency()
    {
		if (_toggle)
		{
			_attackUIImage.color.a = 0.5f;
		}
		else
		{
			_attackUIImage.color.a = 1f;
		}
    }

}*/
