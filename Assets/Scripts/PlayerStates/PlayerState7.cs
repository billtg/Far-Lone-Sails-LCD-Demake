using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState7 : PlayerBaseState
{
    int thisState = 7;

    float enteredStateTime;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveLeft(GameManager gm)
    {
        Debug.Log("Moving Left");
        gm.ChangePlayerState(gm.ps8);
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps1);
    }
    public override void Jump(GameManager gm)
    {
        Debug.Log("Won't Work");
        //Debug.Log("Jumping");
        //gm.ChangePlayerState(gm.ps1);
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