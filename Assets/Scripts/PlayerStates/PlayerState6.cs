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
    }
    public override void MoveLeft(GameManager gm)
    {
        Debug.Log("Moving Left");
        gm.pushingButton1 = false;
        gm.ChangePlayerState(gm.ps5);
    }
    public override void MoveRight(GameManager gm)
    {
        //Debug.Log("Won't Work");
        //Debug.Log("Moving Right");
        //gm.ChangePlayerState(gm.ps0);
    }
    public override void Jump(GameManager gm)
    {
        //Debug.Log("Won't Work");
        //Debug.Log("Moving Up");
        gm.ChangePlayerState(gm.ps12);
    }

    public override void PlayerUpdate(GameManager gm)
    {
        if (gm.button1State == 3)
            return;
        //Push Button1 in if it's not already pushed in
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Release the brake
            if (gm.brakeActive)
                Brake.instance.Button4Pushed(false);
            if (gm.fuel == 0)
                return;
            //Debug.Log("Pushing");
            if (!gm.pushingButton1)
            {
                Debug.Log("Started pushing button");
                gm.pushingButton1 = true;
                startedPushing = Time.time;
                if (gm.brakeActive)
                    Brake.instance.Button4Pushed(false);
            }
            if (Time.time - startedPushing > buttonTime)
            {
                Debug.Log("Button Pushed");
                gm.SetButton1State(gm.button1State + 1);
                if (gm.button1State == 3)
                {
                    //Button fully pushed. Time to let go.
                    //MoveLeft(GameManager.instance);
                }
                else
                {
                    //Button not fully pushed. Reset pushing time
                    startedPushing = Time.time;
                }
            }
        }
        else
        {
            gm.pushingButton1 = false;
        }
    }
}
