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
        if (PlayerData.movePlayer)
        {
            float yVelocity = playerRB.velocity.y;
            Vector3 velocity = gameObject.transform.forward * movementSpeed * Time.deltaTime;

            playerRB.velocity = new Vector3(velocity.x, yVelocity, velocity.z);
        }
        else
            playerRB.velocity = Vector3.zero;
    }
}
