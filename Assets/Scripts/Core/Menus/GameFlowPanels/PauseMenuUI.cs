using IJ.Core.Menus.Others;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private LevelSettingsUI _settings;

    public void StartPause()
    {
        Time.timeScale = 0f;
        _settings.SetSettingsUI();
        pausePanel.SetActive(true);
    }

    public void EndPause()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
