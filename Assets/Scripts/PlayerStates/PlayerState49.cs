using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState49 : PlayerBaseState
{
    int thisState = 49;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void Jump(GameManager gm)
    {
        if (!gm.playerHoldingBox)
            gm.ChangePlayerState(gm.ps50);
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps48);
    }
    public override void MoveDown(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps48);
    }
    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingBox)
        {
            //Drop box if a space is available and door is open
            if (!gm.lcdGroundBoxes[thisState].activeSelf && Beacon.instance.doorOpen)
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
