using IJ.Core.Menus.Main.Levels;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Unit/Level")]
[System.Serializable]
public class Level : ScriptableObject
{
    [Header("General Info")]
    [SerializeField] private int number;
    public int Number { get => number; set { number = value; } }

    [SerializeField] private int levelIndex;
    public int LevelIndex { get => levelIndex; set { levelIndex = value; } }

    [SerializeField] private string sceneName;
    public string SceneName { get => sceneName; }
    [SerializeField] private string crossSceneName;
    public string CrossSceneName { get => crossSceneName; }

    [SerializeField] private LevelsPanelUI.CROSS crossType;
    public LevelsPanelUI.CROSS CrossType { get => crossType; }

    [SerializeField] private Location location;
    public Location Location { get => location; }

    [Header("Difficulty settings")]
    [SerializeField] private int maxCrashesNumber = 3;
    public int MaxCrashesNumber { get => maxCrashesNumber; }

    [SerializeField] List<float> maxTimeByRoad;
    public List<float> MaxTimeByRoad { get => maxTimeByRoad; }

    [SerializeField] List<float> maxAdditionalTimeToWaitByRoad;
    public List<float> MaxAdditionalTimeToWaitByRoad { get => maxAdditionalTimeToWaitByRoad; }

    [Header("Crossroads settings")]
    [SerializeField] private PointsPair[] pointNumbersPairs;
    public PointsPair[] PointNumberPairs { get => pointNumbersPairs; }
    [SerializeField] private int[] endPointsWithParking;
    public int[] EndPointsWithParking { get => endPointsWithParking; }

    [SerializeField] private Vector3[] corrections; // x - new x point, y - new y point, z - point number
    public Vector3[] Corrections { get => corrections; }

    [SerializeField] private StartPointPackCorrection[] startPointPacksCorrections; // x - new x point, y - new y point
    public StartPointPackCorrection[] StartPointPacksCorrections { get => startPointPacksCorrections; }

    [Header("Road Packs")]
    [SerializeField] private int[] activeRoadPacks;
    public int[] ActiveRoadPacks { get => activeRoadPacks; }

    [Header("Vehicle Creator settings")]
    // if need curve by hour
    [SerializeField] private TimeIntervalsByRoad[] timeIntervalsByRoads;
    public TimeIntervalsByRoad[] TimeIntervalsByRoads { get => timeIntervalsByRoads; set => timeIntervalsByRoads = value; }

    [Header("Repair Site settings")]
    [SerializeField] private List<TimePoint> repairSiteTimePoints;
    public List<TimePoint> RepairSiteTimePoints { get => repairSiteTimePoints; }
}
