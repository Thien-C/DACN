using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeItem : MonoBehaviour
{
    [SerializeField] private float speedChangeDuration = 3f; // Thời gian hiệu ứng kéo dài (giây)
    [SerializeField] private float speedMultiplier = 2f;     // Hệ số thay đổi tốc độ (2 = nhanh gấp đôi, 0.5 = chậm bằng nửa)
    [SerializeField] private bool speedUp = true;           // True = tăng tốc, False = giảm tốc

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerController = other.GetComponent<Player>();
            if (playerController != null)
            {
                playerController.ActivateSpeedChange(speedMultiplier, speedChangeDuration, speedUp);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player không có Player script!");
            }
        }
    }
}