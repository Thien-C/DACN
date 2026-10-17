using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AG_ChargeState : ChargeState
{
    private AngryPig pig;
    public AG_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, AngryPig pig) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.pig = pig;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isPlayerInMinArgoRange)
        {
            stateMachine.ChangeState(pig.idleState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(pig.idleState);
        }
    }
}
