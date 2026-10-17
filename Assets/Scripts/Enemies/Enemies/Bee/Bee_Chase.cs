using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Chase : ChaseState
{
    private Bee bee;
    public Bee_Chase(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Bee bee) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.bee = bee;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!entity.CheckPlayerInFlyMaxAgroRange())
        {
            stateMachine.ChangeState(bee.idleState); // Mất dấu player → Quay về Idle
            return;
        }

        if (entity.CheckPlayerInFlyMinAgroRange())
        {
            stateMachine.ChangeState(bee.rangeAttackState); // Player đến gần → Attack
            return;
        }
    }

}
