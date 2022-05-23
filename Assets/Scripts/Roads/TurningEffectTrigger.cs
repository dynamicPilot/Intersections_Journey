using UnityEngine;

public class TurningEffectTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            // turning effect if needed
            collision.gameObject.GetComponent<VehicleUnit>().StartTurningLightEffectWhenDetectCrossroads();
        }
    }
}
