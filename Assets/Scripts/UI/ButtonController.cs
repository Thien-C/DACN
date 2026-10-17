using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private int level;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null && level > 0)
        {
            bool isUnlocked = GameManager.Instance.IsLevelUnlocked(level);
            button.interactable = isUnlocked;

            // Xử lý visual khi bị khóa
            if (!isUnlocked)
            {
                var colors = button.colors;
                colors.disabledColor = new Color(0.4f, 0.4f, 0.4f, 0.8f);
                button.colors = colors;
            }
        }
    }

    // Phát âm thanh UI (nếu cần)
    private void PlaySfx()
    {
        SoundManager.Instance.PlaySound(GameEnum.ESound.uiClick);
    }

    // Bắt đầu game - chuyển sang menu chọn level
    public void StartGame()
    {
        PlaySfx();
        UIManager.Instance.ShowGameMenuLevelUI();
    }

    // Mở setting
    public void OpenSettings()
    {
        PlaySfx();
        UIManager.Instance.ShowGameSettingUI();
    }

    // Đóng setting
    public void CloseSettings()
    {
        PlaySfx();
        GameManager.Instance.ExitButton();
    }

    // Tải level
    public void LoadLevel()
    {
        PlaySfx();
        GameManager.Instance.LoadLevel(level);
    }

    // Nút Resume trong pause menu
    public void ResumeGame()
    {
        PlaySfx();
        GameManager.Instance.ResumeGame();
    }

    // Nút Pause
    public void PauseGame()
    {
        PlaySfx();
        GameManager.Instance.PauseGame();
    }
    // Nút Continue (Resume) trong pause menu
    public void ContinueGame()
    {
        PlaySfx();
        GameManager.Instance.ResumeGame();
    }

    // Nút Restart game
    public void RestartGame()
    {
        PlaySfx();
        GameManager.Instance.RestartGame();
    }

    // Nút Quit game
    public void QuitGame()
    {
        PlaySfx();
        GameManager.Instance.QuitGame();
    }

    // Trở về main menu
    public void ReturnMainMenu()
    {
        PlaySfx();
        GameManager.Instance.ReturnMainMenu();
        GameManager.Instance.ResetScore();
    }
}