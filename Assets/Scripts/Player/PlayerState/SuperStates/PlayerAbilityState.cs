using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    private bool isGrounded;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Grounded;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }
}
