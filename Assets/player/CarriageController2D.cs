using UnityEngine;
using UnityEngine.InputSystem;

public class CarriageController2D : MonoBehaviour
   {
    [Header("Jump")]
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("Rotation")]
    [Tooltip("Base rotation strength. Start low (5–15).")]
    public float torque = 8f;

    [Tooltip("If true, rotation input only works while airborne.")]
    public bool rotateOnlyInAir = false;

    [Tooltip("How much weaker rotation is while grounded (0–1).")]
    [Range(0f, 1f)]
    public float groundTorqueMultiplier = 0.25f;

    [Tooltip("Clamp max spin speed (degrees/second).")]
    public float maxAngularSpeed = 180f;

    [Header("Stability")]
    [Tooltip("Recommended Rigidbody2D Angular Drag is ~2–6 for controllable spins.")]
    public float recommendedAngularDrag = 3f; // informational; set in Rigidbody2D Inspector

    private Rigidbody2D rb;
    private bool isGrounded;

    // Single-jump lock: once you jump, you must land to jump again
    private bool hasJumped;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ground check
        bool groundedNow = false;
        if (groundCheck != null)
            groundedNow = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        isGrounded = groundedNow;

        // Reset jump when you land (like most 2D platformers)
        if (isGrounded)
            hasJumped = false;

        // Jump (Space) - allowed only if grounded AND you haven't jumped since last landing
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame &&
            isGrounded &&
            !hasJumped)
        {
            hasJumped = true;

            // Cancel downward velocity so jump height feels consistent
            Vector2 v = rb.linearVelocity;
            if (v.y < 0) v.y = 0;
            rb.linearVelocity = v;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (Keyboard.current == null) return;

        float dir = 0f;
        if (Keyboard.current.leftArrowKey.isPressed) dir = 1f;
        if (Keyboard.current.rightArrowKey.isPressed) dir = -1f;

        if (dir != 0f)
        {
            // Optional: only rotate in air
            if (!rotateOnlyInAir || !isGrounded)
            {
                // Less rotation while grounded, full control in air
                float mult = isGrounded ? groundTorqueMultiplier : 1f;
                rb.AddTorque(dir * torque * mult, ForceMode2D.Force);
            }
        }

        // Clamp spin speed so it never becomes uncontrollable
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}