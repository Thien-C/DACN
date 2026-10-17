using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_IdleState : IdleState
{
    private Goblin goblin;
    public Goblin_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinArgoRange)
        {
            stateMachine.ChangeState(goblin.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(goblin.moveState);
        }
    }
}
