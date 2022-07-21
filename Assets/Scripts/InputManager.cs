using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No Gamepad");
            return; // No gamepad connected.
        }

        if (gamepad.dpad.left.wasPressedThisFrame)
        {
            Debug.Log("Gamepad left");
            GameManager.instance.Left();
        }
    }
}
