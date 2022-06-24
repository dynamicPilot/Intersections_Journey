using System.Collections.Generic;
using UnityEngine;

public interface IHoldScannerTrafficLightsInfo
{
    public abstract bool AddTrafficLight(TrafficLight trafficLight);
    public abstract bool RemoveTrafficLight(TrafficLight trafficLight);
}

[System.Serializable]
public class VScannerTrafficLightInfo : IGetDistanceInfo, IHoldScannerTrafficLightsInfo
{
    [SerializeField] private TrafficLight _trafficLightToFollow;

    private IPositionShearer _positionShearer;

    public VScannerTrafficLightInfo(IPositionShearer positionShearer)
    {
        _positionShearer = positionShearer;
    }

    public List<float> GetInfo()
    {
        List<float> distance = new List<float>();
        Vector3 position = _positionShearer.GetPosition();
        float gap = _positionShearer.GetGap();

        distance.Add(0f);
        distance.Add(GetDistanceToTrafficLigh(position, gap));

        return distance;
    }

    float GetDistanceToTrafficLigh(Vector3 position, float gap)
    {
        if (_trafficLightToFollow != null)
            return ScannerUtilities.DistanceToSingleObjectWithZeroVelocity(position, _trafficLightToFollow.gameObject.transform.position, gap);
        else
            return -100f;
    }

    public bool AddTrafficLight(TrafficLight trafficLight)
    {
        if (_trafficLightToFollow == null && trafficLight != _trafficLightToFollow)
        {
            _trafficLightToFollow = trafficLight;
            return true;
        }
        return false;
    }

    public bool RemoveTrafficLight(TrafficLight trafficLight)
    {
        if (trafficLight == _trafficLightToFollow)
        {
            _trafficLightToFollow = null;
            return false;
        }
        return true;
    }
}
