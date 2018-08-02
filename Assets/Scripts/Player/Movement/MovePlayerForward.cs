using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerForward : MonoBehaviour
{
    public float movementSpeed = 750;

    private Rigidbody playerRB;

    // Use this for initialization
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.velocity = gameObject.transform.forward * movementSpeed * Time.deltaTime;
    }
}
