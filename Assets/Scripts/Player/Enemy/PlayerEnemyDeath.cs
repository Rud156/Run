using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDeath : MonoBehaviour
{
    [Header("Death Effects")]
    public GameObject gameOverMenu;
    public GameObject deathEffect;

    [Header("UI Content")]
    public GameObject gameOverHolder;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagManager.Enemy))
        {
            Instantiate(deathEffect, gameObject.transform.position, deathEffect.transform.rotation);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            gameOverHolder.SetActive(true);
        }
    }

}
