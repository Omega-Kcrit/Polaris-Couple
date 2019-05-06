using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (InputManager.AButton())
        {
            Debug.Log("A Button pressed");
        }

        if (InputManager.BButton())
        {
            Debug.Log("B Button pressed");
        }

        if (InputManager.XButton())
        {
            Debug.Log("X Button pressed");
        }

        if (InputManager.YButton())
        {
            Debug.Log("Y Button pressed");
        }

        if (InputManager.MainJoystick() != Vector3.zero)
        {
            Debug.Log("Main Joystick moving! Current position: " + InputManager.MainJoystick());
        }

        if (InputManager.RightTrigger())
        {
            Debug.Log("Right trigger pressed");
        }

        if (InputManager.LeftTrigger())
        {
            Debug.Log("Left trigger pressed");
        }
    }
}
