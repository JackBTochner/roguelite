using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAttackManager : MonoBehaviour
{
	[SerializeField] private GameObject _attackUIImage;

	[Header("Listening to")]
    [SerializeField] private BoolEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateAttackUI;
	}
    private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateAttackUI;
	}
    private void UpdateAttackUI(bool isAttacking)
    {
		if (isAttacking)
		{
			_attackUIImage.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
		}
		else
		{
			_attackUIImage.GetComponent<CanvasRenderer>().SetAlpha(1f);
		}
    }

}
