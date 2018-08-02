using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEnemySpawner : MonoBehaviour
{
    [Header("Spawner Data")]
    public GameObject enemy;
    public float minSpawnTime;
    public float maxSpawnTime;
    public GameObject spawnPoint;

    private Coroutine coroutine;

    // Use this for initialization
    void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        coroutine = StartCoroutine(SpawnEnemy());
    }

    public void StopSpawn()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 position = spawnPoint.transform.position;
            Instantiate(enemy, position, enemy.transform.rotation);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
