using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_DeathState : DeadState
{
    private Goblin goblin;
    public Goblin_DeathState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
    }
}
