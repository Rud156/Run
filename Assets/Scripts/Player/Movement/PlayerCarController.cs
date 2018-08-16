using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : BaseCarController
{
    [Header("Movement Trails")]
    public ParticleSystem frontLTrail;
    public ParticleSystem frontRTrail;
    public ParticleSystem rearLTrail;
    public ParticleSystem rearRTrail;

    [Header("Trails Stats")]
    public float maxParticlesToSpawn;

    private float horizontalInput;
    private float verticalInput;

    [HideInInspector]
    public bool disableDefaultControl;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => disableDefaultControl = true;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        GetInput();

        if (!disableDefaultControl)
        {
            base.Steer(horizontalInput);
            base.Accelerate(verticalInput);
        }

        base.UpdateWheelPoses();

        UpdateTrails();
    }

    public override void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void UpdateTrails()
    {
        DisplayTrail(frontLTrail, frontLWheelCollider);
        DisplayTrail(frontRTrail, frontRWheelCollider);
        DisplayTrail(rearLTrail, rearLWheelCollider);
        DisplayTrail(rearRTrail, rearRWheelCollider);
    }

    private void DisplayTrail(ParticleSystem trail, WheelCollider collider)
    {
        ParticleSystem.EmissionModule emission = trail.emission;

        if (collider.isGrounded)
            emission.rateOverTime = Mathf.FloorToInt(verticalInput * maxParticlesToSpawn);
        else
            emission.rateOverTime = 0;
    }
}
