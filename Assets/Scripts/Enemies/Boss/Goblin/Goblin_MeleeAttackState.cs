using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_MeleeAttackState : MeleeAttackState
{
    private Goblin goblin;
    public Goblin_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosstion, D_MeleeAttackState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, attackPosstion, stateData)
    {
        this.goblin = goblin;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
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
