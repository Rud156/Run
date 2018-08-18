using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerStuckUnstuckController : MonoBehaviour
{
    public float waitTime = 5f;

    private bool playerIsInBox;
    private float startTime;
    private Vector3 safePoint;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => playerIsInBox = false;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!playerIsInBox)
            return;

        if (Time.time > startTime + waitTime)
            ResetVehiclePosition();
    }

    public void ResetVehiclePosition()
    {
        transform.position = safePoint;
        transform.rotation = Quaternion.identity;
        SetPlayerExitedBox();
    }

    public void SetPlayerInBox(Vector3 safePoint)
    {
        playerIsInBox = true;
        startTime = Time.time;
        this.safePoint = safePoint;
    }

    public void SetPlayerExitedBox() => playerIsInBox = false;
}
