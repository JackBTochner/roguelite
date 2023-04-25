using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public List<string> VisitedEncounters;

    public Run currentRun;

    public Run lastRun;

    public MapManagerSO mapManager;

    public GameObject mapCanvas;

    public Exit playerExit;

    public RunManagerAnchor runManagerAnchor;

    public GameSceneSO hubSceneSO = default;

    public LoadEventChannelSO loadLocation = default;

    public VoidEventChannelSO returnToHub = default;

    private void OnEnable()
    {
        runManagerAnchor.Provide(this);
    }

    private void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Start()
    {
        AssignPlayerExit();
    }

    // TODO: REPLACE WITH A WAY TO END THE RUN
    /*
    IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Hub");
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void EndCurrentRun()
    {
        Debug.Log("Yeah");
        StartCoroutine(LoadNextSceneAsync());
        ClearCurrentRun();
    }*/
    
    public void ReturnToHub()
    {
        ClearCurrentRun();
        loadLocation.RaiseEvent(hubSceneSO, false, false);
        returnToHub.RaiseEvent();
    }

    public void StartNewRun()
    {
        ClearCurrentRun();
        mapManager.GenerateNewMap();
        Debug.Log("StartingNewRun");
    }

    private void ClearCurrentRun()
    {
        lastRun = currentRun;
        currentRun = new Run();
    }

    private void OnSceneUnloaded(Scene current)
    {
        mapCanvas.SetActive(false);
        playerExit = null;
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignPlayerExit();
    }

    private void AssignPlayerExit()
    {
        var exitGO = GameObject.FindWithTag("PlayerExit");
        if (exitGO)
        {
            playerExit = exitGO.GetComponent<Exit>();
        }

        //TODO: add check to see which type of scene we are in.
        if (playerExit != null)
        {
            playerExit.PlayerEnteredExit.RemoveListener (OnPlayerEnterExit);
            playerExit.PlayerEnteredExit.AddListener (OnPlayerEnterExit);
        }
    }

    public void OnPlayerEnterExit()
    {
        //mapManager.GetComponentInParent<Canvas>(true).gameObject.SetActive(true);
        mapCanvas.SetActive(true);
        Debug.Log("PlayerEnteredExit");
    }
}

[System.Serializable]
public class Run
{
    [SerializeField]
    public RunStatistics runStatistics;
}

[System.Serializable]
public struct RunStatistics
{
    public List<string> VisitedEncounters;

    public int nodesTravelled;

    public int bossNodesTravelled;

    public Dictionary<string, int> enemiesDefeated; // key: enemy, value: amt killed

    public string diedTo;

    public int currencyGained;

    public int currencySpent;

    public int boonsGained;

    public Dictionary<string, int> characterBoons; // key: character, value: amt of boons
}
