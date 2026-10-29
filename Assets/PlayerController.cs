using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Eğilme Ayarları")]
    public float crouchSpeed = 4f;
    public Vector2 crouchColliderSize = new Vector2(1f, 0.5f);
    public Vector2 crouchColliderOffset = new Vector2(0f, -0.25f);

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private bool isGrounded;
    private bool isCrouching;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;
    }

    void Update()
    {
        // Zemin Kontrolü
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Eğilme Kontrolü
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Crouch();
        }
        else if (isCrouching)
        {
            StopCrouch();
        }

        // Zıplama Kontrolü
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = isCrouching ? crouchSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Crouch()
    {
        isCrouching = true;
        col.size = crouchColliderSize;
        col.offset = crouchColliderOffset;
    }

    void StopCrouch()
    {
        isCrouching = false;
        col.size = originalColliderSize;
        col.offset = originalColliderOffset;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}