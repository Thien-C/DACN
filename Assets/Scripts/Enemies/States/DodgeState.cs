using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class DodgeState : State
{
    protected D_DodgeState stateData;
    protected bool performCloseRangeActicon;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;

    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeActicon = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Grounded;
        }
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

        Movement?.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
