using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    [Header("Game Start Objects")]
    public ParticleSystem playerTrails;
    public EnemySpawner enemySpawner;
    public FrontEnemySpawner frontEnemySpawner;

    [Header("UI Content")]
    public Text scoreDisplay;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerData.movePlayer = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    // Use this for initialization
    void Start()
    {
        SetScoreBoard();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        SetScoreBoard();
    }

    void SetScoreBoard()
    {
        int playerScore;
        if (PlayerPrefs.HasKey(ControlsInput.PlayerScore))
            playerScore = PlayerPrefs.GetInt(ControlsInput.PlayerScore);
        else
            playerScore = 0;

        scoreDisplay.text = playerScore.ToString();
    }

    public void StartGame()
    {
        playerTrails.Play();
        enemySpawner.StartSpawn();
        frontEnemySpawner.StartSpawn();

        PlayerData.movePlayer = true;
        PlayerData.currentScore = 0;

        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
