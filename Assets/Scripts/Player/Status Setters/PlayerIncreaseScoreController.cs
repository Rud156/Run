using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIncreaseScoreController : MonoBehaviour
{
    [Header("Score Stats")]
    public GameObject scoreEffect;
    public int minScoreAmount;
    public int maxScoreAmount;
    public float scoreIncreaseRate = 0.1f;

    [Header("Reward Text")]
    public TextMesh rewardTextMesh;
    public Animator rewardTextAnimator;

    [Header("UI Text")]
    public Text uiScoreText;

    private float currentScore;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => currentScore = 0;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        currentScore += scoreIncreaseRate * Time.deltaTime;
        uiScoreText.text = $"Points: {currentScore}";
    }

    public void GenerateRandomScore()
    {
        int randomScore = Random.Range(minScoreAmount, maxScoreAmount);
        rewardTextMesh.text = $"+{randomScore} Points";
        rewardTextAnimator.SetTrigger(AnimatorVariables.DisplayText);

        currentScore += randomScore;
    }
}
