using UnityEngine;
using Hung.CoreSystem;
using Hung.Combat.KnockBack;
namespace Hung.Weapons.Components
{
    public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
    {
        private ActionHitbox hitBox;

        private Movement movement;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IKnockBackable knockBackable))
                {
                    knockBackable.KnockBack(new Combat.KnockBack.KnockBackData(currentAttackData.Angle, currentAttackData.Strength, movement.FacingDirection,Core.Root));
                }
            }
        }
        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitbox>();
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
            movement = Core.GetCoreComponent<Movement>();
        }

        protected override void OnDesTroy()
        {
            base.OnDesTroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }

    }
}
