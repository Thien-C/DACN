using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Tên scene tiếp theo (gán trong Inspector)
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            UIManager.Instance.ShowEnterPrompt(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance.ShowEnterPrompt(false); // Ẩn text từ UIManager
        }
    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextLevel();
            UIManager.Instance.ShowEnterPrompt(false);
        }
    }
    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            GameManager.Instance.SaveScore(); // Lưu điểm trước khi qua màn
            SceneManager.LoadScene(nextSceneName); // Load scene tiếp theo
            UIManager.Instance.ShowGameHubUI(); // Hiển thị Game Hub ở màn mới
        }
        else
        {
            Debug.LogError("Next scene name không được gán trong Inspector!");
        }
    }
}