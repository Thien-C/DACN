using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Idle : IdleState
{
    private Bee bee;
    public Bee_Idle(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Bee bee) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.bee = bee;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.CheckPlayerInFlyMaxAgroRange())
        {
            stateMachine.ChangeState(bee.chargeState); // Phát hiện player → Đuổi theo
        }
    }
}
