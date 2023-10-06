using System.Collections;
using System.Collections.Generic;
using Malee.List;
using Map;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnManager : MonoBehaviour
{
    [Reorderable]
    public ListOfEnemySpawnWaves waves;

    public float currentWave = 0;

    public Exit playerExit;
    //
        public GameObject abilityUpgradeMenu;
    //
    [Header("Listening On")]
    [SerializeField]
    private VoidEventChannelSO _onSceneReady = default; //Raised by SceneLoader when the scene is set to active
    [SerializeField]
    private TransformAnchor _playerTransformAnchor = default;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += SpawnWaves;
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnWaves;
        SceneManager.activeSceneChanged -= OnSceneChanged;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene current)
    {
        playerExit = null;
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        playerExit = null;
        GameObject exitGO = GameObject.FindWithTag("PlayerExit");
        if (exitGO != null)
        {
            playerExit = exitGO.GetComponent<PlayerExit>();
            LockPlayerExit();
        }
    }

    void SpawnWaves()
    {
        //
            //abilityUpgradeMenu.SetActive(true);
        //
        StartCoroutine(SpawnWavesRoutine());
    }

    IEnumerator SpawnWavesRoutine()
    {
//        Debug.Log("Spawning waves");
        for (int i = 0; i < waves.Count; i++)
        {
            // waves[i].SpawnWave();
            StartCoroutine(waves[i].SpawnWave(_playerTransformAnchor));
            while (!waves[i].IsWaveComplete()) yield return null;
            Debug.Log("Wave: " + (i + 1) + "Completed!");
            
            //if (i < waves.Count -1 )
            //{   
            //    abilityUpgradeMenu.SetActive(true);
            //    while (abilityUpgradeMenu.activeSelf)
            //    {
            //        Debug.Log(waves.Count);
            //        yield return null;
            //    }
            //}
        }
        UnlockPlayerExit();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.SaveScore(false);
    }

    private void LockPlayerExit()
    {
        if (playerExit != null) playerExit.LockExit();
    }

    private void UnlockPlayerExit()
    {
        if (playerExit != null) playerExit.UnlockExit();
    }
}

[System.Serializable]
public class ListOfEnemySpawnWaves : ReorderableArray<EnemySpawnWave> { }

[System.Serializable]
public class EnemySpawnWave
{
    public string name;

    public GameObject enemySpawnIndicator;
    public float individualSpawnDelay = 2.0f;

    [Header("Preconditions")]
    public float waveInitialSpawnDelay = 0.0f;

    [Header("Complete Conditions")]
    public float waveCompleteDelay = 0.0f;

    private float _delayCompleteTime = Mathf.NegativeInfinity;

    [Tooltip("If assigned, wave will require the player to activate the trigger to complete the wave.\n If left unassigned, will ignore the requirement")]
    public SpawnWaveTrigger nextWaveSpawnTrigger;

    private bool triggeredNextWave = false;

    public bool mustKillAllEnemiesToCompleteWave = true;

    [Header("Wave Details")]
    public float spawnRadius = 25f;

    public Enemy[] enemiesToSpawn;

    [HideInInspector]
    public int currentEnemies;

    public bool IsWaveComplete()
    {
        bool complete = true;

        if (mustKillAllEnemiesToCompleteWave && currentEnemies > 0)
            complete = false;
        if (Time.time < _delayCompleteTime) complete = false;
        if (nextWaveSpawnTrigger != null && triggeredNextWave == false)
            complete = false;

        return complete;
    }

    //public IEnumerator SpawnWave(TransformAnchor _playerAnchor)
    //{
    //    currentEnemies = enemiesToSpawn.Length;
    //    _delayCompleteTime = Time.time + waveCompleteDelay;
    //    if (nextWaveSpawnTrigger != null)
    //        nextWaveSpawnTrigger
    //            .playerEnteredTrigger
    //            .AddListener(PlayerEnteredTrigger);

    //    yield return new WaitForSeconds(waveInitialSpawnDelay);

    //    Vector3[] spawnLocation = new Vector3[enemiesToSpawn.Length];

    //    for (int i = 0; i < enemiesToSpawn.Length; i++)
    //    {

    //        //while (Enemy.enemyAlive >= 3)
    //        //{
    //        //    yield return null;
    //        //}


    //        if (_playerAnchor && _playerAnchor.Value != null)
    //            spawnLocation[i] = PickRandomSpawnPoint(_playerAnchor.Value.position);
    //        else 
    //            spawnLocation[i] = PickRandomSpawnPoint(Vector3.zero);
    //        GameObject.Instantiate(enemySpawnIndicator, spawnLocation[i], Quaternion.identity);
    //    }
    //    yield return new WaitForSeconds(individualSpawnDelay);

    //    while (Enemy.enemyAlive >= 3)
    //    {
    //        yield return null;
    //    }

    //    for (int e = 0; e < enemiesToSpawn.Length; e++)
    //    {
    //        Enemy enemy = GameObject.Instantiate(enemiesToSpawn[e], spawnLocation[e], Quaternion.identity).GetComponent<Enemy>();
    //        enemy.OnEnemyDied.AddListener(EnemyKilled);
    //    }
    //}
    //public IEnumerator SpawnWave(TransformAnchor _playerAnchor)
    //{
    //    currentEnemies = enemiesToSpawn.Length;
    //    _delayCompleteTime = Time.time + waveCompleteDelay;
    //    if (nextWaveSpawnTrigger != null)
    //        nextWaveSpawnTrigger
    //            .playerEnteredTrigger
    //            .AddListener(PlayerEnteredTrigger);

    //    yield return new WaitForSeconds(waveInitialSpawnDelay);

    //    Vector3[] spawnLocation = new Vector3[enemiesToSpawn.Length];

    //    for (int i = 0; i < enemiesToSpawn.Length; i++)
    //    {
    //        while (Enemy.enemyAlive >= 3)
    //        {
    //            yield return null;
    //        }

    //        if (_playerAnchor && _playerAnchor.Value != null)
    //            spawnLocation[i] = PickRandomSpawnPoint(_playerAnchor.Value.position);
    //        else
    //            spawnLocation[i] = PickRandomSpawnPoint(Vector3.zero);
    //        GameObject.Instantiate(enemySpawnIndicator, spawnLocation[i], Quaternion.identity);
    //        yield return new WaitForSeconds(individualSpawnDelay);

    //        // Ensure the condition is checked here as well.
    //        while (Enemy.enemyAlive >= 3)
    //        {
    //            yield return null;
    //        }

    //        Enemy enemy = GameObject.Instantiate(enemiesToSpawn[i], spawnLocation[i], Quaternion.identity).GetComponent<Enemy>();
    //        enemy.OnEnemyDied.AddListener(EnemyKilled);
    //    }
    //}

    //public IEnumerator SpawnWave(TransformAnchor _playerAnchor)
    //{
    //    currentEnemies = enemiesToSpawn.Length;
    //    _delayCompleteTime = Time.time + waveCompleteDelay;
    //    if (nextWaveSpawnTrigger != null)
    //        nextWaveSpawnTrigger
    //            .playerEnteredTrigger
    //            .AddListener(PlayerEnteredTrigger);

    //    yield return new WaitForSeconds(waveInitialSpawnDelay);

    //    Vector3[] spawnLocation = new Vector3[enemiesToSpawn.Length];

    //    for (int i = 0; i < enemiesToSpawn.Length; i++)
    //    {
    //        while (Enemy.enemyAlive >= 1)
    //        {
    //            yield return null; // Wait until the number of alive enemies is less than 3
    //        }

    //        if (_playerAnchor && _playerAnchor.Value != null)
    //            spawnLocation[i] = PickRandomSpawnPoint(_playerAnchor.Value.position);
    //        else
    //            spawnLocation[i] = PickRandomSpawnPoint(Vector3.zero);

    //        // Instantiate the indicator immediately before spawning the enemy.
    //        GameObject.Instantiate(enemySpawnIndicator, spawnLocation[i], Quaternion.identity);
    //        yield return new WaitForSeconds(individualSpawnDelay);

    //        // Now spawn the enemy
    //        Enemy enemy = GameObject.Instantiate(enemiesToSpawn[i], spawnLocation[i], Quaternion.identity).GetComponent<Enemy>();
    //        enemy.OnEnemyDied.AddListener(EnemyKilled);
    //    }
    //}
    public IEnumerator SpawnWave(TransformAnchor _playerAnchor)
    {
        currentEnemies = enemiesToSpawn.Length;
        _delayCompleteTime = Time.time + waveCompleteDelay;
        if (nextWaveSpawnTrigger != null)
            nextWaveSpawnTrigger
                .playerEnteredTrigger
                .AddListener(PlayerEnteredTrigger);

        yield return new WaitForSeconds(waveInitialSpawnDelay);

        Vector3[] spawnLocation = new Vector3[enemiesToSpawn.Length];

        int initialSpawnCount = Mathf.Min(25, enemiesToSpawn.Length); // Spawn up to 3 enemies initially

        // Initial spawn of up to 3 enemies
        for (int i = 0; i < initialSpawnCount; i++)
        {
            if (_playerAnchor && _playerAnchor.Value != null)
                spawnLocation[i] = PickRandomSpawnPoint(_playerAnchor.Value.position);
            else
                spawnLocation[i] = PickRandomSpawnPoint(Vector3.zero);

            GameObject.Instantiate(enemySpawnIndicator, spawnLocation[i] + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        yield return new WaitForSeconds(individualSpawnDelay);

        for (int i = 0; i < initialSpawnCount; i++)
        {
            Enemy enemy = GameObject.Instantiate(enemiesToSpawn[i], spawnLocation[i], Quaternion.identity).GetComponent<Enemy>();
            enemy.OnEnemyDied.AddListener(EnemyKilled);
            Enemy.enemyAlive++;
        }

        // Continue spawning remaining enemies one by one
        for (int i = initialSpawnCount; i < enemiesToSpawn.Length; i++)
        {
            while (Enemy.enemyAlive >= 25)
            {
                yield return null; // Wait until the number of alive enemies is less than 3
            }

            if (_playerAnchor && _playerAnchor.Value != null)
                spawnLocation[i] = PickRandomSpawnPoint(_playerAnchor.Value.position);
            else
                spawnLocation[i] = PickRandomSpawnPoint(Vector3.zero);

            GameObject.Instantiate(enemySpawnIndicator, spawnLocation[i]+ new Vector3(0, 0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(individualSpawnDelay);

            Enemy enemy = GameObject.Instantiate(enemiesToSpawn[i], spawnLocation[i], Quaternion.identity).GetComponent<Enemy>();
            enemy.OnEnemyDied.AddListener(EnemyKilled);
            Enemy.enemyAlive++;
        }
    }

    private void PlayerEnteredTrigger(Collider col)
    {
        triggeredNextWave = true;
    }

    private void EnemyKilled(Enemy enemy)
    {
        if (currentEnemies - 1 <= 0)
            currentEnemies = 0;
        else
            currentEnemies -= 1;
        Enemy.enemyAlive--;
    }

    private Vector3 PickRandomSpawnPoint(Vector3 center)
    {
        //TODO: Replace with spawn on navmesh
        //TODO: Replace navmesh with grid system
        Vector3 randomPoint = new Vector3(Random.Range(center.x-spawnRadius, center.x+spawnRadius), 0, Random.Range(center.z-spawnRadius, center.z+spawnRadius));
        GraphNode nearestNode = AstarPath.active.GetNearest(randomPoint, NNConstraint.Default).node;
        Vector3 spawnPosition = Vector3.zero;
        bool isValidPosition = false;
        while (isValidPosition == false)
        {
            if (nearestNode.Walkable)
            {
                spawnPosition = (Vector3)nearestNode.position;
                isValidPosition = true;
            }
        }
        return spawnPosition + new Vector3(0, 1, 0);
    }
}

public enum WaveTriggerType
{
    Timer,
    EliminateLastWave,
    TriggerCollider
}

public enum WaveSpawnType
{
    All,
    OneByOne,
    Random
}
