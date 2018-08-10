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

    [Header("Police Car Health")]
    public Color minHealthColor = Color.red;
    public Color halfHealthColor = Color.yellow;
    public Color maxHealthColor = Color.green;
    public Slider policeCarHealthSlider;
    public Image policeCarHealthFiller;

    private float currentPoliceCarHealth;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => currentPoliceCarHealth = maxPoliceCarHealth;

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
        float maxHealth = maxPoliceCarHealth;
        float currentHealthLeft = currentPoliceCarHealth;
        float healthRatio = currentHealthLeft / maxHealth;

        if (healthRatio <= 0.5)
            policeCarHealthFiller.color = Color.Lerp(minHealthColor, halfHealthColor, healthRatio * 2);
        else
            policeCarHealthFiller.color = Color.Lerp(halfHealthColor, maxHealthColor, (healthRatio - 0.5f) * 2);
        policeCarHealthSlider.value = healthRatio;
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
