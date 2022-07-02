using UnityEngine;

namespace IJ.Core.Objects.LevelAndLocation
{
    [CreateAssetMenu(fileName = "New Collection", menuName = "Unit/Collections/LevelsAndLocationsCollection")]
    public class LevelsAndLocationsCollection : ScriptableObject
    {
        [SerializeField] private Location[] locations;
        public Location[] Locations { get => locations; }
        [SerializeField] private Level[] levels;
        public Level[] Levels { get => levels; set => levels = value; }
    }
}
