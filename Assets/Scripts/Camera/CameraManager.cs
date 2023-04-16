using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
    public Camera mainCamera;

    [SerializeField] private TransformAnchor _cameraTransformAnchor = default;
    // [SerializeField] private CameraAnchor _cameraAnchor = default;
	[SerializeField] private TransformAnchor _playerTransformAnchor = default;

    [Header("Listening on channels")]
	[Tooltip("The CameraManager listens to this event, fired by an event from the player, to shake camera")]
	[SerializeField] private VoidEventChannelSO _camShakeEvent = default;

    [SerializeField] private float _cameraFollowAcceleration = 3;

    private bool _cameraMovementLock = false;

    private void OnEnable()
	{
		// inputReader.CameraMoveEvent += OnCameraMove;
		// inputReader.EnableMouseControlCameraEvent += OnEnableMouseControlCamera;
		// inputReader.DisableMouseControlCameraEvent += OnDisableMouseControlCamera;

		_playerTransformAnchor.OnAnchorProvided += SetupProtagonistVirtualCamera;
		// _camShakeEvent.OnEventRaised += impulseSource.GenerateImpulse;

		_cameraTransformAnchor.Provide(mainCamera.transform);
	}

    private void OnDisable()
	{
		// inputReader.CameraMoveEvent -= OnCameraMove;
		// inputReader.EnableMouseControlCameraEvent -= OnEnableMouseControlCamera;
		// inputReader.DisableMouseControlCameraEvent -= OnDisableMouseControlCamera;

		_playerTransformAnchor.OnAnchorProvided -= SetupProtagonistVirtualCamera;
		// _camShakeEvent.OnEventRaised -= impulseSource.GenerateImpulse;

		_cameraTransformAnchor.Unset();
	}

    private void Start()
	{
		//Setup the camera target if the protagonist is already available
		if(_playerTransformAnchor.isSet)
			SetupProtagonistVirtualCamera();
	}

    private void Update()
    {
        if (_playerTransformAnchor.isSet)
        {
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

		// freeLookVCam.Follow = target;
		// freeLookVCam.LookAt = target;
		// freeLookVCam.OnTargetObjectWarped(target, target.position - freeLookVCam.transform.position - Vector3.forward);
	}

}
