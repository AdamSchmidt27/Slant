using UnityEngine;

public class CarriageDownHillMotor : MonoBehaviour
{
    [Header("Speed Control")]
    public float minDownhillSpeed = 6f;
    public float maxDownhillSpeed = 14f;

    
    [Range(0f, 45f)]
    public float minSlopeAngle = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Knockback")]
    public float knockbackDisableTime = 0.35f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 groundNormal = Vector2.up;
    private float knockbackTimer = 0f;

    private CarriageController2D controller;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CarriageController2D>();
    }

    void FixedUpdate()
    {
        if (controller == null || !controller.gameStarted) return;

        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            return;
        }

        CheckGround();

        if (!isGrounded) return;

        float slopeAngle = Vector2.Angle(groundNormal, Vector2.up);

       
        if (slopeAngle < minSlopeAngle) return;

        Vector2 slopeDir = new Vector2(groundNormal.y, -groundNormal.x).normalized;

       
        if (Vector2.Dot(slopeDir, Vector2.down) < 0f)
            slopeDir *= -1f;

        Vector2 currentVelocity = rb.linearVelocity;

        float tangentSpeed = Vector2.Dot(currentVelocity, slopeDir);
        float clampedTangentSpeed = Mathf.Clamp(tangentSpeed, minDownhillSpeed, maxDownhillSpeed);

      
        Vector2 tangentVelocity = slopeDir * tangentSpeed;
        Vector2 otherVelocity = currentVelocity - tangentVelocity;

        rb.linearVelocity = otherVelocity + slopeDir * clampedTangentSpeed;
    }

    void CheckGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (hit != null)
        {
            isGrounded = true;

            RaycastHit2D rayHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, groundLayer);
            if (rayHit.collider != null)
                groundNormal = rayHit.normal;
            else
                groundNormal = Vector2.up;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector2.up;
        }
    }

    public void ApplyKnockback(Vector2 force)
    {
        knockbackTimer = knockbackDisableTime;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}