using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public float changeSceneAfterTime = 2f;

    public void SetScoreAndChangeScreen(float currentScore, bool busted = false)
    {
        float currentSavedScore = 0;
        if (PlayerPrefs.HasKey(PlayerPrefsVariables.PlayerScore))
            currentSavedScore = PlayerPrefs.GetFloat(PlayerPrefsVariables.PlayerScore);

        if (currentScore > currentSavedScore)
            PlayerPrefs.SetFloat(PlayerPrefsVariables.PlayerScore, currentScore);

        NextSceneData.playerBusted = busted;
        NextSceneData.makeInfoTextVisible = true;
        NextSceneData.nextSceneIndex = 1;
        Invoke("ChangeScene", changeSceneAfterTime);
    }

    private void ChangeScene() =>
        SceneManager.LoadScene(2);
}
