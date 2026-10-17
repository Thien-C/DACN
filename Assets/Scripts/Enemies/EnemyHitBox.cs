using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using UnityEngine;
using Hung.CoreSystem;
public class EnemyHitBox : MonoBehaviour
{
    public int damage = 20;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Chạm vào Player");
    //        IDamageable damageable = collision.GetComponent<IDamageable>();
    //        IKnockBackable knockback = collision.GetComponent<IKnockBackable>();
    //        Debug.Log("Chạm vào ");
            
    //        knockback.KnockBack(new KnockBackData(new Vector2(1, 2), 20, transform.parent.GetComponent<Entity>().Movement.FacingDirection, core.Root));

    //        damageable.Damage(damage);
    //    }
    //}
}
