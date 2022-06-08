using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState38 : PlayerBaseState
{
    int thisState = 38;

    float enteredStateTime;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
        if (gm.playerHoldingWelder)
            FireHose.instance.ActivateHoseLCDs(thisState, true);
    }
    public override void MoveRight(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps36);
    }
    public override void Grab(GameManager gm)
    {
        //Grab the welder, or let go of it
        if (gm.playerHoldingWelder)
        {
            gm.DropWelder();
        }
        else
        {
            gm.PickUpWelder();
        }
    }
    void Exit(GameManager gm)
    {
        if (gm.playerHoldingWelder)
            FireHose.instance.ActivateHoseLCDs(thisState, false);
    }
}
