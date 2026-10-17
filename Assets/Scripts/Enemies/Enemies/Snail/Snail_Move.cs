using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_Move : MoveState
{
    private Snail snail;

    public Snail_Move(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
        D_MoveState stateData, Snail snail) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.snail = snail;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // n?u k? thù ch?m t??ng ho?c không ? trên m?t ??t thì chuy?n tr?ng thái idle
        if (isDetectingWall || !isDetectingLedge)
        {
            Movement?.Flip();
        }

    }
}