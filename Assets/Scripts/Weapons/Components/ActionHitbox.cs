using Hung.CoreSystem;
using System;
using UnityEngine;

namespace Hung.Weapons.Components 
{
    public class ActionHitbox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private CoreComp<CoreSystem.Movement> movement;

        private Vector2 offset;

        private Collider2D[] detected;
        private void HandleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.center.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.center.y
                );
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
            {
                return;
            }
            OnDetectedCollider2D?.Invoke(detected);
        }

        protected override void Start()
        {
            base.Start();
            movement = new CoreComp<Movement>(Core);
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDesTroy()
        {
            base.OnDesTroy();

            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null)
            {
                return;
            }
            foreach (var item in data.GetAllAttackData())
            {
                if (!item.Debug)
                {
                    continue;
                }
                //Gizmos.color = Color.red;
                //Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.position,item.HitBox.size);
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
    }
}

