using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuickAnimManager : MonoBehaviour
{
	[SerializeField] private GameObject _UIImage;

	[Header("Listening to")]
    [SerializeField] private BoolEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateUI;
	}
    private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateUI;
	}
    private void UpdateUI(bool isActivated)
    {
		if (isActivated)
		{
			StartCoroutine(UIEffect());
		}
		else
		{
			_UIImage.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		}
    }
    
    IEnumerator UIEffect()
    {
	    Image img = _UIImage.GetComponent<Image>();
	    img.CrossFadeAlpha(0.1f, 0.1f, false);
	    yield return new WaitForSeconds(0.1f);
	    img.CrossFadeAlpha(1.0f, 0.1f, false);
    }

}
