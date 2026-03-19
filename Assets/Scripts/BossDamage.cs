using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public int damage = 2;
    public int hitsToDamage = 3;

    private int currentHits = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentHits++;

            Debug.Log("Toques al boss: " + currentHits);

            if (currentHits >= hitsToDamage)
            {
                PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

                if (player != null)
                {
                    player.TakeDamage(damage);

                    // 🔊 SONIDO AQUÍ
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
                    }
                }

                currentHits = 0;
            }
        }
    }
}