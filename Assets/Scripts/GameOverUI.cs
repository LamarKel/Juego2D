using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI reasonText;
    public float restartDelay = 2f;

    [Header("Escena a cargar cuando el jugador muere")]
    public string firstLevelName = "Nivel1";

    private bool isGameOverShown = false;

    public void ShowGameOver(string reason)
    {
        if (isGameOverShown) return;

        isGameOverShown = true;

        gameOverPanel.SetActive(true);
        reasonText.text = reason;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);
        }

        Invoke(nameof(ReturnToFirstLevel), restartDelay);
    }

    void ReturnToFirstLevel()
    {
        SceneManager.LoadScene(firstLevelName);
    }
}