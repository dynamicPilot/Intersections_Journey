using System.Collections.Generic;


namespace IJ.Core.Objects.LevelAndLocation
{
    [System.Serializable]
    public class LocationProgress : LocationOrLevelProgress
    {
        private Dictionary<int, int> _levelsPoints = new Dictionary<int, int>();
        public Dictionary<int, int> LevelsPoints { get => _levelsPoints; }

        public LocationProgress(int locationIndex, List<Level> levels, int maxPoints = -1) : base(locationIndex, maxPoints)
        {
            foreach (Level level in levels) _levelsPoints[level.LevelIndex] = 0;
        }

        public void AddLevelPoints(int levelIndex, int pointsAmount, bool needUpdateIsMax = true)
        {
            if (_levelsPoints.ContainsKey(levelIndex))
            {
                _levelsPoints[levelIndex] += pointsAmount;
            }
            else
            {
                _levelsPoints[levelIndex] = pointsAmount;
            }

            AddPoints(pointsAmount, false, needUpdateIsMax);
        }
    }
}

