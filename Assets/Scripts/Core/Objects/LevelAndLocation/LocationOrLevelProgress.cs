using UnityEngine;

namespace IJ.Core.Objects.LevelAndLocation
{
    [System.Serializable]
    public class LocationOrLevelProgress
    {
        [SerializeField] private int _itemIndex;
        public int ItemIndex { get => _itemIndex; }
        [SerializeField] private bool _isAvailable = false;
        public bool IsAvailable { get => _isAvailable; set { _isAvailable = value; } }

        [SerializeField] private bool _isMax = false;
        public bool IsMax { get => _isMax; }

        [SerializeField] private int _pointsEarned = 0;
        public int PointsEarned { get => _pointsEarned; }
        [SerializeField] private int _maxPoints = 0;
        public int MaxPoints { get => _maxPoints; set { _maxPoints = value; } }

        #region Constructors
        public LocationOrLevelProgress()
        {
            _itemIndex = -1;
            _isAvailable = false;
            _pointsEarned = 0;
            _maxPoints = -1;
            _isMax = false;
        }

        public LocationOrLevelProgress(int itemIndex, int maxPoints = -1)
        {
            _itemIndex = itemIndex;
            _maxPoints = maxPoints;
            _isAvailable = false;
            _pointsEarned = 0;
            if (_maxPoints > 0) UpdateIsMax();
        }

        #endregion

        public void AddPoints(int amount = 1, bool firstlySetToZero = false, bool needUpdateMax = false)
        {
            if (firstlySetToZero) ResetPoints();

            _pointsEarned += amount;

            if (needUpdateMax) UpdateIsMax();
        }

        public void SetItemIndex(int itemIndex)
        {
            if (itemIndex >= 0) _itemIndex = itemIndex;
            else _itemIndex = -1;
        }

        public void UpdateIsMax()
        {
            _isMax = (_pointsEarned >= _maxPoints && _maxPoints > 0);
        }

        void ResetPoints()
        {
            _pointsEarned = 0;
        }
    }
}