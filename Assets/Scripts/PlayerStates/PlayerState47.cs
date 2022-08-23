using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState47 : PlayerBaseState
{
    int thisState = 47;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
    }
    public override void MoveUp(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps48);
    }
    public override void MoveDown(GameManager gm)
    {
        gm.ChangePlayerState(gm.ps46);
    }
}
