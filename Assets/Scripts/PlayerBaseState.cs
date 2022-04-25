using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    int thisState;
    public virtual void EnterState(GameManager gm) { }
    public virtual void MoveLeft(GameManager gm) { }
    public virtual void MoveRight(GameManager gm) { }
    public virtual void MoveUp(GameManager gm) { }
    public virtual void MoveDown(GameManager gm) { }
    public virtual void Jump(GameManager gm) { }
    public virtual void Grab(GameManager gm)
    {
    }
    public virtual void Fall(GameManager gm) { }
    public virtual void PlayerUpdate(GameManager gm) { }

    public virtual bool IsOnGround()
    {
        return false;
    }
    public virtual bool GameOver()
    {
        return false;
    }

}
