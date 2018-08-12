using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageAndDeathController : BaseDamageAndDeathController
{
    [Header("Damage Display")]
    public float thresholdBeforeSwitching;
    public float waitTimeBetweenSwitching;
    public Material damageMaterial;
    public Material originalMaterial;
    public Renderer vehicleBody;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => Init();

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
    IEnumerator OnCollisionEnter(Collision other)
    {
        base.CheckSolidCollision(other);

        if (healthLostCurrentFrame > thresholdBeforeSwitching)
        {
            vehicleBody.material = damageMaterial;
            yield return new WaitForSeconds(waitTimeBetweenSwitching);
            vehicleBody.material = originalMaterial;
        }
    }
}
