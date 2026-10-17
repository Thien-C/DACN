using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

//Tìm kiếm player
public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool turnImmediately;

    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;


    protected float lastTurnsTime;

    protected int amountOfTurnsDone;
    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnsTime = startTime;
        amountOfTurnsDone = 0;

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

        if (turnImmediately)
        {
            Movement?.Flip();
            lastTurnsTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        //nếu thời gian lật lần 1 flip + thời gian giữa các lần flip và chưa lật xong || 1 turn = 2 lần flip
        else if (Time.time >= lastTurnsTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            Movement?.Flip();
            lastTurnsTime = Time.time;
            amountOfTurnsDone++;
        }
        if(amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }
        if(Time.time >= lastTurnsTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
