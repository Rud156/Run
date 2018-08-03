using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [Header("GameObjects to Reset")]
    public GameObject player;
    public GameObject enemyHolder;

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
        if (pauseStatus && PlayerData.movePlayer)
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
        Time.timeScale = 1;

        playerDash.ResetDashCount();
        playerTrails.Stop();
        enemySpawner.StopSpawn();
        frontEnemySpawner.StopSpawn();

        player.transform.position = PlayerData.defaultPosition;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<MeshRenderer>().enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;

        foreach (Transform child in enemyHolder.transform)
            Destroy(child.gameObject);

        PlayerData.currentScore = 0;
        PlayerData.movePlayer = false;

        pauseMenuHolder.SetActive(false);
        mainMenuHolder.SetActive(true);
    }
}
