using UnityEngine;

public class BreakableCrate : MonoBehaviour
{
  
   public GameObject brokenCratePrefab;
    public float breakForceThreshold = 8f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > breakForceThreshold)
        {
            Break();
        }
    }

    void Break()
    {
        GameObject broken = Instantiate(
            brokenCratePrefab,
            transform.position,
            transform.rotation
        );

       
        foreach (Rigidbody2D rb in broken.GetComponentsInChildren<Rigidbody2D>())
        {
            Vector2 randomForce = new Vector2(
                Random.Range(-2f, 2f),
                Random.Range(1f, 3f)
            );
            rb.AddForce(randomForce, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
