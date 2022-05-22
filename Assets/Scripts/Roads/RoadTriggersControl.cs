using System.Collections.Generic;
using UnityEngine;

public class RoadTriggersControl : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool isRoadEnd = false;

    [Header("Triggers")]
    [SerializeField] private List<Trigger> roadTriggers;

    [Header("Scripts")]
    [SerializeField] private RoadsManager roadsManager;

    private void Awake()
    {
        SetTriggers();
    }

    void SetTriggers()
    {
        for (int i = 0; i < roadTriggers.Count; i++)
        {
            roadTriggers[i].SetTrigger(this);
        }
    }

    public void TriggerIsTriggered(int triggerStartPointNumber, float totalTimeOnRoad = 0, VehicleUnit.TYPE vehicleType = VehicleUnit.TYPE.car)
    {
        Logging.Log("RoadTriggersControl: trigger start point number is " + triggerStartPointNumber);

        if (!isRoadEnd)
        {
            // to calculate emptiness of start point
            roadsManager.WhenRoadTriggerExit(triggerStartPointNumber);
        }
        else
        {
            roadsManager.WhenRemoveVehicleFromRoad(triggerStartPointNumber, totalTimeOnRoad, vehicleType);
        }
    }
}
