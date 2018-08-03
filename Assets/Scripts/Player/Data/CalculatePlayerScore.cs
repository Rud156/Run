using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatePlayerScore : MonoBehaviour
{
    public Text scoreText;
    public int waitForFrames = 60;

    private float currentFrameCount;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentFrameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.movePlayer)
        {
            scoreText.text = "Score: " + PlayerData.currentScore;
            currentFrameCount += 1;

            if (currentFrameCount >= waitForFrames)
            {
                PlayerData.currentScore += 1;
                currentFrameCount = 0;
            }
        }
        else
            currentFrameCount = 0;
    }
}
