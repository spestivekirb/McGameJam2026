using UnityEngine;

public class Playermove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    private playerReplay replay;


    [Header("Player Settings")]
    [SerializeField] private string playerName;
    [SerializeField] private float jumpHeight = 6.7f;
    [SerializeField] private float speed = 5.1f; // when you have float value, put f after it, serializeField makes private variable visible and editable in inspector
    private bool isAlive = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool isAttacking= false;

    [Header("Collision properties")]
    [SerializeField] private bool facingRight = true;
    [SerializeField] public bool isGrounded = true;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Attack Properties")]
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackHitbox;
    [SerializeField] private LayerMask whatisEnemy;

    // awake is called when the script instance is being loaded, find component or other object for script
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Awake called");
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        replay = GetComponent<playerReplay>();
    }
    // start is called before first update is called after monobehaviour is created, initialize variables and entities here
    private void Start()
    {
        playerName = "Player 1";
        Debug.Log("Welcome " + playerName);
    }
    // update is called once per frame, check input of player
    void Update()
    {
        Debug.Log("Update called");
        handleCollision();
        handleInput();
        handleAnimation();
        handleFlip();
    }

    // slower than Update, calculate physics here, called at fixed time intervals, 50 times per second default
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate called");
    }

    private void handleInput()
    {
        if (canMove) { 
            rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && canJump) // get key down activates when key pressed
            {
                if (isAlive && isGrounded)
                {
                    jump();
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0)) // get key down activates on mouse  click
            {
                if (isAlive && isGrounded)
                {
                    attack();
                }
            }
        } else {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

       private void handleAnimation()
    {
        animator.SetFloat("X velocity", rb.linearVelocity.x);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("Y velocity", rb.linearVelocity.y); 
    }

    private void attack() {
        if (isGrounded && isAlive && !isAttacking) {
            isAttacking = true;
            animator.SetTrigger("attack");
        }
    }

    public void attackEnd() {
        isAttacking = false;
    }

    public void DealDamage() {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitbox.position, attackRange, whatisEnemy);
        Debug.Log("Hit " + hitEnemies.Length + " enemies.");
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>()?.TakeDamage(20);
        }
    }
    
    
    private void handleCollision()
    {
        if (col == null) { isGrounded = false; return; }

        Vector2 checkPoint = new Vector2(
            col.bounds.center.x,
            col.bounds.min.y - 0.02f
        );

        isGrounded = Physics2D.OverlapCircle(
            checkPoint,
            0.08f,
            whatIsGround
        );

    }

    private void jump()
    {
        isGrounded = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight); 
        if (replay != null)
            replay.QueueJump();
        Debug.Log(playerName + " Jumped");
    }

    private void handleFlip()
    {
        if (!canMove || isAttacking) return;
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

    public void ToggleMovement(bool enabled) {
        this.canMove = enabled;
        this.canJump = enabled;
    }

    private void OnDrawGizmos() {
        if (attackHitbox == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitbox.position, attackRange);
    }
    
}