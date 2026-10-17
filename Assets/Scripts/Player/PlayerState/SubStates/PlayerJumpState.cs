using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpLeft = playerData.amountOfJump;
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySound(GameEnum.ESound.jump);
        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfJumpLeft() => amountOfJumpLeft = playerData.amountOfJump;
    public void DecreaseAmountOfJumpLeft() => amountOfJumpLeft--;
}
