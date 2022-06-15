using UnityEngine;

public class RoadTrigger : Trigger
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("Train")) && collision is PolygonCollider2D)
        {
            // use defaults arguments values -- do not need any actual values, because it is just the road state trigger
            VehiclePassTheBorder();
        }
    }
}
