using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState43 : PlayerBaseState
{
    int thisState = 43;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;

        //Activate Door button
        if (Gate.instance.fuelLoaded)
            Gate.instance.PressDoorButton();
    }
    public override void Fall(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps41);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
