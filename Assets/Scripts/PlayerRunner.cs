using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerRunner : MonoBehaviour {
    public float runSpeed = 6f;
    public float jumpForce = 10f;
    public bool autoRun = true;              // toggle in Inspector
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;

    Rigidbody2D rb; bool jumpQueued;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
        rb.freezeRotation = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) jumpQueued = true;
    }

    void FixedUpdate() {
        float xVel = autoRun ? runSpeed : Input.GetAxisRaw("Horizontal") * runSpeed;
        rb.linearVelocity = new Vector2(xVel, rb.linearVelocity.y);

        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
        if (jumpQueued && grounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        jumpQueued = false;
    }
}
