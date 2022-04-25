using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState5 : PlayerBaseState
{
    int thisState = 5;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        //Debug.Log("Moving Left!");
        gm.ChangePlayerState(gm.ps1);
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps6);
    }
    public override void Jump(GameManager gm)
    {
        Debug.Log("Push Steam Button");
        SteamGauge.instance.PressButton3();
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
        //Debug.Log("Falling");
        //gm.ChangePlayerState(gm.ps4);
    }
    public override void PlayerUpdate(GameManager gm)
    {
    }
}
