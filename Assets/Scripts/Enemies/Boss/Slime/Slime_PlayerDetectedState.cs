using Hung.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime slime;
    private KnockBackReceiver CombatComponent { get => combatComponent ?? core.GetCoreComponent<KnockBackReceiver>(ref combatComponent); }
    private KnockBackReceiver combatComponent;
    public Slime_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Slime slime) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(slime.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(slime.chargeState);
        }
        else if (!isPlayerInMaxArgoRange)
        {
            stateMachine.ChangeState(slime.lookForPlayerState);
        }
        else if (!isDetectingLedge && !CombatComponent.isKnockBackActive)
        {
            Movement?.Flip();
            stateMachine.ChangeState(slime.moveState);
        }
    }

}
