using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState6 : PlayerBaseState
{
    int thisState = 6;
    float buttonTime = 1;
    float startedPushing;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        gm.pushingButton1 = true;
        startedPushing = Time.time;
    }
    public override void MoveLeft(GameManager gm)
    {
        Debug.Log("Moving Left");
        gm.pushingButton1 = false;
        gm.ChangePlayerState(gm.ps5);
    }
    public override void MoveRight(GameManager gm)
    {
        Debug.Log("Won't Work");
        //Debug.Log("Moving Right");
        //gm.ChangePlayerState(gm.ps0);
    }
    public override void Jump(GameManager gm)
    {
        //Debug.Log("Jumping");
        //gm.ChangePlayerState(gm.ps5);
    }
    public override void Grab(GameManager gm)
    {
    }
    public override void Fall(GameManager gm)
    {
    }

    public override void PlayerUpdate(GameManager gm)
    {
        //Push Button1 in if it's not already pushed in
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Pushing");
            if (Time.time - startedPushing > buttonTime)
            {
                Debug.Log("Button Pushed");
                gm.SetButton1State(gm.button1State + 1);
                if (gm.button1State == 3)
                {
                    //Button fully pushed. Time to let go.
                    MoveLeft(GameManager.instance);
                }
                else
                {
                    //Button not fully pushed. Reset pushing time
                    startedPushing = Time.time;
                }
            }
        }
        else
            MoveLeft(GameManager.instance);
    }
}
