using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Map;

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

    private void OnEnable()
    {
        runManagerAnchor.Provide(this);
    }

    private void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
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
    }

    public void StartNewRun()
    {
        ClearCurrentRun();
        mapManager.GenerateNewMap();
    }

    private void ClearCurrentRun()
    {
        lastRun = currentRun;
        currentRun = new Run();
    }

    void OnSceneUnloaded(Scene current)
    {
        mapCanvas.SetActive(false);
        playerExit = null;
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        
        playerExit = GameObject.FindGameObjectWithTag("PlayerExit").GetComponent<Exit>();
        //TODO: add check to see which type of scene we are in.
        if (playerExit != null)
        {
            playerExit.PlayerEnteredExit.AddListener(OnPlayerEnterExit);
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
