using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using System.Collections;
using UnityEngine;

namespace Hung.Projectiles
{
    public class Projectile : ProjectileBase
    {
        public override void Initialize(float damage, float projectileSpeed, int facingDirection)
        {
            base.Initialize(damage, projectileSpeed, facingDirection);
            rb.linearVelocity = transform.right * speed; // Đặt vận tốc ngay khi bắn
        }
        private void FixedUpdate()
        {
            if (rb.linearVelocity == Vector2.zero)
            {
                rb.linearVelocity = transform.right * speed;
            }
        }
    }
}
