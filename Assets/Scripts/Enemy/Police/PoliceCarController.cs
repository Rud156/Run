using System.Collections;
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
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.Player);
        if (player != null)
            target = player.transform;
    }

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

        if (target)
            enemyAgent.SetDestination(target.position);
        else
            enemyAgent.ResetPath();

    }

    public override void GetInput()
    {
        Vector3 navMeshDesiredVelocity = enemyAgent.desiredVelocity;
        Vector3 localSpaceVelocity = transform.InverseTransformDirection(navMeshDesiredVelocity);

        horizontalInput = localSpaceVelocity.normalized.x;
        verticalInput = localSpaceVelocity.normalized.z;
    }
}
