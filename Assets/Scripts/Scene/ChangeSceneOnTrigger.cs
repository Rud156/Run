using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public float changeSceneAfterTime = 2f;

    public void ChangeSceneInvoke(bool busted = false)
    {
        NextSceneData.playerBusted = busted;
        Invoke("ChangeScene", changeSceneAfterTime);
    }

    private void ChangeScene() =>
        SceneManager.LoadScene(2);
}
