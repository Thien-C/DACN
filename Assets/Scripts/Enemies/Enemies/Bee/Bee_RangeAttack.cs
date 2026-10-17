using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_RangeAttack : RangedAttackState
{
    private Bee bee;
    public Bee_RangeAttack(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosstion, D_RangedAttackState stateData, Bee bee) : base(entity, stateMachine, animBoolName, attackPosstion, stateData)
    {
        this.bee = bee;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (!entity.CheckPlayerInFlyMinAgroRange())
            {
                stateMachine.ChangeState(bee.chargeState); // Nếu player chạy xa → Quay về Chase
                return;
            }

            if (!entity.CheckPlayerInFlyMaxAgroRange())
            {
                stateMachine.ChangeState(bee.idleState); // Nếu player biến mất → Quay về Idle
                return;
            }
        }
    }

}
