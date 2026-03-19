using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int CurrentHealth { get; private set; }

    public float invulnerableTime = 1f;

    private bool isDead = false;
    private bool isInvulnerable = false;

    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged OnHealthChanged;

    private GameOverUI gameOverUI;

    void Start()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        gameOverUI = FindObjectOfType<GameOverUI>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return;

        CurrentHealth -= damage;
        Debug.Log("Jugador recibió daño");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
        }

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvulnerabilityCoroutine());
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        CurrentHealth += amount;

        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    void Die()
    {
        isDead = true;

        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver("Se te agotaron las vidas");
        }

        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            Die();
        }
    }
}