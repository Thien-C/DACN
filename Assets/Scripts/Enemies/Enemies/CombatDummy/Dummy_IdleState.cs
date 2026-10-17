using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_IdleState : IdleState
{
    private CombatDummy dummy;
    public Dummy_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, CombatDummy dummy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.dummy = dummy;
    }

}
