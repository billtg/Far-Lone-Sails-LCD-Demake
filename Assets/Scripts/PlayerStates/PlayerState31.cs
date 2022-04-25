using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState31 : PlayerBaseState
{
    int thisState = 31;
    bool pushingButton5;
    float buttonTime = 1;
    float startedPushing;

    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveLeft(GameManager gm)
    {
        Debug.Log("Moving Left");
        gm.ChangePlayerState(gm.ps30);
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
        gm.ChangePlayerState(gm.ps32);
    }

    public override void PlayerUpdate(GameManager gm)
    {
        //Push Button1 in if it's not already pushed in
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Start the timer if you're just starting to push it
            if (!pushingButton5)
            {
                Debug.Log("Started pushing button 5");
                pushingButton5 = true;
                startedPushing = Time.time;
            }
            if (Time.time - startedPushing > buttonTime)
            {
                Debug.Log("Button 5 Pushed");
                gm.ChangeSail();
                pushingButton5 = false;
            }
        }
        else
        {
            if (pushingButton5)
                pushingButton5 = false;
        }
    }
}
