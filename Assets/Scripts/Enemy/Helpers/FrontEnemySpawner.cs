using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEnemySpawner : MonoBehaviour
{
    public GameObject enemyHolder;

    [Header("Spawner Data")]
    public GameObject enemy;
    public float minSpawnTime;
    public float maxSpawnTime;
    public GameObject spawnPoint;
    public GameObject spawnEffect;

    private Coroutine coroutine;

    public void StartSpawn() => coroutine = StartCoroutine(SpawnEnemy());

    public void StopSpawn() => StopCoroutine(coroutine);

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 position = spawnPoint.transform.position;

            Instantiate(spawnEffect, new Vector3(position.x, 1, position.z),
                spawnEffect.transform.rotation);
            GameObject enemyInstance = Instantiate(enemy, position, enemy.transform.rotation);
            enemyInstance.transform.SetParent(enemyHolder.transform);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
