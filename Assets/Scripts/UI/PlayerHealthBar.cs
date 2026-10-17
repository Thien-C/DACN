using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthBar : SingletonBehavior<PlayerHealthBar>
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Stats stats; // Để gán trong Inspector nếu cần, nhưng sẽ được cập nhật động
    private float lastHealthValue;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartCoroutine(InitializeWhenPlayerAvailable());
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            StartCoroutine(InitializeWhenPlayerAvailable());
        }
    }

    private IEnumerator InitializeWhenPlayerAvailable()
    {
        yield return null;

        while (SceneManager.GetActiveScene().buildIndex != 0 && Player.Instance == null)
        {
            yield return null;
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            stats = null;
            InitializeHealthBar();
        }
    }

    private void Update()
    {
        if (stats == null || healthSlider == null)
        {
            FindPlayerStats(); // Tìm lại Stats nếu bị mất
            return;
        }

        if (stats.Health.CurrentValue != lastHealthValue)
        {
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
        }
    }

    public void SetStats(Stats newStats)
    {
        stats = newStats;
        if (stats != null && healthSlider != null)
        {
            healthSlider.maxValue = stats.Health.MaxValue;
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
        }
    }

    public void UpdateHealthBar()
    {
        if (stats != null)
        {
            stats.Health.Init();
            InitializeHealthBar();
        }
    }

    private void InitializeHealthBar()
    {
        if (stats == null)
        {
            FindPlayerStats();
        }

        if (stats != null && healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.maxValue = stats.Health.MaxValue;
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
        }
    }

    private void FindPlayerStats()
    {
        GameObject player = Player.Instance != null
            ? Player.Instance.gameObject
            : GameObject.FindWithTag("Player");

        if (player != null)
        {
            stats = player.GetComponentInChildren<Stats>();
            if (stats != null)
            {
                SetStats(stats); // Cập nhật lại stats và thanh máu
            }
            else
            {
                Debug.LogError("Không tìm thấy Stats trên Player!");
            }
        }
    }
}