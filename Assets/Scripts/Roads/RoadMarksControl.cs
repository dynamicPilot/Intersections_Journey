using System.Collections.Generic;
using UnityEngine;

public class RoadMarksControl : MonoBehaviour
{
    [SerializeField] private List<RoadMark> marks;

    IDictionary<int, int> roadMarksIndexByStartPointNumber = new Dictionary<int, int>();

    private void Awake()
    {
        for (int i = 0; i < marks.Count; i++)
        {
            roadMarksIndexByStartPointNumber[marks[i].RoadStartPointNumber] = i;
        }
    }

    public bool IsIndicatorOn(int startPointNumber)
    {
        //int directionIndex = 
        //if (startPointNumber < marks.Count) return marks[startPointNumber].gameObject.activeSelf;
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber)) return marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.activeSelf;
        else return false;
    }

    public Vector2 GetIndicatorPosition(int startPointNumber)
    {
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber))
        {
            return marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public void StopIndicator(int startPointNumber)
    {
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber))
        {
            if (marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.activeSelf)
            {
                marks[roadMarksIndexByStartPointNumber[startPointNumber]].StopIndicator();
                //marks[index].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateIndicatorValue(int startPointNumber, float value)
    {
        //Logging.Log("RoadMarksControl: update indicator index" + index + " value " + value);
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber))
        {
            if (!marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.activeSelf)
            {
                // start indicator
                if (marks[roadMarksIndexByStartPointNumber[startPointNumber]].CheckForStartIndicator())
                {
                    marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.SetActive(true);
                    marks[roadMarksIndexByStartPointNumber[startPointNumber]].StartIndicator();
                }                
            }

            marks[roadMarksIndexByStartPointNumber[startPointNumber]].UpdateIndicatorValue(value, false);
        }
    }

    public void UpdateIndicatorAllertValue(int startPointNumber, float value)
    {
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber))
        {
            marks[roadMarksIndexByStartPointNumber[startPointNumber]].UpdateIndicatorValue(value, true);
        }
    }

    public void StartAllert(int startPointNumber)
    {
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber))
        {
            if (marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.activeSelf) marks[roadMarksIndexByStartPointNumber[startPointNumber]].MoveToAllert();
        }
    }

    public void StopAllert(int startPointNumber)
    {
        if (roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber))
        {
            if (marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.activeSelf) marks[roadMarksIndexByStartPointNumber[startPointNumber]].MoveBackToTimer();
        }
    }
}
