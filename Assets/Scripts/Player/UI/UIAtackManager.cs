using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAttackManager : MonoBehaviour
{
	[SerializeField] private AttackSO _attack = default;
	[SerializeField] private GameObject _attackUIImage;

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
    private void UpdateAttackUI()
    {
		bool isAttackingValue = _attack.GetIsAttack();
		if (isAttackingValue)
		{
			_attackUIImage.GetComponent<CanvasRenderer>().SetAlpha(1f);
		}
		else
		{
			_attackUIImage.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
		}
    }

}
