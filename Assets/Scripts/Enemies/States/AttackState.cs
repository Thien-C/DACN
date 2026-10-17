using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class AttackState : State
{
    protected Transform attackPosstion;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;


    protected bool isPlayerInFlyMinArgoRange;
    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosstion) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosstion = attackPosstion;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInFlyMinArgoRange = entity.CheckPlayerInFlyMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.atsm.attackState = this;
        isAnimationFinished = false;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public virtual void TriggerAttack()
    {
    }
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
