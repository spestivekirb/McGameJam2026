using UnityEngine;

public class shadowFollower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private playerReplay recorder;

    [Header("Replay")]
    [SerializeField] private float delaySeconds = 1.0f;

    [Header("Movement")]
    [SerializeField] private float speed = 5.1f;
    [SerializeField] private float jumpHeight = 6.7f;

    [Header("Collision")]
    [SerializeField] private LayerMask whatIsGround;

    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;

<<<<<<< Updated upstream
    private bool isGrounded;
=======
    [SerializeField] private bool isGrounded;
>>>>>>> Stashed changes
    [SerializeField] private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        HandleCollision();
        HandleAnimation();
        HandleFlip();
    }

    private void FixedUpdate()
    {
        if (recorder == null) return;

        float t = Time.time - delaySeconds;

        if (!recorder.TryGetFrame(t, out var frame))
            return;

        float targetX = frame.horizontal * speed;

        if (frame.jump)
        {
            rb.linearVelocity = new Vector2(targetX, jumpHeight);

        }
        else
        {
            rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);
        }
    }

    private void HandleAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("X velocity", rb.linearVelocity.x);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("Y velocity", rb.linearVelocity.y);
    }

    private void HandleCollision()
    {
        if (col == null) { isGrounded = false; return; }

        Vector2 checkPoint = new Vector2(col.bounds.center.x, col.bounds.min.y - 0.02f);

        isGrounded = Physics2D.OverlapCircle(checkPoint, 0.08f, whatIsGround);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }
}