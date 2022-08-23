using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState48 : PlayerBaseState
{
    int thisState = 48;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveUp(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps49);
    }
    public override void MoveDown(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps47);
    }
}
