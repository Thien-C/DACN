using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hung.CoreSystem;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    private KnockBackReceiver CombatComponent { get => combatComponent ?? core.GetCoreComponent<KnockBackReceiver>(ref combatComponent); }
    private KnockBackReceiver combatComponent;

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxArgoRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge && !CombatComponent.isKnockBackActive)
        {
            Movement?.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }

}
