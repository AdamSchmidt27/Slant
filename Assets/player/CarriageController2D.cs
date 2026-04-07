using UnityEngine;
using UnityEngine.InputSystem;

public class CarriageController2D : MonoBehaviour
   {
     [Header("Start")]
    public bool gameStarted = false;

   
    public GameObject startPrompt;

    [Header("Jump")]
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("Rotation")]
    
    public float torque = 8f;

    public bool rotateOnlyInAir = false;

   
    [Range(0f, 1f)]
    public float groundTorqueMultiplier = 0.25f;

  
    public float maxAngularSpeed = 180f;

    [Header("Stability")]
    
    public float recommendedAngularDrag = 3f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool hasJumped;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (!gameStarted) return;
        if (Keyboard.current == null) return;

        bool groundedNow = false;
        if (groundCheck != null)
            groundedNow = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        isGrounded = groundedNow;

        if (isGrounded)
            hasJumped = false;

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            isGrounded &&
            !hasJumped)
        {
            hasJumped = true;

            Vector2 v = rb.linearVelocity;
            if (v.y < 0) v.y = 0;
            rb.linearVelocity = v;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (!gameStarted) return;
        if (Keyboard.current == null) return;

        float dir = 0f;
        if (Keyboard.current.leftArrowKey.isPressed) dir = 1f;
        if (Keyboard.current.rightArrowKey.isPressed) dir = -1f;

        if (dir != 0f)
        {
            if (!rotateOnlyInAir || !isGrounded)
            {
                float mult = isGrounded ? groundTorqueMultiplier : 1f;
                rb.AddTorque(dir * torque * mult, ForceMode2D.Force);
            }
        }

        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
    }

    public void StartGame()
    {
        gameStarted = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (startPrompt != null)
            startPrompt.SetActive(false);
    }

    public void StopGame()
    {
        gameStarted = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
   