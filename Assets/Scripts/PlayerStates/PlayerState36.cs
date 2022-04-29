using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState36 : PlayerBaseState
{
    int thisState = 36;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        if (gm.playerHoldingNozzle)
            FireHose.instance.ActivateHoseLCDs(thisState, true);
    }
    public override void MoveRight(GameManager gm)
    {
        Debug.Log("Moving Right");
        Exit(gm);
        gm.ChangePlayerState(gm.ps2);
    }
    public override void Jump(GameManager gm)
    {
        //Debug.Log("Won't Work");
        if (!gm.playerHoldingBox)
        {
            Debug.Log("Jumping");
            Exit(gm);
            gm.ChangePlayerState(gm.ps37);
        }
    }
    void Exit(GameManager gm)
    {
        if (gm.playerHoldingNozzle)
            FireHose.instance.ActivateHoseLCDs(thisState, false);
    }
    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingNozzle)
        {
            gm.DropNozzle();
        }
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
}
