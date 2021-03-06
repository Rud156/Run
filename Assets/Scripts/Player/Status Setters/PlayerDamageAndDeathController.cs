﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageAndDeathController : BaseDamageAndDeathController
{
    public GameObject directionArrow;

    [Header("Health Display")]
    public Color minHealthColor = Color.red;
    public Color halfHealthColor = Color.yellow;
    public Color maxHealthColor = Color.green;
    public Slider healthSlider;
    public Image healthFiller;

    [Header("Audio Display")]
    public AudioSource vehicleDamage;
    public float maxAudioVolume = 0.9f;
    public float minAudioVolume = 0.5f;

    [Header("Damage Effect")]
    public float thresholdBeforeSwitching;
    public float waitTimeBetweenSwitching;
    public Material damageMaterial;
    public Material originalMaterial;
    public Renderer vehicleBody;

    private PlayerIncreaseScoreController increaseScoreController;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        base.Init();
        increaseScoreController = GetComponent<PlayerIncreaseScoreController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        base.UpdateHealth();

        CheckHealthZero();
        UpdateHealthToUI();
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
            PlayAudio(other);
            yield return new WaitForSeconds(waitTimeBetweenSwitching);
            vehicleBody.material = originalMaterial;
        }
    }

    private new void CheckHealthZero()
    {
        if (base.currentCarHealth <= 0)
        {
            increaseScoreController.SendScoreToSceneTrigger(false);
            Destroy(directionArrow);
            base.CheckHealthZero();
        }
    }

    private void PlayAudio(Collision other)
    {
        float maxVolume = Mathf.Max(other.relativeVelocity.magnitude, base.maxDamagePossible);
        float damageVolume = ((other.relativeVelocity.magnitude / maxVolume) * maxAudioVolume) +
            minAudioVolume;

        vehicleDamage.volume = damageVolume;
        vehicleDamage.Play();
    }

    private void UpdateHealthToUI()
    {
        float maxHealth = base.maxCarHealth;
        float currentHealthLeft = base.currentCarHealth;
        float healthRatio = currentHealthLeft / maxHealth;

        if (healthRatio <= 0.5)
            healthFiller.color = Color.Lerp(minHealthColor, halfHealthColor, healthRatio * 2);
        else
            healthFiller.color = Color.Lerp(halfHealthColor, maxHealthColor, (healthRatio - 0.5f) * 2);
        healthSlider.value = healthRatio;
    }
}
