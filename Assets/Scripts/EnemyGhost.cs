using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    [Header("Patrulla")]
    public float patrolSpeed = 1.5f;
    public float moveDistance = 2f;

    [Header("Persecución")]
    public float chaseSpeed = 2.5f;
    public float detectionRange = 4f;
    public float stopDistance = 1.2f;   // distancia para no montarse encima
    public float verticalOffset = 0.5f; // queda un poco por encima del jugador

    [Header("Referencia")]
    public Transform player;

    private Vector3 startPos;
    private bool movingRight = true;
    private bool isChasing = false;

    void Start()
    {
        startPos = transform.position;

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        isChasing = distanceToPlayer <= detectionRange;

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
            transform.localScale = new Vector3(-6, 6, 1);

            if (transform.position.x >= startPos.x + moveDistance)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);
            transform.localScale = new Vector3(6, 6, 1);

            if (transform.position.x <= startPos.x - moveDistance)
                movingRight = true;
        }
    }

    void ChasePlayer()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y + verticalOffset, transform.position.z);

        float distanceX = targetPos.x - transform.position.x;
        float distanceY = targetPos.y - transform.position.y;

        // Solo se acerca si está más lejos que la distancia mínima
        if (Mathf.Abs(distanceX) > stopDistance)
        {
            float moveX = Mathf.Sign(distanceX) * chaseSpeed * Time.deltaTime;
            transform.position += new Vector3(moveX, 0f, 0f);
        }

        // Ajuste suave en vertical, sin montarse encima
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, targetPos.y, transform.position.z),
            chaseSpeed * 0.5f * Time.deltaTime
        );

        // Voltear sprite
        if (distanceX > 0)
            transform.localScale = new Vector3(-6, 6, 1);
        else if (distanceX < 0)
            transform.localScale = new Vector3(6, 6, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ghost tocó a: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}