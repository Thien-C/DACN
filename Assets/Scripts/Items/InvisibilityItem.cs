using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InvisibilityItem : MonoBehaviour
{
    [SerializeField] private float invisibilityDuration = 2f; // Thời gian tàng hình (giây)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer playerRenderer = other.GetComponent<SpriteRenderer>();
            Player playerController = other.GetComponent<Player>();

            if (playerRenderer != null && playerController != null)
            {
                // Gọi hàm tàng hình từ Player
                playerController.ActivateInvisibility(playerRenderer, invisibilityDuration);
                // Xóa item sau khi kích hoạt
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player không có SpriteRenderer hoặc Player script!");
            }
        }
    }
}
