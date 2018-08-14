using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody))]
public class DestroyProjectile : MonoBehaviour
{
    public GameObject destoryEffect;
    [Header("Camera Shaker")]
    public float magnitude = 3;
    public float roughness = 5;
    public float fadeInTime = 0.1f;
    public float fadeOutTime = 0.1f;


    public float damageAmount;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Instantiate(destoryEffect, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        Destroy(gameObject);
    }
}
