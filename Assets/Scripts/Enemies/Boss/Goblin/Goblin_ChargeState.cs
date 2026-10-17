using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_ChargeState : ChargeState
{
    private Goblin goblin;
    public Goblin_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(goblin.lookForPlayerState);
        }
        else if (isChargeOverTime)
        {
            if (isPlayerInMinArgoRange)
            {
                stateMachine.ChangeState(goblin.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(goblin.lookForPlayerState);
            }
        }
    }

}
