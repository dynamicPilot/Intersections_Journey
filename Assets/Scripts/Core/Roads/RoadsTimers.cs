using System.Collections.Generic;

[System.Serializable]
public class RoadsTimers
{
    private float aCoeff = -0.25f;
    private float bCoeff = 0.25f;

    public List<float> _roadTimers = new List<float>();
    public List<float> maxTimeByRoad = new List<float>();
    public List<float> maxAdditionalTimeToWaitByRoad = new List<float>();

    public List<int> _roadsWithAllert = new List<int>();

    public delegate void TimerIsOver(int roadIndex);
    public event TimerIsOver OnTimerIsOver;

    public RoadsTimers(int roadsCount, List<float> _maxTimeByRoad, List<float> _maxAdditionalTimeToWaitByRoad, float _aCoeff, float _bCoeff)
    {
        for (int i = 0; i < roadsCount; i++) _roadTimers.Add(0);
        maxTimeByRoad = _maxTimeByRoad;
        maxAdditionalTimeToWaitByRoad = _maxAdditionalTimeToWaitByRoad;
        aCoeff = _aCoeff;
        bCoeff = _bCoeff;
    }

    public void RemoveUnitFromRoad(int roadIndex, float totalTimeOnRoad, TYPE type, int taxiCounter)
    {
        _roadTimers[roadIndex] -= totalTimeOnRoad;

        if (type == TYPE.taxi)
        {
            _roadTimers[roadIndex] -= totalTimeOnRoad * (aCoeff + bCoeff * taxiCounter); // PAY ATTENTION! Possibly it's need some change later!
        }
    }

    public void UpdateTimer(RoadTimer timer)
    {
        List<float> deltas = timer.GetDeltas();

        for (int i = 0; i < deltas.Count; i++)
        {
            if (deltas[i] == 0)
            {
                if (_roadTimers[i] != 0)
                {
                    Logging.Log("RoadTimers: reset timer");
                    _roadTimers[i] = 0;
                    if (_roadsWithAllert.Contains(i)) REmoveFromRoadsWithAllert(i);
                }
                
                timer.UpdateIndicatorState(i, RoadIndicators.STATE.stop);
                continue;
            }

            _roadTimers[i] += deltas[i];

            float additionalTime = maxAdditionalTimeToWaitByRoad[i] * timer.GetUnitsNumber(i);
            if (_roadTimers[i] >= maxTimeByRoad[i] + additionalTime)
            {
                // game over state
                if (OnTimerIsOver != null) OnTimerIsOver.Invoke(i);
                return;
            }
            else if (_roadTimers[i] >= maxTimeByRoad[i])
            {
                float value = (_roadTimers[i] - maxTimeByRoad[i]) / additionalTime;

                // allerting state
                if (!_roadsWithAllert.Contains(i))
                {
                    // move to allert

                    AddToRoadsWithAllert(i);
                    timer.UpdateIndicatorState(i, RoadIndicators.STATE.startAllert, value);
                    continue;
                }

                timer.UpdateIndicatorState(i, RoadIndicators.STATE.allertUpdate, value);
            }
            else
            {
                float value = _roadTimers[i] / maxTimeByRoad[i];

                // if allert is on but need to be off
                if (_roadsWithAllert.Contains(i))
                {
                    REmoveFromRoadsWithAllert(i);
                    timer.UpdateIndicatorState(i, RoadIndicators.STATE.stopAllert, value);
                    continue;
                }

                timer.UpdateIndicatorState(i, RoadIndicators.STATE.update, value);
            }
        }
    }

    void AddToRoadsWithAllert(int roadIndex)
    {
        _roadsWithAllert.Add(roadIndex);
    }

    void REmoveFromRoadsWithAllert(int roadIndex)
    {
        _roadsWithAllert.Remove(roadIndex);
    }
}
