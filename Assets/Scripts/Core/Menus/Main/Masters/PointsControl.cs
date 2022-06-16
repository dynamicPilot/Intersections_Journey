using UnityEngine;
using IJ.Core.UIElements;

namespace IJ.Core.Menus.MainMenu
{
    public class PointsControl : MonoBehaviour, ISetPlayerState
    {
        [Header("Components")]
        [SerializeField] private PointsUI _pointsUI;
        [SerializeField] private DataSaveAndLoad dataSaveAndLoad;

        private PlayerState _playerState;

        private void OnDestroy()
        {
            if (_playerState != null)
            {
                // unsubscribe from delegate
                _playerState.OnPointsNumberChanged -= UpdatePoints;
            }
        }

        public void SetPlayerState(PlayerState playerState)
        {
            _playerState = playerState;
           
            UpdatePoints(_playerState.TotalPointsNumber);
            _playerState.OnPointsNumberChanged += UpdatePoints;
        }

        public bool HaveSomePoints(int amount)
        {
            return _playerState.TotalPointsNumber >= amount;
        }

        public void ChangePointsAmount(int amount, bool needSave = true)
        {
            bool haveChanged = _playerState.AddPointsToTotalNumber(amount);

            if (needSave & haveChanged)
            {
                dataSaveAndLoad.SaveData(_playerState);
            }
        }

        void UpdatePoints(int points)
        {
            _pointsUI.UpdatePoints(points);
        }


    }
}
