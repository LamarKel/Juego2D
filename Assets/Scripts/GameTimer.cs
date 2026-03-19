using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            TimerEnd();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int time = Mathf.CeilToInt(timeRemaining);
        timerText.text = "TIME: " + time;
    }

    void TimerEnd()
    {
        isGameOver = true;

        GameOverUI gameOver = FindObjectOfType<GameOverUI>();
        PlayerHealth player = FindObjectOfType<PlayerHealth>();

        if (gameOver != null)
        {
            gameOver.ShowGameOver("Se te agotó el tiempo");
        }

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }
    }
}