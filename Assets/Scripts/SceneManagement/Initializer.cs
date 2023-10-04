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
    [Header("Load into Main Menu")]
    [SerializeField] private GameSceneSO _menuToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private AssetReference _menuLoadChannel = default;
    
    [Header("Alternative to loading into the main menu")]
    [SerializeField] public bool skipMainMenu = false;
    [SerializeField] private GameSceneSO _replacementSceneToLoad = default;
    [Header("Broadcasting on")]
    [SerializeField] private AssetReference _loadLocationChannel = default;
    void Start()
    {
        Time.timeScale = 1;
        _managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
    }

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        if(skipMainMenu)
            _loadLocationChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadReplacementScene;
        else
            _menuLoadChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadMainMenu;
    }

    private void LoadMainMenu(AsyncOperationHandle<LoadEventChannelSO> obj)
    {
        obj.Result.RaiseEvent(_menuToLoad, true);
        SceneManager.UnloadSceneAsync(0); // Initialization scene should be the only scene in BuildSettings, thus is has index 0.
    }

    private void LoadReplacementScene(AsyncOperationHandle<LoadEventChannelSO> obj)
    { 
        obj.Result.RaiseEvent(_replacementSceneToLoad, true);
        SceneManager.UnloadSceneAsync(0); // Initialization scene should be the only scene in BuildSettings, thus is has index 0.
    }
}
