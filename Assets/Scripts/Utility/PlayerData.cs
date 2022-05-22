using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // class to store player's data
    [SerializeField] private int[] levelPoints;
    public int[] LevelPoints { get => levelPoints; }
    [SerializeField] private int[] locationAvailability;
    public int[] LocationAvailability { get => locationAvailability; }
    [SerializeField] private int totalPoints;
    public int TotalPoints { get => totalPoints; }

    public PlayerData(int[] newLevelPoints, int[] newLocationAvailability, int newTotalPoints)
    {
        levelPoints = newLevelPoints;
        locationAvailability = newLocationAvailability;
        totalPoints = newTotalPoints;
    }
}


