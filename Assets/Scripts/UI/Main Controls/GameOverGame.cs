using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGame : MonoBehaviour
{
    [Header("Player Stats")]
    public PlayerDash playerDash;
    public ParticleSystem playerTrails;
    public EnemySpawner enemySpawner;
    public FrontEnemySpawner frontEnemySpawner;

    [Header("UI Content")]
    public Text highScoreText;
    public Text scoreText;
    public GameObject mainMenuHolder;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        int currentScore = PlayerData.currentScore;
        PlayerData.currentScore = 0;
        scoreText.text = currentScore.ToString();

        int highScore;
        if (PlayerPrefs.HasKey(ControlsInput.PlayerScore))
            highScore = PlayerPrefs.GetInt(ControlsInput.PlayerScore);
        else
            highScore = currentScore;

        PlayerPrefs.SetInt(ControlsInput.PlayerScore, highScore);

        playerTrails.Stop();
        enemySpawner.StopSpawn();
        frontEnemySpawner.StopSpawn();
        PlayerData.movePlayer = false;
    }

    public void ReplayGame()
    {
        playerTrails.Play();
        enemySpawner.StartSpawn();
        frontEnemySpawner.StartSpawn();
        
        PlayerData.movePlayer = true;
        PlayerData.currentScore = 0;

        gameObject.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        playerDash.ResetDashCount();
        playerTrails.Stop();
        enemySpawner.StopSpawn();
        frontEnemySpawner.StopSpawn();

        gameObject.SetActive(false);
        mainMenuHolder.SetActive(true);
    }

}
