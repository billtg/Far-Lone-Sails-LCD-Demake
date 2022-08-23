using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState50 : PlayerBaseState
{
    int thisState = 50;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps51);
    }
    public override void Fall(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps49);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
