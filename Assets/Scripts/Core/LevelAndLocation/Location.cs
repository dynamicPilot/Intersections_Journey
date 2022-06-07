using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Unit/Location")]
public class Location : ScriptableObject
{
    [SerializeField] private string locationName;
    public string LocationName { get => locationName; }
    [SerializeField] private int locationIndex;
    public int LocationIndex { get => locationIndex; set { locationIndex = value; } }

    [SerializeField] private List<Level> levels;
    public List<Level> Levels { get => levels; }

    [SerializeField] private int pointsToMakeAvailable = 0;
    public int PointsToMakeAvailable { get => pointsToMakeAvailable; }

    [SerializeField] private int maxPoints = 0;
    public int MaxPoints { get => maxPoints; set { maxPoints = value; } }

    [SerializeField] private Location[] locationsToMakeAvailable;
    public Location[] LocationsToMakeAvailable { get => locationsToMakeAvailable; }


    // images and color for inner menu
    [Header("Colors")]
    [SerializeField] private Color[] crossroadsColors;
    public Color[] CrossroadsColors { get => crossroadsColors; }
    [SerializeField] private Color[] uiColors;
    public Color[] UIColors { get => uiColors; }
}
