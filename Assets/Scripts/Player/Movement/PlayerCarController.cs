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

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        GetInput();

        base.Steer(horizontalInput);
        base.Accelerate(verticalInput);

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
        DisplayTrail(frontLTrail);
        DisplayTrail(frontRTrail);
        DisplayTrail(rearLTrail);
        DisplayTrail(rearRTrail);
    }

    private void DisplayTrail(ParticleSystem trail)
    {
        ParticleSystem.EmissionModule emission = trail.emission;
        emission.rateOverTime = Mathf.FloorToInt(verticalInput * maxParticlesToSpawn);
    }
}
