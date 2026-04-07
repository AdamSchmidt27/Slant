using UnityEngine;

public class running : MonoBehaviour
{
     public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
