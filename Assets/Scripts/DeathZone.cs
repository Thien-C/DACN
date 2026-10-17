using Hung.Combat.Damage;
using Hung.Combat.KnockBack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")) // Kiểm tra nếu Player chạm vào hitbox
        {
            IDamageable damageable = collision.gameObject.GetComponentInChildren<IDamageable>();
            if (damageable != null)
            {
                Debug.Log("Enemy va cham" +collision.gameObject.name);
                damageable.Damage(new DamageData(200, collision.gameObject));
            }
        }
    }
}
