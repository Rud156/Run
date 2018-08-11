using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn GameObjects")]
    public List<Transform> spawnPoints;
    public GameObject spawnEffect;
    public GameObject enemy;
    public GameObject spawnerHolder;

    [Header("Spawn Stats")]
    public float waitBetweenSpawn = 5f;
    public float waitBeteweenEffectAndSpawn = 0.1f;

    [Header("Debug")]
    public bool spawnOnStart;

    private Coroutine coroutine;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (spawnOnStart)
            StartSpawn();
    }

    public void StartSpawn() =>
        coroutine = StartCoroutine(SpawnEnemies());

    public void StopSpawn() =>
        StopCoroutine(coroutine);

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int randomNumber = Random.Range(0, 1000);
            int randomIndex = randomNumber % spawnPoints.Count;

            Vector3 spawnPosition = spawnPoints[randomIndex].position;
            Instantiate(spawnEffect, spawnPosition, spawnEffect.transform.rotation);

            yield return new WaitForSeconds(waitBeteweenEffectAndSpawn);

            GameObject enemyInstance = Instantiate(enemy, spawnPosition, enemy.transform.rotation);
            enemyInstance.transform.SetParent(spawnerHolder.transform);

            yield return new WaitForSeconds(waitBetweenSpawn);
        }
    }
}
