﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    public float rotationSpeed = 100;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        PlayerData.yaw = gameObject.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // float moveX = Input.GetAxis(ControlsInput.Horizontal);

        float moveX;

        if (PlayerData.leftButtonPressed && !PlayerData.rightButtonPressed)
            moveX = -1;
        else if (PlayerData.rightButtonPressed && !PlayerData.leftButtonPressed)
            moveX = 1;
        else
            moveX = 0;

        PlayerData.yaw += moveX * rotationSpeed * Time.deltaTime;
        gameObject.transform.eulerAngles = Vector3.up * PlayerData.yaw;
    }
}
