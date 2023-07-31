using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStaminaBarManager : MonoBehaviour
{
    [SerializeField] private StaminaSO _playerStamina = default;

    [SerializeField] private Slider _staminaBar;
    [SerializeField] private TextMeshProUGUI _staminaText;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateStaminaUI;
		
		InitializeStaminaBar();
	}

	private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateStaminaUI;
	}

    private void InitializeStaminaBar()
    {
        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        _staminaBar.maxValue = _playerStamina.MaxStamina;
        _staminaBar.value = _playerStamina.CurrentStamina;
        _staminaText.text = new string(_playerStamina.CurrentStamina.ToString() + "/" + _playerStamina.MaxStamina.ToString());
    }
}
