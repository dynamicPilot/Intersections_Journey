using UnityEngine;

public class ObjectsDetector
{
    private IStartAndEndPathPoints _routerInfo;
    private IHoldScannerUnitsInfo _unitsInfo;
    private IHoldScannerTrafficLightsInfo _trafficLightInfo;

    public ObjectsDetector(IStartAndEndPathPoints routerInfo, IHoldScannerUnitsInfo unitsInfo, IHoldScannerTrafficLightsInfo trafficLightInfo)
    {
        _routerInfo = routerInfo;
        _unitsInfo = unitsInfo;
        _trafficLightInfo = trafficLightInfo;
    }

    public bool DetectTrafficLight(Collider2D collision)
    {
        TrafficLight trafficLight = collision.gameObject.GetComponent<TrafficLight>();
        if (trafficLight == null)
        {
            return false;
        }
        else if (!trafficLight.CheckPointNumber(_routerInfo.StartPathPoints()) &&
            !trafficLight.CheckPointNumber(_routerInfo.EndPathPoints()))
        {
            return false;
        }

        return _trafficLightInfo.AddTrafficLight(trafficLight);
    }

    public void DetectCar(Collider2D collision, DIRECTION direction)
    {
        IDirectionShearer unitDirection = collision.gameObject.GetComponent<VScanner>();
        IPositionShearer unitPosition = collision.gameObject.GetComponent<VScanner>().PositionShearer;

        if (direction != DIRECTION.none)
        {
            // compaire directions
            if (unitDirection.GetDirection() != direction) return;
        }

        _unitsInfo.AddUnit(unitPosition, collision.gameObject.GetComponent<VMover>());
    }

    public void DetectTrain(Collider2D collision, DIRECTION direction)
    {
        Logging.Log("Detect train");
        DetectCar(collision, direction);
    }

    public bool UndetectTrafficLight(Collider2D collision)
    {
        return _trafficLightInfo.RemoveTrafficLight(collision.gameObject.GetComponent<TrafficLight>());
    }

    public void UndetectCar(Collider2D collision, DIRECTION direction)
    {
        //IDirectionShearer unitDirection = collision.gameObject.GetComponent<VScanner>();
        IPositionShearer unitPosition = collision.gameObject.GetComponent<VScanner>().PositionShearer;
        //if (direction != DIRECTION.none)
        //{
        //    // compaire directions
        //    if (unitDirection.GetDirection() != direction) return;
        //}

        _unitsInfo.RemoveUnit(unitPosition, collision.gameObject.GetComponent<VMover>());
    }

    public void UndetectTrain(Collider2D collision, DIRECTION direction)
    {
        UndetectCar(collision, direction);
    }
}
