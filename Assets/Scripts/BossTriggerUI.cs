using UnityEngine;

public class BossTriggerUI : MonoBehaviour
{
    public GameObject bossHealthBar;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossHealthBar.SetActive(true);
        }
    }
}