using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState1 : PlayerBaseState
{
    int thisState = 1;

    float enteredStateTime;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps7);
    }
    public override void MoveRight(GameManager gm)
    {
        Debug.Log("Moving Right");
        gm.ChangePlayerState(gm.ps5);
    }
    public override void MoveUp(GameManager gm)
    {
        //Debug.Log("Won't Work");
        Debug.Log("Moving Up");
        Elevator.instance.SetElevatorState(2);
        gm.ChangePlayerState(gm.ps2);
    }
    public override void MoveDown(GameManager gm)
    {
        //Debug.Log("Won't Work");
        Debug.Log("Moving Down");
        Elevator.instance.SetElevatorState(0);
        gm.ChangePlayerState(gm.ps0);
    }

    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingBox)
        {
            //Drop box if a space is available
            if (!gm.lcdGroundBoxes[thisState].activeSelf)
                gm.PickUpBox(thisState, false);
        }
        else
        {
            //pick up a box if there's one here
            if (gm.lcdGroundBoxes[thisState].activeSelf)
                gm.PickUpBox(thisState, true);
        }
    }
}
