﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Data")]
    public BoxCollider enclosingBoxCollider;
    public GameObject enemy;
    public float spawnTime;
    public GameObject spawnEffect;

    [Header("Player Data")]
    public GameObject player;
    public float distanceFromPlayer = 75f;

    private Coroutine coroutine;

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
            Vector3 randomPoint = new Vector3(
                Random.Range(-enclosingBoxCollider.bounds.extents.x, enclosingBoxCollider.bounds.extents.x),
                0,
                Random.Range(-enclosingBoxCollider.bounds.extents.z, enclosingBoxCollider.bounds.extents.z)
            ) + enclosingBoxCollider.bounds.center;

            float generatedDistanceFromPlayer = Vector3.Distance(randomPoint,
                new Vector3(player.transform.position.x, 0, player.transform.position.z));

            if (distanceFromPlayer > generatedDistanceFromPlayer)
            {
                yield return null;
                continue;
            }

            Instantiate(spawnEffect, new Vector3(randomPoint.x, 1, randomPoint.z),
                spawnEffect.transform.rotation);
            Instantiate(enemy, randomPoint, enemy.transform.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
