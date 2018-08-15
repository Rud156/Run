using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class LaunchAndExplodeMeteor : MonoBehaviour
{
    [Header("Fall Stats")]
    public GameObject groundExplosionEffect;
    public float damageAmount = 30;
    public float affectRadius = 5;

    [Header("Camera Shake Stats")]
    public float magnitude = 5;
    public float roughness = 5;
    public float fadeInTime = 0.15f;
    public float fadeOutTime = 0.15f;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Instantiate(groundExplosionEffect, transform.position, groundExplosionEffect.transform.rotation);
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, affectRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (!rb)
                continue;

            if (collider.CompareTag(TagManager.Player))
                collider.GetComponent<PlayerDamageAndDeathController>().ReduceHealth(damageAmount);
            else if (collider.CompareTag(TagManager.Enemy))
                collider.GetComponent<PoliceCarDamageAndDeathController>().ReduceHealth(damageAmount);
        }

        Destroy(gameObject);
    }
}
