using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BabyCollision : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");

        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
