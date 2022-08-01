using System.Collections;
using System.Collections.Generic;
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
    GameObject _multiShot;

    private bool _stopSpawning = false;

 

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnMultiPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events
    //While loop
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.5f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemy_shooterPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
          // while loop (infinite loop)
        //Instantiate enemy prefab
        //yield wait for 5 seconds

    }
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
                int multiShotRandom = Random.Range(5, 6); //increase max random range
                Instantiate(powerups[multiShotRandom], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(20f, 40f)); 
            }
        }

        public void OnPlayerDeath()
        {
        _stopSpawning = true;
        }
}
