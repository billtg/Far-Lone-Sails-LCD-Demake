using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState39 : PlayerBaseState
{
    int thisState = 39;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveUp(GameManager gm)
    {
        if (Gate.instance.ladderDown)
            gm.ChangePlayerState(gm.ps40);
    }
    public override void Fall(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps35);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
