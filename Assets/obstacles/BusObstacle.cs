using UnityEngine;

public class BusObstacle : MonoBehaviour
{
    public float speed = 3f;
    private gameManager gm;

    void Start()
    {
        gm = FindFirstObjectByType<gameManager>();
    }

    void Update()
    {
        if (gm == null || !gm.gameStarted) return;

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}