using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Entity
{
    public Snail_Move moveState { get; private set; }
    [SerializeField]
    private D_MoveState moveStateData;
    public override void Awake()
    {
        base.Awake();
        moveState = new Snail_Move(this, stateMachine, "move", moveStateData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Kiểm tra nếu Player chạm vào hitbox
        {
            IKnockBackable knocback = collision.GetComponent<IKnockBackable>();
            knocback.KnockBack(new KnockBackData(new Vector2(1, 1), 20, 1, collision.transform.parent.gameObject));
            //Destroy(gameObject);
        }
    }
}
