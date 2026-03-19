using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Image[] hearts;

    public Color fullColor = Color.red;
    public Color emptyColor = Color.gray;

    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHearts;
            UpdateHearts(playerHealth.CurrentHealth, playerHealth.maxHealth);
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHearts;
        }
    }

    void UpdateHearts(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth)
            {
                hearts[i].enabled = true;

                if (i < currentHealth)
                    hearts[i].color = fullColor;
                else
                    hearts[i].color = emptyColor;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}