using Hung.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_PlayerDetectedState : PlayerDetectedState
{
    private Goblin goblin;
    private KnockBackReceiver CombatComponent { get => combatComponent ?? core.GetCoreComponent<KnockBackReceiver>(ref combatComponent); }
    private KnockBackReceiver combatComponent;
    public Goblin_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(goblin.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(goblin.chargeState);
        }
        else if (!isPlayerInMaxArgoRange)
        {
            stateMachine.ChangeState(goblin.lookForPlayerState);
        }
        else if (!isDetectingLedge && !CombatComponent.isKnockBackActive)
        {
            Movement?.Flip();
            stateMachine.ChangeState(goblin.moveState);
        }
    }
}
