using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState44 : PlayerBaseState
{
    int thisState = 44;
    float enteredStateTime;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;

        //Activate Fuel button
        if (!Gate.instance.fuelButtonPressed)
            Gate.instance.PressFuelButton();
    }
    public override void Fall(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps42);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
