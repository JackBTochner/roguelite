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

    [SerializeField] private GameObject _staminaBar;
    
    [SerializeField] private Image staminaBarFill;

    [SerializeField] private StaminaSO _playerStamina = default;

    [SerializeField] private TextMeshProUGUI _staminaText;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default;
    [SerializeField] private TransformAnchor _cameraTransformAnchor = default;
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;
	private PlayerCharacter _playerCharacter;

    private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateStaminaUI;
        _playerTransformAnchor.OnAnchorProvided += AssignPlayerCharacter;
		InitializeStaminaBar();
	}

    private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateStaminaUI;
        _playerTransformAnchor.OnAnchorProvided -= AssignPlayerCharacter;
	}

    private void InitializeStaminaBar()
    {
        UpdateStaminaUI();
    }

    private void Update()
    {
// TODO: Replace with an event + listener instead.
        if (_playerCharacter)
        {
            _staminaBar.SetActive(_playerCharacter.isDigging);
        }
        /*
        // Always face the camera 
        if (_cameraTransformAnchor.isSet)
        {
            Vector3 dir = transform.position - _cameraTransformAnchor.Value.position;
            //transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(transform.forward, dir) + 90);
            transform.LookAt(_cameraTransformAnchor.Value.position);
        }
        */
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

    private void AssignPlayerCharacter()
    {
        if (_playerTransformAnchor.Value.TryGetComponent(out PlayerCharacter pc))
		{
			_playerCharacter = pc;
		}
    }
}
