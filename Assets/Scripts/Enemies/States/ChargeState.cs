using Hung.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;


    protected D_ChargeState stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeOverTime;
    protected bool performCloseRangeAction;


    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinArgoRange = entity.CheckPlayerInMinAgroRange();
        if (CollisionSenses)
        {
            isDetectingWall = CollisionSenses.WallFornt;
            isDetectingLedge = CollisionSenses.LedgeVertical;
        }
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        isChargeOverTime = false;
        //khi chuyển sang trạng thái này tốc độ của enemy sẽ thay đổi
        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeOverTime = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
