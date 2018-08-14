using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light objectLight;
    private float randomTime;

    // Use this for initialization
    void Start()
    {
        objectLight = GetComponent<Light>();
        StartCoroutine(StartFlicker());
    }

    IEnumerator StartFlicker()
    {
        while (true)
        {
            randomTime = Random.Range(0, 2);

            objectLight.enabled = false;
            yield return new WaitForSeconds(randomTime);

            randomTime = Random.Range(0, 2);

            objectLight.enabled = true;
            yield return new WaitForSeconds(randomTime);
        }
    }
}
