using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState2 : PlayerBaseState
{
    int thisState = 2;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        if (gm.playerHoldingNozzle || gm.playerHoldingWelder)
            FireHose.instance.ActivateHoseLCDs(thisState, true);
    }
    public override void MoveLeft(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps36);
    }
    public override void MoveUp(GameManager gm)
    {
        Elevator.instance.SetElevatorState(3);
        Exit(gm);
        gm.ChangePlayerState(gm.ps3);
    }
    public override void MoveDown(GameManager gm)
    {
        Elevator.instance.SetElevatorState(1);
        Exit(gm);
        gm.ChangePlayerState(gm.ps1);
    }
    public override void Jump(GameManager gm)
    {
    }
    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingNozzle)
            gm.DropNozzle();
        else if (gm.playerHoldingWelder)
            gm.DropWelder();
        else
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
    void Exit(GameManager gm)
    {
        if (gm.playerHoldingNozzle || gm.playerHoldingWelder)
            FireHose.instance.ActivateHoseLCDs(thisState, false);
    }
}
