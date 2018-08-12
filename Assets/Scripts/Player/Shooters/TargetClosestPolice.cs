using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClosestPolice : MonoBehaviour
{
    public GameObject policeHolder;

    [Header("Projectile")]
    public GameObject projectile;
    public GameObject projectileShooter;
    public GameObject projectileLaunchPoint;
    public float projectileLaunchSpeed;

    [Header("Shooter Stats")]
    public float waitBetweenShots;
    public float minDistanceBeforeShooting;
    public GameObject shotEffect;

    private Coroutine coroutine;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => StartShooting();

    public void StartShooting() => coroutine = StartCoroutine(Shoot());

    public void StopShooting() => StopCoroutine(coroutine);

    IEnumerator Shoot()
    {
        while (true)
        {
            int totalPoliceVehicles = policeHolder.transform.childCount;
            float shortestDistance = minDistanceBeforeShooting;
            GameObject nearestPolice = null;

            for (int i = 0; i < totalPoliceVehicles; i++)
            {
                float distanceToPolice = Vector3.Distance(projectileLaunchPoint.transform.position,
                    policeHolder.transform.GetChild(i).position);

                if (distanceToPolice <= shortestDistance)
                {
                    shortestDistance = distanceToPolice;
                    nearestPolice = policeHolder.transform.GetChild(i).gameObject;
                }
            }

            if (nearestPolice != null)
            {
                Vector3 direction = nearestPolice.transform.position -
                    projectileLaunchPoint.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                projectileShooter.transform.rotation = lookRotation;

                GameObject shotEffectInstance = Instantiate(shotEffect,
                    projectileLaunchPoint.transform.position, Quaternion.identity);
                shotEffectInstance.transform.rotation = lookRotation;

                GameObject projectileInstance = Instantiate(projectile,
                    projectileLaunchPoint.transform.position, Quaternion.identity);
                projectileInstance.GetComponent<Rigidbody>().velocity =
                    projectileLaunchPoint.transform.forward * projectileLaunchSpeed;
            }

            yield return new WaitForSeconds(waitBetweenShots);
        }
    }
}
