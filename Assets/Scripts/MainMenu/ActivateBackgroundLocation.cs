using UnityEngine;

public class ActivateBackgroundLocation : MonoBehaviour
{
    public delegate void ActivateBackground();
    public event ActivateBackground OnActivateBackground;

    public delegate void StopBackground();
    public event StopBackground OnStopBackground;
    public void SetBackgroundAsActive()
    {
        if (OnActivateBackground != null)
        {
            OnActivateBackground.Invoke();
        }
    }

    public void SetBackgroundAsDisactive()
    {
        if (OnStopBackground != null)
        {
            OnStopBackground.Invoke();
        }
    }
}
