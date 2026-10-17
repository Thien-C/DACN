using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class AngryPig : Entity
{
    public AG_IdleState idleState {  get; private set; }    
    public AG_MoveState moveState { get; private set; }
    public AG_ChargeState chargeState { get; private set; }
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Awake()
    {
        base.Awake();
        moveState = new AG_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new AG_IdleState(this, stateMachine, "idle", idleStateData, this);
        chargeState = new AG_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Kiểm tra nếu Player chạm vào hitbox
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            IKnockBackable knocback = collision.GetComponent<IKnockBackable>();
            if (damageable != null)
            {
                damageable.Damage(new DamageData(20, collision.transform.parent.gameObject));
                knocback.KnockBack(new KnockBackData(new Vector2(1, 1), 20, 1, collision.transform.parent.gameObject));
            }
            //Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        stateMachine.Initialize(moveState);

    }
}
