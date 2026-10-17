using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : Entity
{

    public Dummy_IdleState idleState { get; private set; }
    [SerializeField]
    private D_IdleState idleStateData;
    public override void Awake()
    {
        base.Awake();
        idleState = new Dummy_IdleState(this, stateMachine, "idle", idleStateData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
}
