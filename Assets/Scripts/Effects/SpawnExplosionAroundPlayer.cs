using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class SpawnExplosionAroundPlayer : MonoBehaviour
{
    [Header("Effect Stats")]
    public int totalGroundEffectsToSpawn = 10;
    public int spawnRadius = 10;

    [Header("Meteor")]
    public float heightAbovePlayerToSpawn = 50f;
    public GameObject meteor;

    [Header("Player")]
    public Transform playerTransform;

    public void SpawnEffectAroundPlayer()
    {
        List<Vector3> positions = GetPointsAroundPlayer();

        foreach (var position in positions)
            Instantiate(meteor, position, Quaternion.identity);
    }

    private List<Vector3> GetPointsAroundPlayer()
    {
        float currentRadius = spawnRadius;
        Vector3 centerPoint = playerTransform.position;
        float angleDiv = 360 / totalGroundEffectsToSpawn;

        List<Vector3> spawnPoints = new List<Vector3>();

        for (float i = 0; i <= 360; i += angleDiv)
        {
            float x = centerPoint.x + currentRadius * Mathf.Cos(i);
            float z = centerPoint.z + currentRadius * Mathf.Sin(i);
            spawnPoints.Add(new Vector3(x, centerPoint.y + heightAbovePlayerToSpawn, z));
        }

        return spawnPoints;
    }
}
