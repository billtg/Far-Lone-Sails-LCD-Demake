using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState1 : PlayerBaseState
{
    int thisState = 1;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        if (gm.playerHoldingNozzle)
            FireHose.instance.activateLCDs(thisState, true);
    }
    public override void MoveLeft(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps7);
    }
    public override void MoveRight(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps5);
    }
    public override void MoveUp(GameManager gm)
    {
        Elevator.instance.SetElevatorState(2);
        Exit(gm);
        gm.ChangePlayerState(gm.ps2);
    }
    public override void MoveDown(GameManager gm)
    {
        if (!gm.playerHoldingNozzle)
        {
            Elevator.instance.SetElevatorState(0);
            gm.ChangePlayerState(gm.ps0);
        }
    }

    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingNozzle)
            gm.DropNozzle();
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
        if (gm.playerHoldingNozzle)
            FireHose.instance.activateLCDs(thisState, false);
    }
}
