using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIProjectileCountManager : MonoBehaviour
{
    [SerializeField] private ProjectileCountSO _projectileCount = default;
    [SerializeField] private TextMeshProUGUI _projectileText;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _updateProjectileUI = default;

    private void OnEnable()
	{
		_updateProjectileUI.OnEventRaised += UpdateProjectileUI;
		
		InitializeProjectileUI();
	}
    private void OnDisable()
	{
		_updateProjectileUI.OnEventRaised -= UpdateProjectileUI;
	}
    private void InitializeProjectileUI()
    {
        UpdateProjectileUI();
    }
    private void UpdateProjectileUI()
    {
        _projectileText.text = new string(_projectileCount.CurrentProjectileCount.ToString() + "/" + _projectileCount.MaxProjectileCount.ToString());
    }

}
