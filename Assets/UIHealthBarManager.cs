using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBarManager : MonoBehaviour
{
    [SerializeField] private HealthSO _playerHealth = default;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateHealthUI;
		
		InitializeHealthBar();
	}

	private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateHealthUI;
	}

    private void InitializeHealthBar()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _healthBar.maxValue = _playerHealth.MaxHealth;
        _healthBar.value = _playerHealth.CurrentHealth;
        _healthText.text = new string(_playerHealth.CurrentHealth.ToString() + "/" + _playerHealth.MaxHealth.ToString());
    }
}
