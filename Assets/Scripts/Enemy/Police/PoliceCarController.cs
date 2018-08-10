﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : BaseCarController
{
    [Header("Nav Mesh")]
    public NavMeshAgent enemyAgent;
    public GameObject navMeshObject;

    private Transform target;

    private float horizontalInput;
    private float verticalInput;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() =>
        target = GameObject.FindGameObjectWithTag(TagManager.Player).transform;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        UpdateNavMesh();
        GetInput();

        base.Steer(horizontalInput);
        base.Accelerate(verticalInput);

        base.UpdateWheelPoses();
    }

    private void UpdateNavMesh()
    {
        navMeshObject.transform.localPosition = Vector3.zero;
        navMeshObject.transform.localRotation = Quaternion.identity;

        enemyAgent.SetDestination(target.position);
    }

    public override void GetInput()
    {
        Vector3 navMeshDesiredVelocity = enemyAgent.desiredVelocity;
        Vector3 localSpaceVelocity = transform.InverseTransformDirection(navMeshDesiredVelocity);

        horizontalInput = localSpaceVelocity.normalized.x;
        verticalInput = localSpaceVelocity.normalized.z;
    }
}