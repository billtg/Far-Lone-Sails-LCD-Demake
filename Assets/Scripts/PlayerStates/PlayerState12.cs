using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState12 : PlayerBaseState
{
    int thisState = 12;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps11);
    }
    public override void Grab(GameManager gm)
    {
    }
    public override void Fall(GameManager gm)
    {
        Debug.Log("Falling");
        gm.ChangePlayerState(gm.ps6);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
