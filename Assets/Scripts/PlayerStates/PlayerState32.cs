using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState32 : PlayerBaseState
{
    int thisState = 32;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps33);
    }
    public override void Fall(GameManager gm)
    {
        Debug.Log("Falling");
        gm.ChangePlayerState(gm.ps31);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
