using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy_shooterPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _multiShot;
    
    private bool _stopSpawning = false;

    private UIManager _uiManager;

    [SerializeField]
    private TMP_Text _waveCountText;
    //private int waveCount;
    //private float waveTextTimer = 1.0f;

    [SerializeField]
    EnemyWaves[] _waves;// private GameObject[] enemyTypeToSpawn;
    public int _enemyCount = 0;// enemies remaining
    private int _currentWave;
    private bool _isSpawnActive = true; //_canSpawn


   // [SerializeField]
   // private float _spawnRate;
    //[SerializeField]
    // [SerializeField]
   //private float timesBetweenWaves = 2.0f;
   // private bool _isNextWaveActive = false;
   //private bool _isEnemySpawn = false;

    [System.Serializable]
    public class EnemyWaves
    {   
        public GameObject[] enemyTypeToSpawn;
        public string name;
        public int enemyCountPerWave;
        public float nextWave = 5f;
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnMultiPowerUpRoutine());
        _uiManager = GameObject.Find("Game_Manager").GetComponent<UIManager>();
      //  if (_uiManager == null)
      //  {
       //     Debug.LogError("The UI Manager is NULL.");
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events
    //While loop
    //  IEnumerator SpawnEnemyRoutine()
    //  {
    //  yield return new WaitForSeconds(3.5f);

    //  while (_stopSpawning == false)
    //  {
    //      Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
    //      GameObject newEnemy = Instantiate(_enemy_shooterPrefab, posToSpawn, Quaternion.identity);
    //      newEnemy.transform.parent = _enemyContainer.transform;
    //      yield return new WaitForSeconds(5.0f);
    //   }
    // while loop (infinite loop)
    //Instantiate enemy prefab
    //yield wait for 5 seconds

    // }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (_isSpawnActive == true)
        {
            for (int i = _waves.Length - 1; i >= 0; i--)
            {
                _currentWave = i;
                _enemyCount = _waves[i].enemyCountPerWave;
               // UpdateWaveCountRoutine();

                while (_enemyCount > 0)
                {
                    if (_waves[i].enemyCountPerWave > 0)
                    {
                        Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
                        GameObject newEnemy = Instantiate(_enemy_shooterPrefab, posToSpawn, Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        _waves[i].enemyCountPerWave--;
                    }

                    yield return new WaitForSeconds(_waves[i].nextWave);
                }

                if (_currentWave == 0 && _enemyCount == 0)
                {
                    _uiManager.YouWin();
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
    // while loop (infinite loop)
    //Instantiate enemy prefab
    //yield wait for 5 seconds

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.5f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            int randomPowerUp = Random.Range(0, 6); //increase max random range
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 4));
            // every 3-6 seconds, spawn in a powerup
        }
    }
        IEnumerator SpawnMultiPowerUpRoutine()
        {
            yield return new WaitForSeconds(20f);
            while (_stopSpawning == false)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
                int multiPowerUp = Random.Range(5, 6); //increase max random range
                Instantiate(powerups[multiPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(20, 40));
            }
        }

        public void OnPlayerDeath()
        {
            _stopSpawning = true;
        }
}
