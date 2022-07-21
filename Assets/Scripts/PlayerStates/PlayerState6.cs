using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState6 : PlayerBaseState
{
    //This state controls pushing of the Motor button
    int thisState = 6;
    float startedPushing;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.pushingButton1 = false;
        gm.ChangePlayerState(gm.ps5);
    }
    public override void Jump(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps12);
    }

    public override void PlayerUpdate(GameManager gm)
    {
        //Ignore if the button is already pushed, or the motor is dead
        if (gm.button1State == 3)
            return;
        if (Health.instance.motorHealth == 0)
            return;

        //Push Button1 in if it's not already pushed in
        if (Gamepad.current.dpad.right.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            //Release the brake
            if (gm.brakeActive)
                Brake.instance.Button4Pushed(false);
            if (gm.fuel == 0)
                return;

            //Start pushing the button if it wasn't started
            if (!gm.pushingButton1)
            {
                Debug.Log("Started pushing button");
                gm.pushingButton1 = true;
                startedPushing = Time.time;
                //if (gm.brakeActive)
                //    Brake.instance.Button4Pushed(false);
            }

            if (Time.time - startedPushing > gm.button1PushingTime*Health.instance.motorDelayFactor)
            {
                //Check for blocking gate
                if (gm.gateBlocking)
                {
                    gm.HitGate();
                    return;
                }
                Debug.Log("Button Pushed");
                gm.SetButton1State(gm.button1State + 1);
                if (gm.button1State == 3)
                {
                    //Button fully pushed. Time to let go.
                    //MoveLeft(GameManager.instance);
                }
                else
                {
                    //Button not fully pushed. Reset pushing time
                    startedPushing = Time.time;
                }
            }
        }
        else
        {
            gm.pushingButton1 = false;
        }
    }
}
