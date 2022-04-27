using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState8 : PlayerBaseState
{
    int thisState = 8;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        if (gm.playerHoldingNozzle)
            FireHose.instance.activateLCDs(thisState, true);
    }
    public override void MoveLeft(GameManager gm)
    {
        if (!gm.playerHoldingNozzle)
            gm.ChangePlayerState(gm.ps9);
    }
    public override void MoveRight(GameManager gm)
    {
        Exit(gm);
        gm.ChangePlayerState(gm.ps7);
    }
    public override void Jump(GameManager gm)
    {
        if (!gm.button2Pushed && !gm.playerHoldingNozzle && Health.instance.fuelHealth > 0)
        {
            Debug.Log("Jumping");
            gm.ChangePlayerState(gm.ps10);
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
