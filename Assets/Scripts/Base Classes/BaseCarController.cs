using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCarController : MonoBehaviour
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

    [Header("Movement Stats")]
    public float maxSteerAngle = 30;
    public float motorForce = 50;

    public abstract void GetInput();
    public void Steer(float horizontalInput)
    {
        float steerAngle = maxSteerAngle * horizontalInput;
        frontLWheelCollider.steerAngle = steerAngle;
        frontRWheelCollider.steerAngle = steerAngle;
    }
    public void Accelerate(float verticalInput)
    {
        frontLWheelCollider.motorTorque = verticalInput * motorForce;
        frontRWheelCollider.motorTorque = verticalInput * motorForce;

        rearLWheelCollider.motorTorque = verticalInput * motorForce;
        rearRWheelCollider.motorTorque = verticalInput * motorForce;
    }

    public void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLWheelCollider, frontLWheel);
        UpdateWheelPose(frontRWheelCollider, frontRWheel);
        UpdateWheelPose(rearLWheelCollider, rearLWheel);
        UpdateWheelPose(rearRWheelCollider, rearRWheel);
    }

    public void UpdateWheelPose(WheelCollider currentCollider, Transform currentTransform)
    {
        Vector3 position = currentTransform.position;
        Quaternion rotation = currentTransform.rotation;

        currentCollider.GetWorldPose(out position, out rotation);

        currentTransform.position = position;
        currentTransform.rotation = rotation;
    }
}
