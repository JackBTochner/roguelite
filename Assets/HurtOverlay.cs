using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HurtOverlay : MonoBehaviour
{
	[SerializeField] private Image _hurtImage;
    public Color damagedColor;

	[Header("Listening to")]
    [SerializeField] private IntEventChannel _UIUpdateValue = default;

    private void OnEnable()
	{
		_UIUpdateValue.OnEventRaised += UpdateHurtUI;
	}
    private void OnDestroy()
	{
		_UIUpdateValue.OnEventRaised -= UpdateHurtUI;
	}
    private void UpdateHurtUI(int value)
    {
        //float alpha = Mathf.Lerp(0, 1, value / 100);
        damagedColor.a = Mathf.PingPong(Time.time, 1);
        _hurtImage.color = damagedColor;
    }
}