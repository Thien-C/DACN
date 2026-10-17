using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
    public Slime_IdleState idleState { get; private set; }
    public Slime_MoveState moveState { get; private set; }
    public Slime_MeleeAttackState meleeAttackState { get; private set; }
    public Slime_PlayerDetectedState playerDetectedState { get; private set; }
    public Slime_ChargeState chargeState { get; private set; }
    public Slime_LookForPlayerState lookForPlayerState { get; private set; }
    public Slime_DeathState deadState { get; private set; }

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
    private SlimeCPManager manager;

    public override void Awake()
    {
        base.Awake();

        moveState = new Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Slime_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Slime_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Slime_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPossition, meleeAttackStateData, this);
        deadState = new Slime_DeathState(this, stateMachine, "dead", deadStateData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    public void Init(SlimeCPManager _manager)
    {
        manager = _manager;
        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPossition.position, meleeAttackStateData.attackRadius);
    }

    public void SpawnSlime(Slime spawnPrefab, int count, float radius)
    {
        List<GameObject> listSpawn = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            float angle = i * (180f / count); // Chia đều góc theo số lượng spawn
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector2 spawnPosition = (Vector2)transform.position + direction * radius;
            Quaternion rotation = Quaternion.identity; // Giữ nguyên hướng hoặc điều chỉnh nếu cần
            Slime go = Instantiate(spawnPrefab, spawnPosition, rotation);
            go.Init(manager);
            manager.AddSmallSlime(go.gameObject);
        }
        //manager.OpenSpawnSmallSlime();
    }
}
