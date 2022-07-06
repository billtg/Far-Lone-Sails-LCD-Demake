using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState35 : PlayerBaseState
{
    int thisState = 35;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps34);
    }
    public override void Jump(GameManager gm)
    {
        //Only jump if the ladder is down
        if (Gate.instance.ladderDown)
            gm.ChangePlayerState(gm.ps39);
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
