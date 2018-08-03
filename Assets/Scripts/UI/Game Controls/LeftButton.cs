using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        PlayerData.leftButtonPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerData.leftButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerData.leftButtonPressed = false;
    }
}
