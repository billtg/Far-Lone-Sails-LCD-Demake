using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState1 : PlayerBaseState
{
    int thisState = 1;

    float enteredStateTime;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        enteredStateTime = Time.time;
    }
    public override void MoveLeft(GameManager gm)
    {
        Debug.Log("Won't Work!");
    }
    public override void MoveRight(GameManager gm)
    {
        Debug.Log("Moving Right");
        gm.ChangePlayerState(gm.ps0);
    }
    public override void Jump(GameManager gm)
    {
        Debug.Log("Won't Work");
        //Debug.Log("Jumping");
        //gm.ChangePlayerState(gm.ps1);
    }
    public override void Grab(GameManager gm)
    {
    }
    public override void Fall(GameManager gm)
    {
        Debug.Log("Falling");
        gm.ChangePlayerState(gm.ps2);
    }
    public override void PlayerUpdate(GameManager gm)
    {
        //Check for falling time
        if (Time.time - enteredStateTime > gm.playerHangTime)
            Fall(gm);
    }
}
