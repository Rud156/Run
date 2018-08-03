using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static  Vector3 defaultPosition = new Vector3(0, 1.1f, -80.7f);

    // Controls
    public static float yaw;
    public static bool movePlayer;
    
    // Buttons
    public static bool leftButtonPressed;
    public static bool rightButtonPressed;

    // Score
    public static int currentScore;
}
