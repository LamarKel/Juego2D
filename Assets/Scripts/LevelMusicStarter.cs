using UnityEngine;

public class LevelMusicStarter : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.levelMusic);
        }
    }
}