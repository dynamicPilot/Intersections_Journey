using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void StartPause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void EndPause()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
