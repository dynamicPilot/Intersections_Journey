using UnityEngine;

public class TrainEffects : MonoBehaviour
{
    private Vector2 crashZoneCenter;
    private float crashZoneRadius;
    private bool isInCrashZone = false;

    public void EnterCrashZone(Vector2 newCenter, float newRadius)
    {
        isInCrashZone = true;

        crashZoneCenter = newCenter;
        crashZoneRadius = newRadius;
    }

    public void ExitCrashZone()
    {
        isInCrashZone = false;
    }

    public bool IsContactPointInCrashZone(Vector2 contactPoint)
    {
        if (isInCrashZone)
        {
            return (contactPoint.x > crashZoneCenter.x - crashZoneRadius) && (contactPoint.x < crashZoneCenter.x + crashZoneRadius) &&
                (contactPoint.y > crashZoneCenter.y - crashZoneRadius) && (contactPoint.y < crashZoneCenter.y + crashZoneRadius);
        }

        return false;
    }
}
