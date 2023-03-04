using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene
/// and raising the event to load the Main Menu
/// </summary>

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameSceneSO _managersScene = default;
    [SerializeField] private GameSceneSO _menuToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private AssetReference _menuLoadChannel = default;

    void Start()
    {
        _managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
    }

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        _menuLoadChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadMainMenu;
    }

    private void LoadMainMenu(AsyncOperationHandle<LoadEventChannelSO> obj)
    {
        obj.Result.RaiseEvent(_menuToLoad, true);
        SceneManager.UnloadSceneAsync(0); // Initialization scene should be the only scene in BuildSettings, thus is has index 0.
    }
}
