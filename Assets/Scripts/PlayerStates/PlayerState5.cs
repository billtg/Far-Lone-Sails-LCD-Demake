using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState5 : PlayerBaseState
{
    int thisState = 5;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        if (gm.playerHoldingNozzle)
            FireHose.instance.activateLCDs(thisState, true);
    }
    public override void MoveLeft(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps1);
    }
    public override void MoveRight(GameManager gm)
    {
        if (!gm.playerHoldingNozzle)
            gm.ChangePlayerState(gm.ps6);
    }
    public override void Jump(GameManager gm)
    {
        Debug.Log("Push Steam Button");
        SteamGauge.instance.PressButton3();
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
