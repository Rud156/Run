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

    [Header("Scene Changer")]
    public ChangeSceneOnTrigger changeSceneOnTrigger;

    private Vector3 lastPlayerPosition;
    private bool nextSceneInvoked;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lastPlayerPosition = player.position;
        nextSceneInvoked = false;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();

        UpdateLastPlayerPosition();
    }

    private void LookAtTarget()
    {
        Vector3 targetPosition = player != null ? player.position : lastPlayerPosition;

        Vector3 lookDirection = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    private void MoveToTarget()
    {
        // Player is NULL only when destroyed
        Vector3 targetVector = lastPlayerPosition;
        Vector3 forwardVector = Vector3.forward;
        Vector3 rightVector = Vector3.right;
        Vector3 upVector = Vector3.up;

        if (player != null)
        {
            targetVector = player.position;
            forwardVector = player.forward;
            rightVector = player.right;
            upVector = player.up;
        }

        if (player == null && !nextSceneInvoked)
        {
            nextSceneInvoked = true;
            changeSceneOnTrigger.ChangeSceneInvoke();
        }

        Vector3 targetPosition = targetVector +
            forwardVector * cameraOffset.z +
            rightVector * cameraOffset.x +
            upVector * cameraOffset.y;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void UpdateLastPlayerPosition()
    {
        if (player != null)
            lastPlayerPosition = player.position;
    }
}
