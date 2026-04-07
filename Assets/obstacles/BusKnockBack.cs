using UnityEngine;

public class BusKnockBack : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float knockbackForce = 10f;
    public float upwardForce = 3f;
    public float hitCooldown = 0.4f;

    private float lastHitTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < lastHitTime + hitCooldown)
            return;

        if (other.CompareTag("Player"))
        {
            CarriageDownHillMotor motor = other.GetComponent<CarriageDownHillMotor>();

            if (motor != null)
            {
                Vector2 dir = new Vector2(-1f, 0.5f).normalized;

                Vector2 force = new Vector2(
                    dir.x * knockbackForce,
                    upwardForce
                );

                motor.ApplyKnockback(force);

                lastHitTime = Time.time;
            }
        }
    }
}