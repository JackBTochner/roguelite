using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerSpawnSystem : MonoBehaviour
{
    [Header("AssetReferences")]
    [SerializeField] private InputReader _inputReader = default;
	[SerializeField] private PlayerCharacter _playerPrefab = default;
	[SerializeField] private TransformAnchor _playerTransformAnchor = default;
	[SerializeField] private TransformEventChannelSO _playerInstantiatedChannel = default;
	// [SerializeField] private PathStorageSO _pathTaken = default;

    [Header("Scene Ready Event")]
	[SerializeField] private VoidEventChannelSO _onSceneReady = default; //Raised by SceneLoader when the scene is set to active

//    private LocationEntrance[] _spawnLocations;
    private Transform _defaultSpawnPoint;

    public PlayerCharacter getPlayer(){
        return _playerPrefab;
    }
    private void Awake()
    {
//        _spawnLocations = GameObject.FindObjectsOfType<LocationEntrance>();
        _defaultSpawnPoint = transform.GetChild(0);
    }

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += SpawnPlayer;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnPlayer;
    }

    private Transform GetSpawnLocation()
    {
        return _defaultSpawnPoint;
    }

    private void SpawnPlayer()
    {
        Transform spawnLocation = GetSpawnLocation();
        PlayerCharacter playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

        _playerInstantiatedChannel.RaiseEvent(playerInstance.transform);
        _playerTransformAnchor.Provide(playerInstance.transform);
        // _inputReader.EnableGameplayInput();
    }
}
