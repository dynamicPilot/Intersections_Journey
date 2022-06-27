using UnityEngine;
using IJ.Utilities.Configs;

public class SnapshotTransition : MonoBehaviour
{
    [SerializeField] private AudioConfig _config;

    public void StartGame()
    {
        _config.ActiveSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToPausedGame()
    {
        Logging.Log("SnapshotTransition: to pause");
        _config.PausedSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToActiveGame()
    {
        _config.ActiveSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToStartGame()
    {
        _config.StartSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToMainMenu()
    {
        _config.MainMenuSnaphot.TransitionTo(_config.TransitionTime);
    }
}
