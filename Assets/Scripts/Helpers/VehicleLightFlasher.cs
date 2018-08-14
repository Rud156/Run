using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLightFlasher : MonoBehaviour
{
    [Header("Lights")]
    public Renderer redLight;
    public Renderer blueLight;

    [Header("Blink Stats")]
    public float blinkRate;

    private Color redColor = Color.red;
    private Color blueColor = Color.blue;
    private Color blackColor = Color.black;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => StartCoroutine(BlinkLight());

    IEnumerator BlinkLight()
    {
        bool selectRed = true;

        while (true)
        {
            if (selectRed)
            {
                redLight.material.color = redColor;
                redLight.material.SetColor("_EmissionColor", redColor * 4);
                blueLight.material.color = blackColor;
                blueLight.material.SetColor("_EmissionColor", blackColor);
            }
            else
            {
                redLight.material.color = blackColor;
                redLight.material.SetColor("_EmissionColor", blackColor);
                blueLight.material.color = blueColor;
                blueLight.material.SetColor("_EmissionColor", blueColor * 4);
            }

            selectRed = !selectRed;
            yield return new WaitForSeconds(blinkRate);
        }
    }
}