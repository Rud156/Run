using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public abstract class BaseDamageAndDeathController : MonoBehaviour
{
    [Header("Police Car Stats")]
    public float maxCarHealth = 30;
    public GameObject destroyEffect;
    public float maxHealthLostFromCollision = 10;
    public float maxDamagePossible;

    [Header("Vehicle Fire")]
    public float healthRatioToSpawnFire = 0.75f;
    public int minFireParticles = 30;
    public int maxFireParticles = 100;
    public GameObject vehicleFireSpawnPoint;
    public GameObject vehicleFire;

    [Header("Camera Shaker")]
    public float magnitude = 5;
    public float roughness = 5;
    public float fadeInTime = 0.15f;
    public float fadeOutTime = 0.15f;

    protected float currentCarHealth;
    protected List<ParticleSystem> vehicleFireParticleSystem;
    protected Rigidbody vehicleRB;
    protected float healthLostCurrentFrame;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public void Init()
    {
        currentCarHealth = maxCarHealth;
        vehicleFireParticleSystem = new List<ParticleSystem>();
        vehicleRB = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    public void CheckSolidCollision(Collision other)
    {
        float maxValue = Mathf.Max(other.relativeVelocity.magnitude, maxDamagePossible);
        healthLostCurrentFrame = (other.relativeVelocity.magnitude / maxValue) * maxHealthLostFromCollision;

        currentCarHealth -= healthLostCurrentFrame;
    }

    public void UpdateHealth()
    {
        float currentHealthRatio = currentCarHealth / maxCarHealth;
        if (currentHealthRatio >= healthRatioToSpawnFire)
            return;

        if (vehicleFireParticleSystem.Count == 0)
        {
            GameObject vehicleFireInstance = Instantiate(vehicleFire,
                vehicleFireSpawnPoint.transform.position,
                vehicleFire.transform.rotation);
            vehicleFireInstance.transform.SetParent(gameObject.transform);

            vehicleFireParticleSystem.Add(vehicleFireInstance.
                transform.GetChild(0).GetComponent<ParticleSystem>());
            vehicleFireParticleSystem.Add(vehicleFireInstance.
                transform.GetChild(1).GetComponent<ParticleSystem>());
        }

        foreach (ParticleSystem item in vehicleFireParticleSystem)
        {
            float mappedEmission = ExtensionFunctions.Map(currentHealthRatio,
                healthRatioToSpawnFire, 0,
                minFireParticles, maxFireParticles);

            ParticleSystem.EmissionModule emission = item.emission;
            emission.rateOverTime = mappedEmission;
        }
    }

    public void IncreaseHealth(float healthAmount)
    {
        if (currentCarHealth + healthAmount >= maxCarHealth)
            currentCarHealth = maxCarHealth;
        else
            currentCarHealth += healthAmount;
    }

    public void ReduceHealth(float healthAmount) =>
        currentCarHealth -= healthAmount;

    public void CheckHealthZero()
    {
        if (currentCarHealth <= 0)
        {
            Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            Destroy(gameObject);
        }
    }
}
