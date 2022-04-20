using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState2 : PlayerBaseState
{
    int thisState = 2;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        Debug.Log("Won't Work!");
    }
    public override void MoveRight(GameManager gm)
    {
        Debug.Log("Moving Right");
        gm.ChangePlayerState(gm.ps3);
    }
    public override void Jump(GameManager gm)
    {
        //Debug.Log("Won't Work");
        Debug.Log("Jumping");
        gm.ChangePlayerState(gm.ps1);
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
