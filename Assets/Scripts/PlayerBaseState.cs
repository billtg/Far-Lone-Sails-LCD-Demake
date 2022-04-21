using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public virtual void EnterState(GameManager gm) { }
    public virtual void MoveLeft(GameManager gm) { }
    public virtual void MoveRight(GameManager gm) { }
    public virtual void MoveUp(GameManager gm) { }
    public virtual void MoveDown(GameManager gm) { }
    public virtual void Jump(GameManager gm) { }
    public virtual void Grab(GameManager gm) { }
    public virtual void Fall(GameManager gm) { }
    public virtual void PlayerUpdate(GameManager gm) { }



}
