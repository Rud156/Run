using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceCarDamageAndDeathController : MonoBehaviour
{

    [Header("Police Car Stats")]
    public float maxPoliceCarHealth = 30;
    public GameObject destroyEffect;
    public float maxHealthLostFromCollision = 10;
    public float maxDamagePossible;

    [Header("Vehicle Fire")]
    public float healthRatioToSpawnFire = 0.75f;
    public int minFireParticles = 30;
    public int maxFireParticles = 100;
    public GameObject vehicleFireSpawnPoint;
    public GameObject vehicleFire;

    private float currentPoliceCarHealth;
    private List<ParticleSystem> vehicleFireParticleSystem;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentPoliceCarHealth = maxPoliceCarHealth;
        vehicleFireParticleSystem = new List<ParticleSystem>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        UpdateHealth();
        CheckHealthZero();
    }

    /// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
    {
        float maxValue = Mathf.Max(other.relativeVelocity.magnitude, maxDamagePossible);
        float totalHealthLost = (other.relativeVelocity.magnitude / maxValue) * maxHealthLostFromCollision;

        currentPoliceCarHealth -= totalHealthLost;
    }

    private void UpdateHealth()
    {
        float currentHealthRatio = currentPoliceCarHealth / maxPoliceCarHealth;
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

    private void CheckHealthZero()
    {
        if (currentPoliceCarHealth <= 0)
        {
            Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
            Destroy(gameObject);
        }
    }
}
