using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadLampStatus : MonoBehaviour
{
    [Header("Directional Light")]
    public float intensityThreshold = 1;
    public Light directionalLight;

    [Header("Head Lamps")]
    public GameObject headLampL;
    public GameObject headLampR;

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if (directionalLight.intensity < intensityThreshold && !headLampL.activeInHierarchy)
        {
            headLampL.SetActive(true);
            headLampR.SetActive(true);
        }

        if (directionalLight.intensity >= intensityThreshold && headLampL.activeInHierarchy)
        {
            headLampL.SetActive(false);
            headLampR.SetActive(false);
        }
    }
}
