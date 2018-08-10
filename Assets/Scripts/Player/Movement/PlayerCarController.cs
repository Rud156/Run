using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : BaseCarController
{
    private float horizontalInput;
    private float verticalInput;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        GetInput();

        base.Steer(horizontalInput);
        base.Accelerate(verticalInput);

        base.UpdateWheelPoses();
    }

    public override void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
}
