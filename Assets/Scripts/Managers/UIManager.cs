using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance { get; private set; }

    // UI Panels
    [Header("Game Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject gameHubPanel;
    [SerializeField] private GameObject gameMenuLevelPanel;
    [SerializeField] private GameObject gameSettingPanel;
    [SerializeField] private GameObject gamePausePanel;

    // Text Components
    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI enterPromptText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeUI();
    }

    // Khởi tạo giao diện ban đầu
    private void InitializeUI()
    {
        HideAllUI();
        ShowGameStartUI();

        // Ẩn enter prompt
        if (enterPromptText != null)
        {
            enterPromptText.gameObject.SetActive(false);
        }

        // Cập nhật điểm cao
        UpdateHighScoreDisplay();
    }

    // Các phương thức hiển thị UI
    public void ShowGameStartUI()
    {
        HideAllUI();
        gameStartPanel.SetActive(true);
    }

    public void ShowGameHubUI()
    {
        HideAllUI();
        gameHubPanel.SetActive(true);
        UpdateScore(GameManager.Instance.playerScore);
    }

    public void ShowGameOverUI()
    {
        HideAllUI();
        gameOverPanel.SetActive(true);
        UpdateGameOverUI();
    }

    public void ShowGameMenuLevelUI()
    {
        HideAllUI();
        gameMenuLevelPanel.SetActive(true);
    }

    public void ShowGameSettingUI()
    {
        HideAllUI();
        gameSettingPanel.SetActive(true);
    }

    public void ShowPauseUI()
    {
        HideAllUI();
        gamePausePanel.SetActive(true);
    }

    // Ẩn toàn bộ UI
    public void HideAllUI()
    {
        gameOverPanel.SetActive(false);
        gameStartPanel.SetActive(false);
        gameSettingPanel.SetActive(false);
        gameMenuLevelPanel.SetActive(false);
        gamePausePanel.SetActive(false);
        gameHubPanel.SetActive(false );
    }
    // Cập nhật UI game over
    private void UpdateGameOverUI()
    {
        int currentScore = GameManager.Instance.playerScore;
        UpdateScoreDisplay(gameOverScoreText, currentScore);
        UpdateHighScoreDisplay();
    }

    // Hiển thị/ẩn enter prompt
    public void ShowEnterPrompt(bool show)
    {
        if (enterPromptText != null)
        {
            enterPromptText.gameObject.SetActive(show);
        }
    }

    // Cập nhật điểm số
    public void UpdateScore(int score)
    {
        UpdateScoreDisplay(scoreText, score);
    }

    // Phương thức chung để cập nhật text điểm
    private void UpdateScoreDisplay(TextMeshProUGUI textComponent, int score)
    {
        if (textComponent != null)
        {
            textComponent.text = $"Score: {score}";
        }
    }

    // Cập nhật điểm cao
    private void UpdateHighScoreDisplay()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {highScore}";
        }
    }
}