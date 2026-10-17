using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_DeathState : DeadState
{
    private Slime slime;
    public Slime_DeathState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Slime slime) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.slime = slime;
    }


}
