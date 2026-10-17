using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_MoveState : MoveState
{
    private Slime slime;
    public Slime_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Slime slime) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.slime = slime;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(slime.playerDetectedState);
        }
        // nếu kẻ thù chạm tường hoặc không ở trên mặt đất thì chuyển trạng thái idle
        else if (isDetectingWall || !isDetectingLedge)
        {
            slime.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(slime.idleState);
        }

    }
}
