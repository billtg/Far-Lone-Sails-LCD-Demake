using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState8 : PlayerBaseState
{
    int thisState = 8;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        //Activate hose sprites
        if (gm.playerHoldingNozzle || gm.playerHoldingWelder)
            FireHose.instance.ActivateHoseLCDs(thisState, true);
        //Activate Firehose
        if (gm.playerHoldingNozzle && Fire.instance.fuelOnFire)
            FireHose.instance.StartHosing(HealthBar.fuel);
        //Activate welding
        if (gm.playerHoldingWelder && Health.instance.fuelHealth < 3)
            FireHose.instance.StartWelding(HealthBar.fuel);
    }
    public override void MoveLeft(GameManager gm)
    {
        if (!gm.playerHoldingNozzle && !gm.playerHoldingWelder)
            gm.ChangePlayerState(gm.ps9);
    }
    public override void MoveRight(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps7);
    }
    public override void Jump(GameManager gm)
    {
        if (!gm.button2Pushed && !gm.playerHoldingNozzle && !gm.playerHoldingWelder && Health.instance.fuelHealth > 0)
        {
            gm.ChangePlayerState(gm.ps10);
        }
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
        FireHose.instance.StopHosing();
        FireHose.instance.StopWelding();
    }
}
