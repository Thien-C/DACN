using Hung.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected D_RangedAttackState stateData;
    protected GameObject projectile;
    protected ProjectileBase projectileScript;
    public RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosstion, D_RangedAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosstion)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        // Lấy projectile từ ObjectPoolManager
        projectile = ObjectPoolManager.Instance.GetObject(stateData.projectile, attackPosstion.position, attackPosstion.rotation);
        if (projectile != null)
        {
            projectileScript = projectile.GetComponent<ProjectileBase>();
            projectileScript.Initialize(stateData.projectileDamage, stateData.projectileSpeed, Movement.FacingDirection);
        }
    }

}
