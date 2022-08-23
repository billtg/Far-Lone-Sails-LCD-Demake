using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState51 : PlayerBaseState
{
    int thisState = 51;
    public override void EnterState(GameManager gm)
    {
        gm.UpdatePlayerSprite(thisState);
        Beacon.instance.PushBeaconButton();
    }
    public override void MoveLeft(GameManager gm)
    {
        if (!Beacon.instance.beaconLit)
            gm.ChangePlayerState(gm.ps50);
    }
}
