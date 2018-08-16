using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageAndDeathController : BaseDamageAndDeathController
{
    [Header("Health Display")]
    public Color minHealthColor = Color.red;
    public Color halfHealthColor = Color.yellow;
    public Color maxHealthColor = Color.green;
    public Slider healthSlider;
    public Image healthFiller;

    [Header("Audio Display")]
    public AudioSource vehicleDamage;
    public float maxAudioPitch = 3;
    public float minAudioPitch = 0;

    [Header("Damage Effect")]
    public float thresholdBeforeSwitching;
    public float waitTimeBetweenSwitching;
    public Material damageMaterial;
    public Material originalMaterial;
    public Renderer vehicleBody;

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

    private void PlayAudio(Collision other)
    {
        float maxDamage = Mathf.Max(other.relativeVelocity.magnitude, base.maxDamagePossible);
        float damagePitch = ((other.relativeVelocity.magnitude / maxDamage) * maxAudioPitch) +
            minAudioPitch;

        print(damagePitch);

        vehicleDamage.pitch = damagePitch;
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
