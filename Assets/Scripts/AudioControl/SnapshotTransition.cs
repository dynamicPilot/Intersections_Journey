using UnityEngine;
using UnityEngine.Audio;
using Utilites.Configs;

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
}
