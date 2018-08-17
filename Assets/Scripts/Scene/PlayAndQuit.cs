using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAndQuit : MonoBehaviour
{
    public Text scoreText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        float currentScore = 0;
        if (PlayerPrefs.HasKey(PlayerPrefsVariables.PlayerScore))
            currentScore = PlayerPrefs.GetFloat(PlayerPrefsVariables.PlayerScore);

        scoreText.text = $"Highest Score: {ExtensionFunctions.Format2DecimalPlace(currentScore)}";
    }

    public void PlayGame()
    {
        NextSceneData.nextSceneIndex = 1;
        NextSceneData.makeInfoTextVisible = false;
        SceneManager.LoadScene(2);
    }

    public void QuitGame() => Application.Quit();
}
