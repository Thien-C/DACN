using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_LookForPlayer : LookForPlayerState
{
    private Goblin goblin;
    public Goblin_LookForPlayer(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(goblin.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(goblin.moveState);
        }
    }
}
