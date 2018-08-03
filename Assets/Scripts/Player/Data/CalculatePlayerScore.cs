using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatePlayerScore : MonoBehaviour
{
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.movePlayer)
            scoreText.text = "Score: " + PlayerData.currentScore;
    }
}
