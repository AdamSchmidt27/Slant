using UnityEngine;

public class winPlatform : MonoBehaviour
{
    private gameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<gameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.root.CompareTag("Player") || collision.collider.CompareTag("Player"))
        {
            gameManager.WinGame();
        }
    }
}
