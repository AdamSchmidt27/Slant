using UnityEngine;

public class dogObstacle : MonoBehaviour
{
    [Header("Floating")]
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;

    [Header("Knockback")]
    public float knockbackForce = 12f;
    public Vector2 knockbackDirection = new Vector2(-1f, 0.5f);
    public float hitCooldown = 0.5f;

    private Vector3 startPos;
    private float lastHitTime;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (Time.time < lastHitTime + hitCooldown)
        return;

    if (other.CompareTag("Player"))
    {
        CarriageDownHillMotor motor = other.GetComponent<CarriageDownHillMotor>();

        if (motor != null)
        {
            Vector2 force = knockbackDirection.normalized * knockbackForce;
            motor.ApplyKnockback(force);
            lastHitTime = Time.time;
        }
    }
}
  
}
