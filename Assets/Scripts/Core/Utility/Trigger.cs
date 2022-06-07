using UnityEngine;

public class Trigger : MonoBehaviour
{
    RoadTriggersControl roadTriggersControl;
    [SerializeField] int triggerStartPointNumber = -1;
    public int TriggerStartPointNumber { get => triggerStartPointNumber; }

    public virtual void SetTrigger(RoadTriggersControl newRoadTriggerControl)
    {
        //triggerIndex = index;
        roadTriggersControl = newRoadTriggerControl;
    }

    public virtual void VehiclePassTheBorder(float totalTimeOnRoad = 0, TYPE vehicleType = TYPE.car)
    {
        if (roadTriggersControl == null || triggerStartPointNumber < 0) return;

        //Logging.Log("Trigger: vehicle pass the border: trigger start point number is " + triggerStartPointNumber);
        roadTriggersControl.TriggerIsTriggered(triggerStartPointNumber, totalTimeOnRoad, vehicleType);       
    }
}
