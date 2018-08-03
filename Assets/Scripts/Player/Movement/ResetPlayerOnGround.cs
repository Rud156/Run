using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerOnGround : MonoBehaviour
{
    public GameObject gameOverHolder;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRB = other.GetComponent<Rigidbody>();
        if (!playerRB || !other.CompareTag(TagManager.Player))
            return;

        other.GetComponent<MeshRenderer>().enabled = false;
        other.GetComponent<CapsuleCollider>().enabled = false;

        gameOverHolder.SetActive(true);
    }

}
