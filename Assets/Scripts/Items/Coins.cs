using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasLanded = false; // Tránh trigger nhiều lần
    private bool isPickup = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isPickup = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasLanded)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            hasLanded = true; // Đánh dấu đã chạm đất
        }

        // Logic cho Player không bị ảnh hưởng bởi hasLanded
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPickup)
            {
                isPickup = true;
                SoundManager.Instance.PlaySound(GameEnum.ESound.itemCollectedSound);
                gameObject.SetActive(false);
                GameManager.Instance.AddScore(20);
                //Debug.Log($"Coin collected! Current Score: {GameManager.Instance.playerScore}");
                Destroy(gameObject); // Hủy coin ngay lập tức
            }
        }
    }
}