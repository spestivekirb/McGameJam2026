using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    [SerializeField] public bool canMove = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool isAttacking= false;
    [SerializeField] private Transform graphics; // drag player/Animator here

    [Header("Collision properties")]
    [SerializeField] private bool facingRight = true;
    [SerializeField] public bool isGrounded = true;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Attack Properties")]
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackHitbox;
    [SerializeField] private LayerMask whatisEnemy;

    private bool isReloading = false;

    

    // awake is called when the script instance is being loaded, find component or other object for script
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Awake called");
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        replay = GetComponent<playerReplay>();

        if (graphics == null)
            graphics = animator.transform;
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
        handleCollision();
        handleInput();
        handleAnimation();
        handleFlip();
    }

    // slower than Update, calculate physics here, called at fixed time intervals, 50 times per second default
    private void FixedUpdate()
    {
    }

    private void handleInput()
    {
        if (canMove && isAlive) { 
            rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && canJump) // get key down activates when key pressed
            {
                if (isAlive && isGrounded)
                {
                    jump();
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0)) // get key down activates on mouse click
            {
                if (isAlive && isGrounded)
                {
                    //attack();
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

    public void die()
    {
        if (!isAlive) return;
        isAlive = false;
        animator.Play("die");
        StartCoroutine(ReloadAfterDelay(1f));
    }

    private IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void handleFlip()
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
        Vector3 s = graphics.localScale;
        s.x = Mathf.Abs(s.x) * (facingRight ? -1 : 1); 
        graphics.localScale = s;
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

    public void FreezePlayer(float seconds)
    {
        StartCoroutine(FreezeRoutine(seconds));
    }

    private IEnumerator FreezeRoutine(float seconds)
    {
        Vector2 savedVelocity = rb.linearVelocity;
        float savedGravity = rb.gravityScale;
        canMove = false;
        
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        // 3. The actual wait timer
        yield return new WaitForSeconds(seconds);


        rb.linearVelocity = savedVelocity;
        rb.gravityScale = savedGravity;
        
        canMove = true;
    }
    
}