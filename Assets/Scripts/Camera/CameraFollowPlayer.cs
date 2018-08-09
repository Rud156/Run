using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Base Stats")]
    public float followSpeed = 10f;
    public float lookSpeed = 10f;
    public Vector3 cameraOffset;

    [Header("Player")]
    public Transform player;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = player.position +
            player.forward * cameraOffset.z +
            player.right * cameraOffset.x +
            player.up * cameraOffset.y;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
