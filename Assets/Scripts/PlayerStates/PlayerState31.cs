using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        gm.ChangePlayerState(gm.ps30);
    }
    public override void Jump(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps32);
    }

    public override void PlayerUpdate(GameManager gm)
    {
        //Push Button5 in
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            //Start the timer if you're just starting to push it
            if (!pushingButton5)
            {
                Debug.Log("Started pushing button 5");
                pushingButton5 = true;
                startedPushing = Time.time;
                if (!gm.sailsUp && Health.instance.sailHealth > 0)
                    Sails.instance.SetSails(1);
            }
            if (Time.time - startedPushing > buttonTime*Health.instance.sailDelayFactor && Health.instance.sailHealth > 0)
            {
                Debug.Log("Button 5 Pushed");
                gm.ChangeSail();
                pushingButton5 = false;
                if (!gm.sailsUp)
                    Sails.instance.SetSails(0);
            }
        }
        else
        {
            if (pushingButton5)
            {
                pushingButton5 = false;
                if (!gm.sailsUp)
                    Sails.instance.SetSails(0);
            }
        }
    }
}
