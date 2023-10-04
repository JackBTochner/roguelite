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
			StartCoroutine(UIEffect());
		}
		else
		{
			_attackUIImage.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		}
    }
    
    IEnumerator UIEffect()
    {
	    Image img = _attackUIImage.GetComponent<Image>();
	    img.CrossFadeAlpha(0.1f, 0.1f, false);
	    yield return new WaitForSeconds(0.1f);
	    img.CrossFadeAlpha(1.0f, 0.1f, false);
    }

}
