using UnityEngine;
using IJ.Utilities.Configs;

public class SnapshotTransition : MonoBehaviour
{
    [SerializeField] private AudioConfig _config;

    bool isOnPause = false;
    public void StartGame()
    {
        _config.ActiveSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToPausedGame()
    {
        Logging.Log("SnapshotTransition: to pause");
        isOnPause = true;
        _config.PausedSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToActiveGame()
    {
        isOnPause = false;
        _config.ActiveSnaphot.TransitionTo(_config.TransitionTime);
    }

    public void ToStartGame()
    {
        _config.StartSnaphot.TransitionTo(_config.TransitionTime);
    }
}
