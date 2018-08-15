using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class SpawnExplosionAroundPlayer : MonoBehaviour
{
    [Header("General Stats")]
    public GameObject groundExplosionEffect;
    public float heightAboveGroundToSpawn = 0.2f;
    public int totalGroundEffectsToSpawn = 10;
    public int radius = 10;

    [Header("Explosion Stats")]
    public float affectRadius = 5f;
    public float damagePower = 10f;

    [Header("Enemy Damage Stats")]
    public float damageAmount = 20;

    [Header("Player")]
    public Transform playerTransform;

    [Header("Camera Shaker")]
    public float magnitude = 10;
    public float roughness = 10;
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.3f;

    public void SpawnEffectAroundPlayer()
    {
        List<Vector3> positions = GetPointsAroundPlayer();

        foreach (var position in positions)
        {
            Instantiate(groundExplosionEffect,
                new Vector3(position.x, position.y, position.z),
                groundExplosionEffect.transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(position, affectRadius);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (!rb || !rb.CompareTag(TagManager.Enemy))
                    continue;

                rb.AddExplosionForce(damagePower, position, affectRadius, 3f, ForceMode.Impulse);
                rb.GetComponent<PoliceCarDamageAndDeathController>().ReduceHealth(damageAmount);
            }
        }

        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }

    private List<Vector3> GetPointsAroundPlayer()
    {
        float currentRadius = radius;
        Vector3 centerPoint = playerTransform.position;
        float angleDiv = 360 / totalGroundEffectsToSpawn;

        List<Vector3> spawnPoints = new List<Vector3>();

        for (float i = 0; i <= 360; i += angleDiv)
        {
            float x = centerPoint.x + currentRadius * Mathf.Cos(i);
            float z = centerPoint.z + currentRadius * Mathf.Sin(i);
            spawnPoints.Add(new Vector3(x, centerPoint.y + heightAboveGroundToSpawn, z));
        }

        return spawnPoints;
    }
}
