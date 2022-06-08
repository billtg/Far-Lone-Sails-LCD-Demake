using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState3 : PlayerBaseState
{
    int thisState = 3;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        if (gm.playerHoldingNozzle || gm.playerHoldingWelder)
            FireHose.instance.ActivateHoseLCDs(thisState, true);
    }
    public override void MoveRight(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps30);
    }
    public override void MoveDown(GameManager gm)
    {
        Elevator.instance.SetElevatorState(2);
        Exit(gm);
        gm.ChangePlayerState(gm.ps2);
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
