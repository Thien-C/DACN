using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public float damage;
    public float speed;
    protected int facingDirection;
    protected Rigidbody2D rb;
    public string tagName;
    private float lifetime = 3f; // Thời gian tồn tại trước khi tự hủy

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Initialize(float damage, float projectileSpeed, int facingDirection)
    {
        this.damage = damage;
        this.speed = projectileSpeed;
        this.facingDirection = facingDirection;
        CancelInvoke(nameof(ReturnToPool)); // Hủy lịch trình cũ để tránh lỗi
        Invoke(nameof(ReturnToPool), lifetime); // Lên lịch tự hủy sau 'lifetime' giây
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        IKnockBackable knockback = collision.GetComponent<IKnockBackable>();

        if (damageable != null)
        {
            damageable.Damage(new DamageData(damage, collision.transform.parent.gameObject));
        }
        if (knockback != null)
        {
            knockback.KnockBack(new KnockBackData(new Vector2(1, 1), 20, facingDirection, collision.transform.parent.gameObject));
        }

        ReturnToPool();
    }

    protected void ReturnToPool()
    {
        CancelInvoke(nameof(ReturnToPool)); // Hủy hẹn giờ nếu viên đạn va chạm sớm hơn
        rb.linearVelocity = Vector2.zero; // Reset vận tốc để tránh lỗi khi tái sử dụng
        gameObject.SetActive(false);
        ObjectPoolManager.Instance.ReturnObject(gameObject, tagName);
    }
}
