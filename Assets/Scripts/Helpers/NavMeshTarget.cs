﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshTarget : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start() =>
        agent = GetComponent<NavMeshAgent>();

    // Update is called once per frame
    void FixedUpdate() =>
        agent.SetDestination(target.position);
}
