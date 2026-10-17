using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AG_IdleState : IdleState
{
    private AngryPig pig;
    public AG_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData,AngryPig pig) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.pig = pig;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinArgoRange)
        {
            stateMachine.ChangeState(pig.chargeState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(pig.moveState);
        }
    }
}
