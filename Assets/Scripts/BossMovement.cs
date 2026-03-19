using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 2.5f;
    public float detectionRange = 8f;
    public float stopDistance = 1.5f;

    private Transform player;
    private SpriteRenderer sr;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (player.position.x > transform.position.x)
                sr.flipX = false;
            else if (player.position.x < transform.position.x)
                sr.flipX = true;
        }
    }
}