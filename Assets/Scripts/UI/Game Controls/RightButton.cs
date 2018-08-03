using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        PlayerData.rightButtonPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerData.rightButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerData.rightButtonPressed = false;
    }
}
