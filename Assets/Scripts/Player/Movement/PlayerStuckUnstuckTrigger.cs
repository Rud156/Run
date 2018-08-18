using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuckUnstuckTrigger : MonoBehaviour
{
    public PlayerStuckUnstuckController stuckUnstuckController;
    public Transform safePoint;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Rigidbody target = other.GetComponent<Rigidbody>();
        if (!target || !other.CompareTag(TagManager.Player))
            return;

        stuckUnstuckController.SetPlayerInBox(safePoint.position);
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        Rigidbody target = other.GetComponent<Rigidbody>();
        if (!target || !other.CompareTag(TagManager.Player))
            return;

        stuckUnstuckController.SetPlayerExitedBox();
    }
}
