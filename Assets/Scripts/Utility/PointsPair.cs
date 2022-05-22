using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointsPair
{
    [SerializeField] private int startPointNumber;
    public int StartPointNumber { get => startPointNumber; }
    [SerializeField] private List<int> endPointNumbers = new List<int>();
    [SerializeField] private bool needChanceControl = false;
    [SerializeField] private List<float> endPointNumberChances = new List<float>();

    public int GetRandomEndPointNumber()
    {
        if (needChanceControl && endPointNumberChances.Count == endPointNumbers.Count)
        {
            float chance = Random.Range(0f, 1f);

            for (int i = 0; i < endPointNumberChances.Count; i++)
            {
                if (chance < endPointNumberChances[i])
                {
                    return endPointNumbers[i];
                }
            }
        }

        return endPointNumbers[Random.Range(0, endPointNumbers.Count)];
    }

    public bool IsEndPoint(int pointNumber)
    {
        return endPointNumbers.Contains(pointNumber);
    }

    public List<int> GetAllEndPoints()
    {
        return endPointNumbers;
    }
}

