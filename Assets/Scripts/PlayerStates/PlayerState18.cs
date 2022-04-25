using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState18 : PlayerBaseState
{
    int thisState = 18;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps15);
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps19);
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

    public override bool IsOnGround()
    {
        return true;
    }
}
