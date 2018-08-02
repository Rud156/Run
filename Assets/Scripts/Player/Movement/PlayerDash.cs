﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    public GameObject player;

    [Header("Dash Stats")]
    public float dashDistance = 10;
    public GameObject dashTrail;
    public GameObject dashBody;
    public int waitForTotalFrames = 120;

    [Header("Dash UI")]
    public Slider dashSlider;
    public Image dashFiller;
    public Color minWaitColor;
    public Color halfWaitColor;
    public Color maxWaitColor;

    private int currentFrameCount;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentFrameCount = waitForTotalFrames;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentFrameCount < waitForTotalFrames)
            currentFrameCount += 1;

        if (Input.GetKeyDown(KeyCode.Q))
            CheckAndDashPlayer();

        int maxWaitTime = waitForTotalFrames;
        int currentWaitTime = currentFrameCount;
        float waitRatio = (float)currentWaitTime / maxWaitTime;

        if (waitRatio <= 0.5)
            dashFiller.color = Color.Lerp(minWaitColor, halfWaitColor, waitRatio * 2);
        else
            dashFiller.color = Color.Lerp(halfWaitColor, maxWaitColor, (waitRatio - 0.5f) * 2);
        dashSlider.value = waitRatio;
    }

    public void CheckAndDashPlayer()
    {
        if (currentFrameCount >= waitForTotalFrames)
            DashPlayer();
    }

    void DashPlayer()
    {
        RaycastHit hit;
        Vector3 destination = player.transform.position +
            player.transform.forward * dashDistance;

        // Obstacle is in Front
        if (Physics.Linecast(player.transform.position, destination, out hit))
            destination = player.transform.position +
                player.transform.forward * (hit.distance - 1);

        player.transform.position = destination;
        Instantiate(dashTrail, player.transform.position,
            player.transform.rotation);

        Instantiate(dashBody,
            new Vector3(player.transform.position.x, player.transform.position.y - 0.8f,
                player.transform.position.z),
            dashBody.transform.rotation);

        currentFrameCount = 0;
    }
}
