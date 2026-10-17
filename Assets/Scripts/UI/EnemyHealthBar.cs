using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 offset;

    private Stats stats;
    private float lastHealthValue;

    private void Start()
    {
        stats = GetComponentInParent<Stats>(); // Lấy Stats của quái

        healthSlider.maxValue = stats.Health.MaxValue;
        healthSlider.value = stats.Health.CurrentValue;
        lastHealthValue = healthSlider.value;

        UpdateHealthBar();
    }

    private void Update()
    {
        if (stats.Health.CurrentValue != lastHealthValue)
        {
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
            UpdateHealthBar();
        }
        // Cập nhật vị trí thanh máu theo quái
        healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    private void UpdateHealthBar()
    {
        healthSlider.gameObject.SetActive(stats.Health.CurrentValue < stats.Health.MaxValue);
    }
}
