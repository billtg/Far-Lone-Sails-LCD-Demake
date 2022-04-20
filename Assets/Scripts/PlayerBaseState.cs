using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(GameManager gm);
    public abstract void MoveLeft(GameManager gm);
    public abstract void MoveRight(GameManager gm);
    public abstract void Jump(GameManager gm);
    public abstract void Grab(GameManager gm);

    public abstract void Fall(GameManager gm);

    public abstract void PlayerUpdate(GameManager gm);

}
