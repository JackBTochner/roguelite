using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player;

[ExecuteAlways]
public class UIStaminaRadialBar : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private Image staminaBarFill;

    [SerializeField] private StaminaSO _playerStamina = default;

    [SerializeField] private TextMeshProUGUI _staminaText;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default;
    [SerializeField] private TransformAnchor _cameraTransformAnchor = default;
    [SerializeField] private PlayerCharacter playerCharacter;

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

    private void Update()
    {
        // Always face the camera 
        if (_cameraTransformAnchor.isSet)
        {
            Vector3 dir = transform.position - _cameraTransformAnchor.Value.position;
            //transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(transform.forward, dir) + 90);
            transform.LookAt(_cameraTransformAnchor.Value.position);
        }
        
    }

    private void UpdateStaminaUI()
    {
        float staminaPercentage =  _playerStamina.CurrentStamina / _playerStamina.MaxStamina;
        // Remap to range 0 to 0.5
        staminaBarFill.fillAmount = staminaPercentage / 2;

        // Assign colors
        //if(playerCharacter)

        //_staminaBar.maxValue = _playerStamina.MaxStamina;
        //_staminaBar.value = _playerStamina.CurrentStamina;
        if(_staminaText)
            _staminaText.text = new string(_playerStamina.CurrentStamina.ToString() + "/" + _playerStamina.MaxStamina.ToString());
    }
}
