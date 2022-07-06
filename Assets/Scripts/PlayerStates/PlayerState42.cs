using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState42 : PlayerBaseState
{
    int thisState = 42;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps41);
    }
    public override void Jump(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps44);
    }
    public override void Grab(GameManager gm)
    {
        if (gm.playerHoldingBox)
        {
            //Drop box if a space is available & if fuel door is open
                if (!gm.lcdGroundBoxes[thisState].activeSelf && Gate.instance.fuelDoorOpen)
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
