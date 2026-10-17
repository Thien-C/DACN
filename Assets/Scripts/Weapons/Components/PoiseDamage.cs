using UnityEngine;
using Hung.Combat.PoiseDamage;

namespace Hung.Weapons.Components
{
    public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {

        private ActionHitbox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
                {
                    poiseDamageable.DamagePoise(new Combat.PoiseDamage.PoiseDamageData(currentAttackData.Amount, Core.Root));
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitbox>();
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }
        protected override void OnDesTroy()
        {
            base.OnDesTroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }

    }

}
