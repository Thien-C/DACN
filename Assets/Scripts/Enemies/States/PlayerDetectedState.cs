using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isPlayerInMaxArgoRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;
 
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxArgoRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxArgoRange = entity.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        if (CollisionSenses)
        {
            isDetectingLedge = collisionSenses.LedgeVertical;
        }
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
