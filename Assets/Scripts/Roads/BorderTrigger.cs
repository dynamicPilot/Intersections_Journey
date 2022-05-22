using UnityEngine;

public class BorderTrigger : Trigger
{
    [SerializeField] bool isRoadTrigger = false;
    [SerializeField] bool needIsIntoCrossroadsChange = true;

    //[Header("Direction")]
    //[SerializeField] private bool needSetDirection = false;
    //[SerializeField] private bool needCarToFollowDirectionControl = false;
    //[SerializeField] private VehicleScanner.DIRECTION directionToSet = VehicleScanner.DIRECTION.none;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            //Logging.Log("BorderTrigger: car start turn!");
            //if (needSetDirection)
            //{
            //    collision.gameObject.GetComponent<VehicleScanner>().ChangeDirection(directionToSet, TriggerStartPointNumber, needCarToFollowDirectionControl);
            //    Logging.Log("BorderTrigger: set direction to car " + collision.gameObject.GetComponent<VehicleScanner>().Direction);
            //}
            collision.gameObject.GetComponent<VehicleUnit>().EnterOrExitTurn(true);
                
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            //Logging.Log("BorderTrigger: car pass border!");
            collision.gameObject.GetComponent<VehicleScanner>().CrossBorder(needIsIntoCrossroadsChange);


            if (isRoadTrigger)
            {
                VehiclePassTheBorder(collision.gameObject.GetComponent<VehicleUnit>().GetTotalTimeOnRoad(), collision.gameObject.GetComponent<VehicleUnit>().Type);
                collision.gameObject.GetComponent<VehicleUnit>().ResetRoadStartPointNumber();
            }
        }
    }


    public override void SetTrigger(RoadTriggersControl newRoadTriggerControl)
    {
        base.SetTrigger(newRoadTriggerControl);
        isRoadTrigger = true;
    }
}
