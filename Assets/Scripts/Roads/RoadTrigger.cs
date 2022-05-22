using UnityEngine;

public class RoadTrigger : Trigger
{
    //[Header("Direction")]
    //[SerializeField] private bool needSetDirection = false;
    //[SerializeField] private bool needCarToFollowDirectionControl = false;
    //[SerializeField] private VehicleScanner.DIRECTION directionToSet = VehicleScanner.DIRECTION.none;
    //RoadTriggersControl roadTriggersControl;
    //int triggerIndex = -1;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("Train")) && collision is PolygonCollider2D)
        {
            // use defaults arguments values -- do not need any actual values, because it is just the road state trigger
            VehiclePassTheBorder();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
    //    {
    //        //Logging.Log("BorderTrigger: car start turn!");
    //        if (needSetDirection)
    //        {
    //            collision.gameObject.GetComponent<VehicleScanner>().ChangeDirection(directionToSet, TriggerStartPointNumber, needCarToFollowDirectionControl);
    //            Logging.Log("BorderTrigger: set direction to car " + collision.gameObject.GetComponent<VehicleScanner>().Direction);
    //        }
    //    }
    //}


    //public void SetTriggerIndex(int index, RoadTriggersControl newRoadTriggerControl)
    //{
    //    triggerIndex = index;
    //    roadTriggersControl = newRoadTriggerControl;
    //}

    //void CarPassTheBorder()
    //{
    //    if (roadTriggersControl == null || triggerIndex < 0) return;

    //    roadTriggersControl.TriggerIsTriggered(triggerIndex);
    //}
}
