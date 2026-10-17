using UnityEngine;

public class BeeBullet : ProjectileBase
{
    public override void Initialize(float damage, float projectileSpeed, int facingDirection)
    {
        base.Initialize(damage, projectileSpeed, facingDirection);
        rb.linearVelocity = Vector2.down * speed; // Đặt vận tốc ngay khi bắn
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity == Vector2.zero)
        {
            rb.linearVelocity = Vector2.down * speed;
        }
    }
}
