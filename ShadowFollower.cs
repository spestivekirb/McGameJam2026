using UnityEngine;
using System.Collections;

public class shadowFollower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private playerReplay recorder;

    [Header("Replay")]
    [SerializeField] private float delaySeconds = 2.0f;

    [Header("Movement")]
    [SerializeField] private float speed = 5.1f;
    [SerializeField] private float jumpHeight = 6.7f;

    [Header("Collision")]
    [SerializeField] private LayerMask whatIsGround;

    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;

    private bool isGrounded;

    private bool isFrozen;
    [SerializeField] private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        isFrozen = false;
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

        if (isFrozen) return;


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

    public void ChangeDelay(float delay)
    {
        delaySeconds = delay;
    }

    public float GetDelay()
    {
        return delaySeconds;
    }

    public void FreezeShadow(float seconds)
    {
        StartCoroutine(FreezeRoutine(seconds));
    }

    private IEnumerator FreezeRoutine(float seconds)
    {
        Vector2 savedVelocity = rb.linearVelocity;
        float savedGravity = rb.gravityScale;
        
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        isFrozen = true;
        // 3. The actual wait timer
        yield return new WaitForSeconds(seconds);


        rb.linearVelocity = savedVelocity;
        rb.gravityScale = savedGravity;
        isFrozen = false;
        
    }
}