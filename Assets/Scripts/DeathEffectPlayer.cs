using UnityEngine;

public class DeathEffectPlayer : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 0.08f;

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (frames.Length > 0)
        {
            sr.sprite = frames[0];
        }
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame++;

            if (currentFrame < frames.Length)
            {
                sr.sprite = frames[currentFrame];
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}