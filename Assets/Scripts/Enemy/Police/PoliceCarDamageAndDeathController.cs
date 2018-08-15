using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody))]
public class PoliceCarDamageAndDeathController : BaseDamageAndDeathController
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => base.Init();

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        base.UpdateHealth();
        base.CheckHealthZero();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) => base.CheckSolidCollision(other);

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.Bullet))
        {
            float damageAmount = other.GetComponent<DestroyProjectile>().damageAmount;

            vehicleRB.AddExplosionForce(damageAmount, other.transform.position, 3f, 3f, ForceMode.Impulse);
            base.currentCarHealth -= damageAmount;
        }
    }

    public void ReduceHealth(float healthAmount) =>
        base.currentCarHealth -= healthAmount;
}
