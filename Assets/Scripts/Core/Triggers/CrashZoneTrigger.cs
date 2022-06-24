using UnityEngine;

public class CrashZoneTrigger : MonoBehaviour
{
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Train") && collision is PolygonCollider2D)
        {
            // turning effect if needed
            collision.gameObject.GetComponent<TrainEffects>().EnterCrashZone(circleCollider.bounds.center, circleCollider.radius);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Train") && collision is PolygonCollider2D)
        {
            // turning effect if needed
            collision.gameObject.GetComponent<TrainEffects>().ExitCrashZone();
        }
    }
}
