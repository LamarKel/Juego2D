using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public float loadDelay = 2f;
    public string nextLevelName = "Nivel2";

    private bool levelEnded = false;

    public void ShowLevelComplete()
    {
        if (levelEnded) return;

        levelEnded = true;
        levelCompletePanel.SetActive(true);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.levelComplete);
        }
        Invoke(nameof(LoadNextLevel), loadDelay);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}