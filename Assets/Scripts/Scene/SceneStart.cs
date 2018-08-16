using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{
    [Header("UI Display")]
    public Animator sceneStartAnimator;

    [Header("Player")]
    public PlayerCarController playerCarController;
    public WheelCollider frontLCollider;
    public WheelCollider frontRCollider;
    public WheelCollider rearLCollider;
    public WheelCollider rearRCollider;
    public float motorForce = 250;
    public float maxMovePlayerTime = 3f;

    private bool playerTouchedKey;
    private float currentTime;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        playerTouchedKey = false;
        currentTime = 0;

        sceneStartAnimator.SetTrigger(AnimatorVariables.DisplaySceneStartText);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() => MovePlayerOnStart();

    private void MovePlayerOnStart()
    {
        if (playerTouchedKey || currentTime >= maxMovePlayerTime)
        {
            playerTouchedKey = true;
            playerCarController.disableDefaultControl = false;

            gameObject.SetActive(false);
        }

        float playerVerticalAxisValue = Input.GetAxis("Vertical");
        float playerHorizontalAxisValue = Input.GetAxis("Horizontal");

        if (playerVerticalAxisValue != 0 || playerHorizontalAxisValue != 0)
        {
            playerTouchedKey = true;
            playerCarController.disableDefaultControl = false;
        }

        currentTime += Time.deltaTime;
        AcceleratePlayer();
    }

    private void AcceleratePlayer()
    {
        frontLCollider.motorTorque = 1 * motorForce;
        frontRCollider.motorTorque = 1 * motorForce;
        rearLCollider.motorTorque = 1 * motorForce;
        rearRCollider.motorTorque = 1 * motorForce;
    }
}
