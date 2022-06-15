using System.Collections.Generic;

[System.Serializable]
public class RoadsTimers
{
    private float aCoeff = -0.25f;
    private float bCoeff = 0.25f;

    public List<float> roadTimers = new List<float>();
    public List<float> maxTimeByRoad = new List<float>();
    public List<float> maxAdditionalTimeToWaitByRoad = new List<float>();

    public List<int> roadsWithAllertOn = new List<int>();

    public delegate void TimerIsOver(int roadIndex);
    public event TimerIsOver OnTimerIsOver;

    public RoadsTimers(int roadsCount, List<float> _maxTimeByRoad, List<float> _maxAdditionalTimeToWaitByRoad, float _aCoeff, float _bCoeff)
    {
        for (int i = 0; i < roadsCount; i++) roadTimers.Add(0);
        maxTimeByRoad = _maxTimeByRoad;
        maxAdditionalTimeToWaitByRoad = _maxAdditionalTimeToWaitByRoad;
        aCoeff = _aCoeff;
        bCoeff = _bCoeff;
    }

    public void RemoveUnitFromRoad(int roadIndex, float totalTimeOnRoad, TYPE type, int taxiCounter)
    {
        roadTimers[roadIndex] -= totalTimeOnRoad;

        if (type == TYPE.taxi)
        {
            roadTimers[roadIndex] -= totalTimeOnRoad * (aCoeff + bCoeff * taxiCounter); // PAY ATTENTION! Possibly it's need some change later!
        }
    }

    public void UpdateTimer(RoadTimer timer)
    {
        List<float> deltas = timer.GetDeltas();

        for (int i = 0; i < deltas.Count; i++)
        {
            if (deltas[i] == 0)
            {
                if (roadTimers[i] != 0)
                {
                    Logging.Log("RoadTimers: reset timer");
                    roadTimers[i] = 0;
                    if (roadsWithAllertOn.Contains(i)) StopAllert(i);
                }
                
                timer.UpdateIndicatorState(i, RoadIndicators.STATE.stop);
                continue;
            }

            roadTimers[i] += deltas[i];

            if (roadTimers[i] >= maxTimeByRoad[i])
            {
                
                // allerting state
                if (!roadsWithAllertOn.Contains(i))
                {
                    StartAllert(i);
                    timer.UpdateIndicatorState(i, RoadIndicators.STATE.startAllert);
                    return;
                }

                float additionalTime = maxAdditionalTimeToWaitByRoad[i] * timer.GetUnitsNumber(i);
                if (roadTimers[i] >= maxTimeByRoad[i] + additionalTime)
                {
                    // gameOver
                    if (OnTimerIsOver != null) OnTimerIsOver.Invoke(i);
                }

                timer.UpdateIndicatorState(i, RoadIndicators.STATE.allertUpdate, roadTimers[i] / (maxTimeByRoad[i] + additionalTime));             
            }
            else
            {
                // if allert is on but need to be off
                if (roadsWithAllertOn.Contains(i))
                {
                    StopAllert(i);
                    timer.UpdateIndicatorState(i, RoadIndicators.STATE.stopAllert);
                    return;
                }

                timer.UpdateIndicatorState(i, RoadIndicators.STATE.update, roadTimers[i] / maxTimeByRoad[i]);
            }
        }
    }

    void StartAllert(int roadIndex)
    {
        roadsWithAllertOn.Add(roadIndex);
    }

    void StopAllert(int roadIndex)
    {
        roadsWithAllertOn.Remove(roadIndex);
    }
}
