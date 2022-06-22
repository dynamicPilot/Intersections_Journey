using System.Collections.Generic;

[System.Serializable]
public class RoadTimer
{
    float deltaT;
    float aCoeff;
    float bCoeff;
    float indicatorBorder;
    List<float> deltas;
    List<int> units;
    public List<RoadIndicators.STATE> indicatorStates;
    public List<float> indicatorValues;   
    public RoadTimer(float _deltaT, float _aCoeff, float _bCoeff, float _indicatorBorder)
    {
        deltaT = _deltaT;
        aCoeff = _aCoeff;
        bCoeff = _bCoeff;
        deltas = new List<float>();
        indicatorValues = new List<float>();
        indicatorStates = new List<RoadIndicators.STATE>();

        indicatorBorder = _indicatorBorder;
    }

    public void UpdateUnits(List<int> _units, List<int> taxies)
    {
        units = _units;
        deltas.Clear();
        for (int i = 0; i < units.Count; i++)
        {
            float delta = 0;

            if (units[i] != 0) delta += units[i] * deltaT;

            if (taxies[i] != 0) delta += (aCoeff + bCoeff * taxies[i]) * deltaT;

            deltas.Add(delta);
        }
    }

    public List<float> GetDeltas()
    {
        return deltas;
    }

    public int GetUnitsNumber(int roadIndex)
    {
        return units[roadIndex];
    }

    public void UpdateIndicatorState(int roadIndex, RoadIndicators.STATE state, float value = 0f)
    {
        if (roadIndex > indicatorStates.Count - 1)
        {
            indicatorStates.Add(state);
            indicatorValues.Add(0);
        }

        if (state == RoadIndicators.STATE.update)
        {
            if (value > indicatorBorder)
                indicatorValues[roadIndex] = value;
            else
            {
                indicatorStates[roadIndex] = RoadIndicators.STATE.stop;
            }
        }
        else if (state == RoadIndicators.STATE.allertUpdate)
        {
            indicatorValues[roadIndex] = value;
        }
    }

    public RoadIndicators.STATE GetAllertState(int roadIndex)
    {
        if (roadIndex < indicatorStates.Count) return indicatorStates[roadIndex];
        return RoadIndicators.STATE.stop;
    }

    public float GetUpdateValue(int roadIndex)
    {
        if (roadIndex < indicatorValues.Count) return indicatorValues[roadIndex];
        return 0f;
    }
}
