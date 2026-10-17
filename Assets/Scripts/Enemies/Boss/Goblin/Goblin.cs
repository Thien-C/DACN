using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Goblin : Entity
{
    public Goblin_IdleState idleState { get; private set; }
    public Goblin_MoveState moveState { get; private set; }
    public Goblin_PlayerDetectedState playerDetectedState { get; private set; }
    public Goblin_ChargeState chargeState { get; private set; }
    public Goblin_LookForPlayer lookForPlayerState { get; private set; }
    public Goblin_MeleeAttackState meleeAttackState { get; private set; }
    public Goblin_DeathState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private Transform meleeAttackPossition;
    public override void Awake()
    {
        base.Awake();
        moveState = new Goblin_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Goblin_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Goblin_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Goblin_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Goblin_LookForPlayer(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Goblin_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPossition, meleeAttackStateData, this);
        deadState = new Goblin_DeathState(this, stateMachine, "dead", deadStateData, this);

    }
    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPossition.position, meleeAttackStateData.attackRadius);
    }
}
