using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState46 : PlayerBaseState
{
    int thisState = 46;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps45);
    }
    public override void MoveUp(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps47);
    }
    public override void Jump(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps47);
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
}
