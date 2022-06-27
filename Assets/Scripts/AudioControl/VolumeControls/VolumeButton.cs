using IJ.Utilities;
using IJ.Utilities.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace IJ.AudioControls.VolumeControls
{
    public class VolumeButton : MonoBehaviour
    {
        private enum STATE { notMute, mute }
        [SerializeField] private AudioConfig _audioConfig;
        [SerializeField] private VolumeGroupData _group;
        [SerializeField] private SpriteCollection _collection;
        [SerializeField] private Image _image;

        private STATE _state;
        private int _clickCounter = 0;
        private float _volume;
        private void Awake()
        {
            _clickCounter = 0;
        }

        public bool IsStateChange()
        {
            int temp = _clickCounter;
            _clickCounter = 0;
            return (temp % 2 != 0);           
        }
        
        public void UpdateParameter()
        {
            _state = (_group.IsMute()) ? STATE.mute : STATE.notMute;
            Logging.Log("UpdateParameter: new state " + _state);
            HoldValues();
            UpdateView();
        }

        void HoldValues()
        {
            _clickCounter = 0;

            _volume = (_state == STATE.mute) ? _volume = _audioConfig.DefaultTotalVolume :
                _group.GetValue();
        }

        public void SetValue()
        {
            Logging.Log("Old state " + _state);
            _state = (_state == STATE.mute) ? STATE.notMute : STATE.mute;
            Logging.Log("New state " + _state);
            ChangeValue();
            UpdateView();
        }

        void ChangeValue()
        {
            _clickCounter++;
            if (_state == STATE.notMute) _group.SetValue(_volume);
            else _group.MakeMute();
        }

        private void UpdateView()
        {
            _image.sprite = _collection.Collection[(int) _state];
        }
    }
}
