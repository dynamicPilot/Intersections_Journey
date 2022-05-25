using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float defaultVolumeRate = 0.7f;

    [Header("Scripts")]
    [SerializeField] private SoundsPlayerAndStorage soundsPlayer;

    bool isMute = false;

    private void Awake()
    {
        SetAudio();
    }

    public void SetAudio()
    {
        isMute = false;
        soundsPlayer.SetVolumeRate(defaultVolumeRate);
    }

    public void PlaySound(int index = 0)
    {
        if (isMute) return;

        soundsPlayer.PlaySound(index);
    }

    public void TurnOnOffSound(bool _isMute, bool musicOnly = false)
    {
        // if music only --> radio control

        // UI sounds
        if (isMute != _isMute)
        {
            isMute = _isMute;
            soundsPlayer.TurnOnOff(isMute);
        }
    }
}
