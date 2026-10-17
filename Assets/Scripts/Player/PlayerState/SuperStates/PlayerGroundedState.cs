using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class PlayerGroundedState : PlayerState
{
    //Input
    protected int xInput;
    protected int yInput;
    protected bool isTouchingCeiling;

    protected Movement Movement { get => movement ?? core.GetCoreComponent<Movement>(ref movement); }
    private Movement movement;  
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent<CollisionSenses>(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    private bool jumpInput;
    private bool grabInput;
    private bool dashInput;
    //Check
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isDetectingLedge;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Grounded;
            isTouchingWall = CollisionSenses.WallFornt;
            isDetectingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }

    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        //tan cong
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling && player.PrimaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling && player.SecondaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        //nhay
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded && !isTouchingCeiling)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        //bam tuong
        else if (isTouchingWall && grabInput && isDetectingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        //dash
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
