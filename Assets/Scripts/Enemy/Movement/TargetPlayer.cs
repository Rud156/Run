using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour
{

    private GameObject player;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.Player);
        agent = gameObject.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
