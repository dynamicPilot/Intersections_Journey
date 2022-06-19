using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadIndicators
{
    public enum STATE { stop, update, startAllert, stopAllert, allertUpdate }

    [SerializeField] private RoadMarksControl roadMarks;

    public List<STATE> marksState = new List<STATE>();

    IConvertToPointNumber converter;
    public RoadIndicators(RoadMarksControl _roadMarks, float _valueToShowIndicator, int marksCounter, IConvertToPointNumber _converter)
    {
        roadMarks = _roadMarks;
        converter = _converter;

        for (int i = 0; i < marksCounter; i++)
        {
            marksState.Add(STATE.stop);
        }
    }

    public void UpdateMarks(RoadTimer timer)
    {       
        for (int i = 0; i < marksState.Count; i++)
        {
            STATE newState = timer.GetAllertState(i);
            bool theSameState = marksState[i] == newState;

            if (newState == STATE.stop)
            {
                if (!theSameState)
                {
                    if (StopIndicator(i)) marksState[i] = newState;                   
                }

                continue;
            }

            if (newState == STATE.stopAllert)
            {
                StopAllert(i);
            }
            else if (newState == STATE.startAllert)
            {
                StartAllert(i);
            }
            else if (newState == STATE.allertUpdate)
            {
                UpdateAllert(i, timer.GetUpdateValue(i));
            }
            else
            {
                UpdateIndicator(i, timer.GetUpdateValue(i));
            }

            marksState[i] = newState;
        }
    }

    public Vector3 GetMarkPosition(int roadIndex)
    {
        return roadMarks.GetIndicatorPosition(converter.ConvertToPointNumber(roadIndex));
    }

    void StartAllert(int roadIndex)
    {
        roadMarks.StartAllert(converter.ConvertToPointNumber(roadIndex));
    }

    void StopAllert(int roadIndex)
    {
        roadMarks.StopAllert(converter.ConvertToPointNumber(roadIndex));
    }
    void UpdateAllert(int roadIndex, float value)
    {
        roadMarks.UpdateIndicatorAllertValue(converter.ConvertToPointNumber(roadIndex), value);
    }

    bool StopIndicator(int roadIndex)
    {
        return roadMarks.StopIndicator(converter.ConvertToPointNumber(roadIndex));
    }

    void UpdateIndicator(int roadIndex, float value)
    {
        roadMarks.UpdateIndicatorValue(converter.ConvertToPointNumber(roadIndex), value);
    }
}
