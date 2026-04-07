using UnityEngine;
using System.Collections;
public class FadeAndDestroy : MonoBehaviour
{
        [Header("Timing")]
    public float lifeTime = 2f;      // how long the piece stays fully visible
    public float fadeDuration = 1f;  // how long it takes to fade out

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime);

      
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

      
        if (col != null)
        {
            col.enabled = false;
        }

        float elapsed = 0f;
        Color originalColor = sr.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
