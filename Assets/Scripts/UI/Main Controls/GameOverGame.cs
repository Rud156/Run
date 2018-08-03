using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGame : MonoBehaviour
{
    [Header("GameObjects to Reset")]
    public GameObject player;
    public GameObject enemyHolder;

    [Header("Effector GameObjects")]
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

        int highScore;
        if (PlayerPrefs.HasKey(ControlsInput.PlayerScore))
            highScore = PlayerPrefs.GetInt(ControlsInput.PlayerScore);
        else
            highScore = currentScore;
        highScore = highScore > currentScore ? highScore : currentScore;

        scoreText.text = currentScore.ToString();
        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt(ControlsInput.PlayerScore, highScore);

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
        gameObject.SetActive(false);
        mainMenuHolder.SetActive(true);
    }

}
