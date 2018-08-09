using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLWheelCollider;
    public WheelCollider frontRWheelCollider;
    public WheelCollider rearLWheelCollider;
    public WheelCollider rearRWheelCollider;

    [Header("Actual Wheel Objects")]
    public Transform frontLWheel;
    public Transform frontRWheel;
    public Transform rearLWheel;
    public Transform rearRWheel;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        GetInput();

        Steer();
        Accelerate();

        UpdateWheelPoses();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        frontLWheelCollider.steerAngle = steerAngle;
        frontRWheelCollider.steerAngle = steerAngle;
    }

    private void Accelerate()
    {
        frontLWheelCollider.motorTorque = verticalInput * motorForce;
        frontRWheelCollider.motorTorque = verticalInput * motorForce;

        rearLWheelCollider.motorTorque = verticalInput * motorForce;
        rearRWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLWheelCollider, frontLWheel);
        UpdateWheelPose(frontRWheelCollider, frontRWheel);
        UpdateWheelPose(rearLWheelCollider, rearLWheel);
        UpdateWheelPose(rearRWheelCollider, rearRWheel);
    }

    private void UpdateWheelPose(WheelCollider currentCollider, Transform currentTransform)
    {
        Vector3 position = currentTransform.position;
        Quaternion rotation = currentTransform.rotation;

        currentCollider.GetWorldPose(out position, out rotation);

        currentTransform.position = position;
        currentTransform.rotation = rotation;
    }
}
