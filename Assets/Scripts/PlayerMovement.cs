using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float moveInput;
    private bool isGrounded;
    private bool isAttacking;

    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask enemyLayers;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (!isAttacking)
        {
            if (moveInput > 0)
                sr.flipX = false;
            else if (moveInput < 0)
                sr.flipX = true;

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                DoAttack("attack1", 0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                DoAttack("attack2", 0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                
                DoAttack("attack3", 0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                DoAttack("skill", 0.7f);
            }
        }

        anim.SetFloat("speed", Mathf.Abs(moveInput));
        anim.SetBool("isJumping", !isGrounded);
    }

    void FixedUpdate()
    {
        if (!isAttacking)
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void DoAttack(string triggerName, float attackTime)
    {
        isAttacking = true;
        anim.SetTrigger(triggerName);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.attack);
        }

        DoDamage();
        Invoke(nameof(ResetAttack), attackTime);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }
    void DoDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Detectó: " + enemy.name);

            EnemyHealth enemyHealth = enemy.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelEnd"))
        {
            LevelCompleteUI levelCompleteUI = FindObjectOfType<LevelCompleteUI>();

            if (levelCompleteUI != null)
            {
                levelCompleteUI.ShowLevelComplete();
            }
        }
    }
}