using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Entity
{
    public Bee_Idle idleState { get; private set; }
    public Bee_Chase chargeState { get; private set; }
    public Bee_RangeAttack rangeAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_ChargeState chaseStateData;
    [SerializeField]
    private D_RangedAttackState rangeAttackData;
    [SerializeField]
    private Transform attackPosition;
    public override void Awake()
    {
        base.Awake();
        idleState = new Bee_Idle(this, stateMachine, "idle", idleStateData, this);
        chargeState = new Bee_Chase(this, stateMachine, "chase", chaseStateData, this);
        rangeAttackState = new Bee_RangeAttack(this, stateMachine, "attack", attackPosition, rangeAttackData, this);
    }
    private void Start()
    {
        stateMachine.Initialize(idleState);
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5); // Phạm vi đuổi theo

        Gizmos.color = Color.red;
        Vector2 start = transform.position;
        Vector2 end = start + Vector2.down * entityData.minAgroDistance;
        Gizmos.DrawLine(start, end); // Phạm vi tấn công gần
    }
}
