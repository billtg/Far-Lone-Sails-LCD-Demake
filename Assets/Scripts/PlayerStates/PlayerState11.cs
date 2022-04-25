using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState11 : PlayerBaseState
{
    int thisState = 11;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        //Debug.Log("Won't Work");
        //Debug.Log("Moving Down");
        gm.ChangePlayerState(gm.ps12);
    }
    public override void Jump(GameManager gm)
    {
        Debug.Log("Push Brake Button");
        Brake.instance.Button4Pushed(true);
    }
}
