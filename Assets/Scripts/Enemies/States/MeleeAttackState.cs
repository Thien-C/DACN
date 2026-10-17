using Hung.Combat.Damage;
using UnityEngine;
using Hung.Combat.KnockBack;
using Hung.Combat.PoiseDamage;
using Hung.CoreSystem;
public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;
    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosstion,D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosstion)
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
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosstion.position,stateData.attackRadius,stateData.whatIsPlayer);
        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log("AAA");
                damageable.Damage(new DamageData(stateData.attackDamage, core.Root));
            }
            IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();
            if (knockBackable != null)
            {
                knockBackable.KnockBack(new KnockBackData(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection, core.Root));
            }

            if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.DamagePoise(new PoiseDamageData(stateData.PoiseDamage, core.Root));
            }
        }
    }
}
