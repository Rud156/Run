using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIncreaseScoreTrigger : MonoBehaviour
{
    public PlayerIncreaseScoreController scoreController;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRB = other.GetComponent<Rigidbody>();
        if (!playerRB || !other.CompareTag(TagManager.Player))
            return;

        scoreController.GenerateRandomScore(transform.position);
    }
}
