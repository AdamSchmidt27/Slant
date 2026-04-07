using UnityEngine;

public class BabyRecoveryZone : MonoBehaviour
{
     [Header("References")]
    public Transform baby;
    public Rigidbody2D babyRb;
    public Transform pullTarget;
    public CarriageController2D carriageController;

    [Header("Pull Settings")]
    public float pullStrength = 6f;
    public float maxPullDistance = 2f;
    public float upwardLimit = 1.5f;

    [Header("Velocity Damping")]
    public float extraDrag = 0.98f;

    private void Reset()
    {
        carriageController = GetComponentInParent<CarriageController2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (carriageController != null && !carriageController.gameStarted) return;
        if (!other.CompareTag("Baby")) return;
        if (baby == null || babyRb == null || pullTarget == null) return;

        Vector2 toTarget = (Vector2)pullTarget.position - babyRb.position;
        float distance = toTarget.magnitude;

        if (distance > maxPullDistance) return;

        
        if (baby.position.y < pullTarget.position.y - upwardLimit) return;

        float strengthByDistance = 1f - (distance / maxPullDistance);
        Vector2 force = toTarget.normalized * (pullStrength * strengthByDistance);

        babyRb.AddForce(force, ForceMode2D.Force);

        
        babyRb.linearVelocity *= extraDrag;
    }
}
