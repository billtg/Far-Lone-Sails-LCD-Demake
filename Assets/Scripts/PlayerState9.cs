using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState9 : PlayerBaseState
{
    int thisState = 9;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
    }
    public override void MoveRight(GameManager gm)
    {
        Debug.Log("Moving Right");
        gm.ChangePlayerState(gm.ps8);
    }
    public override void Jump(GameManager gm)
    {
    }
    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingBox)
        {
            //Drop box if a space is available and lift is there
            if (!gm.lcdGroundBoxes[thisState].activeSelf && !gm.button2Pushed)
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
