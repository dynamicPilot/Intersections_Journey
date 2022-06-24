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

    public Vector3 GetIndicatorPosition(int startPointNumber)
    {
        if (HasIndicator(startPointNumber))
        {
            return marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public bool StopIndicator(int startPointNumber)
    {
        if (!IsIndicatorOn(startPointNumber)) return true;

        if (HasIndicator(startPointNumber))
        {
            return marks[roadMarksIndexByStartPointNumber[startPointNumber]].StopIndicator();
        }

        return true;
    }

    public void UpdateIndicatorValue(int startPointNumber, float value)
    {
        if (HasIndicator(startPointNumber))
        {
            if (!IsIndicatorOn(startPointNumber))
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
        if (HasIndicator(startPointNumber))
        {
            marks[roadMarksIndexByStartPointNumber[startPointNumber]].UpdateIndicatorValue(value, true);
        }
    }

    public void StartAllert(int startPointNumber)
    {
        if (HasIndicator(startPointNumber))
        {
            if (IsIndicatorOn(startPointNumber)) marks[roadMarksIndexByStartPointNumber[startPointNumber]].MoveToAllert();
        }
    }

    public void StopAllert(int startPointNumber)
    {
        if (HasIndicator(startPointNumber))
        {
            if (IsIndicatorOn(startPointNumber)) marks[roadMarksIndexByStartPointNumber[startPointNumber]].MoveBackToTimer();
        }
    }

    bool IsIndicatorOn(int startPointNumber)
    {
        if (HasIndicator(startPointNumber)) return marks[roadMarksIndexByStartPointNumber[startPointNumber]].gameObject.activeSelf;
        else return false;
    }

    bool HasIndicator(int startPointNumber)
    {
        return roadMarksIndexByStartPointNumber.ContainsKey(startPointNumber);
    }
}
