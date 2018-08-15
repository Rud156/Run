using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionRewardsController : MonoBehaviour
{
    [Header("Health Reward")]
    public int minHealth = 1;
    public int maxHealth = 10;

    [Header("Bullet Time Reduction Reward")]
    public float minBulletTime = 0.01f;
    public float maxBulletTime = 0.07f;

    [Header("Reward Text")]
    public Animator rewardTextAnimator;
    public TextMesh rewardTextMesh;

    [Header("Player Stats Effector")]
    public PlayerDamageAndDeathController playerDamage;
    public TargetClosestPolice playerTargetClosestPolice;

    [Header("Enemy Stats Effector")]
    public SpawnExplosionAroundPlayer explosionSpawner;

    public void GenerateReward()
    {
        float randomValue = Random.Range(0, 1f);

        if (randomValue <= 0.7f)
            GenerateHealthReward();
        else
            GenerateBulletTimeReward();

        explosionSpawner.SpawnEffectAroundPlayer();
    }

    private void GenerateHealthReward()
    {
        int randomHealth = (Random.Range(0, 1000) % maxHealth) + minHealth;
        playerDamage.IncreaseHealth(randomHealth);
        DisplayAnimatedText($"+{randomHealth} Health");
    }

    private void GenerateBulletTimeReward()
    {
        float randomBulletTime = (float)System.Math.Round(Random.Range(minBulletTime, maxBulletTime), 2);
        playerTargetClosestPolice.DecreaseBulletTime(randomBulletTime);
        DisplayAnimatedText($"-{randomBulletTime} Shot Time");
    }

    private void DisplayAnimatedText(string text)
    {
        rewardTextMesh.text = text;
        rewardTextAnimator.SetTrigger(AnimatorVariables.DisplayText);
    }
}
