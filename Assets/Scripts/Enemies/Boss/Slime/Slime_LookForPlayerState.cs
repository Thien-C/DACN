using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_LookForPlayerState : LookForPlayerState
{
    private Slime slime;
    public Slime_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Slime slime) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.slime = slime;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(slime.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(slime.moveState);
        }
    }
}
