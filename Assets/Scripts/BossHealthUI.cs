using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public EnemyHealth bossHealth;
    public Slider healthSlider;

    void Start()
    {
        if (bossHealth != null)
        {
            healthSlider.maxValue = bossHealth.maxHealth;
            healthSlider.value = bossHealth.maxHealth;
        }
    }

    void Update()
    {
        if (bossHealth != null)
        {
            healthSlider.value = bossHealth.GetCurrentHealth();
        }
    }
}