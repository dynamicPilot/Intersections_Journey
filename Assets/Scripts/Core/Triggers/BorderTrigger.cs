using UnityEngine;

public class BorderTrigger : Trigger
{
    [SerializeField] bool isRoadTrigger = false;
    [SerializeField] bool isEnterCrossroads = false;
    [SerializeField] bool needIsIntoCrossroadsChange = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            if (isEnterCrossroads) collision.gameObject.GetComponent<ICrossroadsSpeedLimit>().CrossroadsSpeedLimit(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            if (needIsIntoCrossroadsChange)
            {
                collision.gameObject.GetComponent<ICrossBorder>().CrossBorder();

                if (!isEnterCrossroads)
                    collision.gameObject.GetComponent<ICrossroadsSpeedLimit>().CrossroadsSpeedLimit(false);
            }


            if (isRoadTrigger)
            {
                VehiclePassTheBorder(collision.gameObject.GetComponent<IDirectionShearer>().GetTotalTimeOnRoad(), collision.gameObject.GetComponent<VInfo>().Type);
                collision.gameObject.GetComponent<IDirectionShearer>().SetRoadStartPointNumber();
            }
        }
    }


    public override void SetTrigger(RoadTriggersControl newRoadTriggerControl)
    {
        base.SetTrigger(newRoadTriggerControl);
        isRoadTrigger = true;
    }
}
