using UnityEngine;
using UnityEngine.InputSystem;
public class gameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool gameStarted = false;
    public bool gameWon = false;
    public bool gameOver = false;

    [Header("UI")]
    public GameObject startPanel;
    public GameObject winPanel;

    void Update()
    {
        
        if (!gameStarted && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;

        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }

    public void WinGame()
    {
        gameWon = true;
        Time.timeScale = 0f;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }
}
