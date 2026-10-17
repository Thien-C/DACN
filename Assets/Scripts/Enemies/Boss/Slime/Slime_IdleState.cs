using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_IdleState : IdleState
{
    private Slime slime;
    public Slime_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Slime slime) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInMinArgoRange)
        {
            stateMachine.ChangeState(slime.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(slime.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
