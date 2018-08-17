using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSceneOnStart : MonoBehaviour
{
    public GameObject infoText;
    public Slider loadingSlider;
    public float bufferTime = 2f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (NextSceneData.makeInfoTextVisible)
        {
            infoText.SetActive(true);
            infoText.GetComponent<Text>().text = NextSceneData.playerBusted ?
                "You were BUSTED !!!" : "You were Destroyed !!!";
        }
        else
        {
            infoText.SetActive(false);
            infoText.GetComponent<Text>().text = "";
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(LoadNextSceneAsync());
    }

    IEnumerator LoadNextSceneAsync()
    {
        loadingSlider.value = 0;
        yield return new WaitForSeconds(bufferTime);

        int sceneIndex = NextSceneData.nextSceneIndex;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            yield return null;
        }
    }
}
