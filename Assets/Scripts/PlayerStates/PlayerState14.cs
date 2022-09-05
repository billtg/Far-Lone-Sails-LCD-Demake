using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState14 : PlayerBaseState
{
    int thisState = 14;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps13);
    }
    public override void Fall(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps15);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
