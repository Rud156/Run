using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [Header("Player Stats")]
    public PlayerDash playerDash;
    public ParticleSystem playerTrails;
    public EnemySpawner enemySpawner;
    public FrontEnemySpawner frontEnemySpawner;

    [Header("UI Content")]
    public Text scoreText;
    public GameObject pauseMenuHolder;
    public GameObject mainMenuHolder;

    /// <summary>
    /// Callback sent to all game objects when the player pauses.
    /// </summary>
    /// <param name="pauseStatus">The pause state of the application.</param>
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            PauseGameOnButtonPress();
    }

    public void PauseGameOnButtonPress()
    {
        Time.timeScale = 0;
        scoreText.text = PlayerData.currentScore.ToString();
        pauseMenuHolder.SetActive(true);
    }

    public void ResumeGameOnButtonPress()
    {
        Time.timeScale = 1;
        pauseMenuHolder.SetActive(false);
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
