using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AG_MoveState : MoveState
{
    private AngryPig pig;
    public AG_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, AngryPig pig) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.pig = pig;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(pig.chargeState);
        }
        // n?u k? thù ch?m t??ng ho?c không ? trên m?t ??t thì chuy?n tr?ng thái idle
        else if (isDetectingWall || !isDetectingLedge )
        {
            pig.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(pig.idleState);
        }

    }
}
