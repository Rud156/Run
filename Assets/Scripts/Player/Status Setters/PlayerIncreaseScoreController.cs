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
    private bool startScoring;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        startScoring = false;
        currentScore = 0;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!startScoring)
            return;

        currentScore += (scoreIncreaseRate * Time.deltaTime);
        uiScoreText.text = $"Points: {ExtensionFunctions.Format2DecimalPlace(currentScore)}";
    }

    public void GenerateRandomScore(Vector3 position)
    {
        if (!startScoring)
            return;

        int randomScore = Random.Range(minScoreAmount, maxScoreAmount);
        rewardTextMesh.text = $"+{randomScore} Points";
        rewardTextAnimator.SetTrigger(AnimatorVariables.DisplayText);

        Instantiate(scoreEffect, position, scoreEffect.transform.rotation);

        currentScore += randomScore;
    }

    public void StartScoreCalculation() => startScoring = true;
}
