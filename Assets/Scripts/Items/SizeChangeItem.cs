using UnityEngine;

public class SizeChangeItem : MonoBehaviour
{
    [SerializeField] private float sizeChangeDuration = 3f; // Thời gian hiệu ứng kéo dài (giây)
    [SerializeField] private float scaleFactor = 2f;       // Hệ số thay đổi kích thước (2 = to gấp đôi, 0.5 = nhỏ bằng nửa)
    [SerializeField] private bool enlarge = true;          // True = phóng to, False = thu nhỏ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerController = other.GetComponent<Player>();
            if (playerController != null)
            {
                // Kích hoạt hiệu ứng thay đổi kích thước

                //playerController.ActivateSizeChange(scaleFactor, sizeChangeDuration, enlarge);

                // Xóa item sau khi sử dụng
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player không có Player script!");
            }
        }
    }
}