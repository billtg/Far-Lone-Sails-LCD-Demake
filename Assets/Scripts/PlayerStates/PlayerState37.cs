using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState37 : PlayerBaseState
{
    int thisState = 37;

    float enteredStateTime;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
        if (gm.playerHoldingNozzle)
            FireHose.instance.ActivateHoseLCDs(thisState, true);
    }
    public override void Grab(GameManager gm)
    {
        //Grab the fire hose, or let go of it
        if (gm.playerHoldingNozzle)
        {
            gm.DropNozzle();
        }
        else
        {
            gm.PickUpNozzle();
        }
    }
    public override void Fall(GameManager gm)
    {
        Debug.Log("Falling");
        Exit(gm);
        gm.ChangePlayerState(gm.ps36);
    }
    void Exit(GameManager gm)
    {
        if (gm.playerHoldingNozzle)
            FireHose.instance.ActivateHoseLCDs(thisState, false);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
