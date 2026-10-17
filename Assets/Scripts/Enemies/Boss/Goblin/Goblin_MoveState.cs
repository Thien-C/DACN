using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_MoveState : MoveState
{
    private Goblin goblin;
    public Goblin_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(goblin.playerDetectedState);
        }

        // nếu kẻ thù chạm tường hoặc không ở trên mặt đất thì chuyển trạng thái idle
        else if (isDetectingWall || !isDetectingLedge)
        {
            goblin.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(goblin.idleState);
        }

    }
}
