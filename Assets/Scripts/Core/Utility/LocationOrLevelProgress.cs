using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationOrLevelProgress
{
    [SerializeField] private int itemIndex;
    public int ItemIndex { get => itemIndex; }
    [SerializeField] private bool isAvailable = false;
    public bool IsAvailable { get => isAvailable; set { isAvailable = value; } }

    [SerializeField] private bool isMax = false;
    public bool IsMax { get => isMax; }

    [SerializeField] private bool isPassed = false;
    public bool IsPassed { get => isPassed; set { isPassed = value; } }

    [SerializeField] private int pointsEarned = 0;
    public int PointsEarned { get => pointsEarned; }
    [SerializeField] private int maxPoints = 0;
    public int MaxPoints { get => maxPoints; set { maxPoints = value; } }

    public LocationOrLevelProgress()
    {
        itemIndex = -1;
        isAvailable = false;
        isPassed = false;
        pointsEarned = 0;
        maxPoints = -1;
        isMax = false;
    }

    //public LocationOrLevelProgress(int newItemNumberOrIndex)
    //{
    //    itemIndex = newItemNumberOrIndex;
    //    isAvailable = false;
    //    pointsEarned = 0;
    //    maxPoints = -1;
    //    isMax = false;
    //}

    public LocationOrLevelProgress(int newItemNumberOrIndex, int newMaxPoints = -1, bool newIsAvailable = false, int newPointsEarned = 0, bool newIsPassed = false)
    {
        itemIndex = newItemNumberOrIndex;
        maxPoints = newMaxPoints;
        isAvailable = newIsAvailable;
        pointsEarned = newPointsEarned;
        isPassed = newIsPassed;
        if (maxPoints> 0) UpdateIsMax();
    }

    public void AddPoints(int amount = 1, bool firstlySetToZero = false, bool needUpdateMax = false)
    {
        //if (!isAvailable) return;

        if (firstlySetToZero) pointsEarned = 0;

        pointsEarned += amount;

        if (pointsEarned > 0 && !isPassed) isPassed = true;

        if (needUpdateMax) UpdateIsMax();
    }

    public void SetItemIndex(int newIndex)
    {
        if (newIndex >= 0) itemIndex = newIndex;
        else itemIndex = -1;
    }

    public void UpdateIsMax()
    {
        //if (!isAvailable) return;

        isMax = (pointsEarned >= maxPoints && maxPoints > 0);
    }
}

[System.Serializable]
public class LocationProgress : LocationOrLevelProgress
{
    private Dictionary<int, int> levelsPoints = new Dictionary<int, int>();
    public Dictionary<int, int> LevelsPoints { get => levelsPoints; }

    //public LocationProgress(int newLocationIndex)
    //{
    //    SetItemIndex(newLocationIndex);
    //    IsAvailable = false;
    //    MaxPoints = -1;
    //    AddPoints(0, true, true);
    //}

    public LocationProgress(int newLocationIndex, List<Level> newLevels, int newMaxPoints = -1, bool newIsAvailable = false, int newPointsEarned = 0, bool newIsPassed = false)
    {
        SetItemIndex(newLocationIndex);
        IsAvailable = newIsAvailable;
        MaxPoints = newMaxPoints;
        IsPassed = newIsPassed;
        AddPoints(newPointsEarned, true, (MaxPoints > 0));

        foreach(Level level in newLevels)
        {
            levelsPoints[level.LevelIndex] = 0;
        }
    }

    public void AddLevelPoints(int levelIndex, int pointsAmount, bool needUpdateIsMax = true)
    {
        //if (!IsAvailable && pointsAmount > 0) return;
        //Logging.Log("LocationProgress: add level points " + levelIndex);
        if (levelsPoints.ContainsKey(levelIndex))
        {
            //Logging.Log("LocationProgress: add to level");
            levelsPoints[levelIndex] += pointsAmount;
        }
        else
        {
            //Logging.Log("LocationProgress: new level");
            levelsPoints[levelIndex] = pointsAmount;
        }

        AddPoints(pointsAmount, false, needUpdateIsMax);
    }


    //public void AddToMaxPoints(int pointsAmount, bool needUpdateIsMax = true)
    //{
    //    MaxPoints += pointsAmount;

    //    if (needUpdateIsMax) UpdateIsMax();
    //}
}
