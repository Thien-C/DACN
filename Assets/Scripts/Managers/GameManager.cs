using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hung.CoreSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerScore { get; private set; }

    [SerializeField] private Player playerPrefab;

    // Biến để theo dõi trạng thái pause
    public bool IsPaused { get; private set; } = false;


    private Vector3 lastCheckpointPosition = Vector3.zero;
    private bool hasCheckpoint = false;
    private const string UNLOCKED_LEVEL_KEY = "UnlockedLevel";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        Time.timeScale = 1f;
        IsPaused = false;

        // Khi quay về MainMenu (index 0)
        if (scene.buildIndex == 0)
        {
            UIManager.Instance.ShowGameStartUI();
            return;
        }

        // Tự động mở khóa màn chơi khi vừa vào màn (nếu chưa mở)
        UnlockLevel(scene.buildIndex);

        if (Player.Instance == null && playerPrefab != null)
        {
            Instantiate(playerPrefab);
        }

        SceneSpawnPoint spawnPoint = FindFirstObjectByType<SceneSpawnPoint>();
        if (Player.Instance != null)
        {
            if (spawnPoint != null)
            {
                Player.Instance.transform.position = spawnPoint.transform.position;
            }
        }

        UIManager.Instance.ShowGameHubUI();
        UIManager.Instance.UpdateScore(playerScore);
    }

    private void Start()
    {
        playerScore = 0;
        UIManager.Instance.UpdateScore(playerScore);
    }

    // Kiểm tra input để pause/unpause
    private void Update()
    {
        // Nếu nhấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }



    // Phương thức pause game
    public void PauseGame()
    {
        if (!IsPaused)
        {
            Time.timeScale = 0f;
            IsPaused = true;
            UIManager.Instance.ShowPauseUI();
        }
    }

    // Phương thức tiếp tục game
    public void ResumeGame()
    {
        if (IsPaused)
        {
            Time.timeScale = 1f;
            IsPaused = false;
            UIManager.Instance.HideAllUI();
            UIManager.Instance.ShowGameHubUI();
        }
    }

    // Phương thức thoát game hoàn toàn
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Nếu đang chạy trên thiết bị thực
            Application.Quit();
#endif
    }

    // Các phương thức khác như cũ
    public void LoadLevel(int level)
    {
        if (IsPaused)
        {
            ResumeGame();
        }

        SaveScore();
        SceneManager.LoadScene(level);
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowGameHubUI();
        SoundManager.Instance.PlayMusic((GameEnum.EMusic)1);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log($"Score increased by {points}. New score: {playerScore}");
        UIManager.Instance.UpdateScore(playerScore);
    }

    public void PlayerDie()
    {
        SaveScore();
        StartCoroutine(ShowGameOverAfterDelay(2f));
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.Instance.ShowGameOverUI();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("LastScore", playerScore);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (playerScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        PlayerPrefs.Save();
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        hasCheckpoint = true;
    }

    // Reset player về checkpoint và hồi máu
    private void ResetToCheckpoint()
    {
        if (hasCheckpoint && Player.Instance != null)
        {
            Player.Instance.Respawn(lastCheckpointPosition);
            // Reset máu của player
            Stats playerStats = Player.Instance.GetComponentInChildren<Stats>();
            if (playerStats != null)
            {
                playerStats.Health.CurrentValue = playerStats.Health.MaxValue;
                PlayerHealthBar.Instance.UpdateHealthBar(); // Cập nhật UI máu
            }
            UIManager.Instance.ShowGameHubUI();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        playerScore = 0;
        hasCheckpoint = false;
        lastCheckpointPosition = Vector3.zero;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetScore()
    {
        playerScore = 0;
        PlayerHealthBar.Instance.UpdateHealthBar();
        UIManager.Instance.UpdateScore(playerScore);
    }

    public void ExitButton()
    {
        if (IsPaused)
        {
            ResumeGame();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UIManager.Instance.ShowGameStartUI();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            UIManager.Instance.HideAllUI();
        }
        else
        {
            UIManager.Instance.ShowGameHubUI();
        }
    }

    public void ReturnMainMenu()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        SceneManager.LoadScene(0);
        SoundManager.Instance.PlayMusic(GameEnum.EMusic.MusicIndex);
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowGameStartUI();
    }

    // Level Unlock Logic
    public int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(UNLOCKED_LEVEL_KEY, 1);
    }

    public void UnlockLevel(int levelIndex)
    {
        int currentUnlocked = GetUnlockedLevel();
        if (levelIndex > currentUnlocked)
        {
            PlayerPrefs.SetInt(UNLOCKED_LEVEL_KEY, levelIndex);
            PlayerPrefs.Save();
            Debug.Log($"Level {levelIndex} unlocked!");
        }
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        // Màn 1 (index 1) mặc định luôn mở
        if (levelIndex <= 1) return true;
        return levelIndex <= GetUnlockedLevel();
    }
}