using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState40 : PlayerBaseState
{
    int thisState = 40;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveRight(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps41);
    }
    public override void MoveDown(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps39);
    }
}
