using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 positionOffset;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Vector3 targetPosition = playerTransform.position +
            playerTransform.forward * positionOffset.z +
            playerTransform.right * positionOffset.x +
            playerTransform.up * positionOffset.y;

        transform.position = targetPosition;
    }
}
