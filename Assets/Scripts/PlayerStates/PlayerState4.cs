using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState4 : PlayerBaseState
{
    int thisState = 4;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps0);
    }
    public override void Jump(GameManager gm)
    {
        //Debug.Log("Jumping");
        //gm.ChangePlayerState(gm.ps5);
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
    public override void Fall(GameManager gm)
    {
    }
    public override void PlayerUpdate(GameManager gm)
    {
    }
}
