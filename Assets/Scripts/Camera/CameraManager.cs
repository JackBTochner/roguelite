using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Player;

public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
    public Camera mainCamera;

    [SerializeField] private TransformAnchor _cameraTransformAnchor = default;
    // [SerializeField] private CameraAnchor _cameraAnchor = default;
	[SerializeField] private TransformAnchor _playerTransformAnchor = default;
	private PlayerCharacter _playerCharacter;
    [SerializeField] private CinemachineImpulseSource impulseSource;

	[Header("Camera Movement")]
    [SerializeField] private float _cameraFollowAcceleration = 3;
	[SerializeField] private float _swaySmooth = 1;
	[SerializeField] private float _swayMultiplierX = 1;
	[SerializeField] private float _swayMultiplierY = 1;
	private float _swayHorizontal;
	private float _swayVertical;

    [Header("Listening on channels")]
	[Tooltip("The CameraManager listens to this event, fired by an event from the player, to shake camera")]
	[SerializeField] private VoidEventChannelSO _camShakeEvent = default;


    private bool _cameraMovementLock = false;

    private void OnEnable()
	{
		// inputReader.CameraMoveEvent += OnCameraMove;
		// inputReader.EnableMouseControlCameraEvent += OnEnableMouseControlCamera;
		// inputReader.DisableMouseControlCameraEvent += OnDisableMouseControlCamera;

		_playerTransformAnchor.OnAnchorProvided += SetupProtagonistVirtualCamera;
        _camShakeEvent.OnEventRaised += impulseSource.GenerateImpulse;

        _cameraTransformAnchor.Provide(mainCamera.transform);
	}

    private void OnDisable()
	{
		// inputReader.CameraMoveEvent -= OnCameraMove;
		// inputReader.EnableMouseControlCameraEvent -= OnEnableMouseControlCamera;
		// inputReader.DisableMouseControlCameraEvent -= OnDisableMouseControlCamera;

		_playerTransformAnchor.OnAnchorProvided -= SetupProtagonistVirtualCamera;
		_camShakeEvent.OnEventRaised -= impulseSource.GenerateImpulse;

		_cameraTransformAnchor.Unset();
	}

    private void Start()
	{
		//Setup the camera target if the protagonist is already available
		if (_playerTransformAnchor.isSet)
		{
			SetupProtagonistVirtualCamera();
		}
	}

    private void LateUpdate()
    {
        if (_playerTransformAnchor.isSet)
        {
			if (_playerCharacter)
			{
				if (_playerCharacter.isDigging)
				{
					_swayHorizontal = Mathf.Lerp(_swayHorizontal, inputReader.MoveComposite.x * _swayMultiplierX, Time.deltaTime * 5f);
					_swayVertical = Mathf.Lerp(_swayVertical, inputReader.MoveComposite.y * _swayMultiplierY, Time.deltaTime * 5f);
					Quaternion rotationX = Quaternion.AngleAxis(-_swayHorizontal, Vector3.up);
					Quaternion rotationY = Quaternion.AngleAxis(-_swayVertical, Vector3.right);
					Quaternion targetRotation = rotationX * rotationY;

					transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, _swaySmooth * Time.deltaTime);
				}
			}
            transform.position = Vector3.Lerp(transform.position, _playerTransformAnchor.Value.position, Time.deltaTime * _cameraFollowAcceleration);
        }
    }

    /// <summary>
    /// Provides Cinemachine with its target, taken from the TransformAnchor SO containing a reference to the player's Transform component.
    /// This method is called every time the player is reinstantiated.
    /// </summary>
    public void SetupProtagonistVirtualCamera()
	{
		// Transform target = _protagonistTransformAnchor.Value;
		if (_playerTransformAnchor.Value.TryGetComponent(out PlayerCharacter pc))
		{
			_playerCharacter = pc;
		}
		// freeLookVCam.Follow = target;
		// freeLookVCam.LookAt = target;
		// freeLookVCam.OnTargetObjectWarped(target, target.position - freeLookVCam.transform.position - Vector3.forward);
	}

}
