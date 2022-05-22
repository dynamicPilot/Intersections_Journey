using System.Collections.Generic;
using UnityEngine;

public class ColliderControlZoneTrigger : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private bool needPointsNumberControl = false;
    [SerializeField] private List<int> pointsNumberToBeAffected = new List<int>();

    [Header("Directions")]
    [SerializeField] private bool needDirectionControl = false;
    [SerializeField] private List<VehicleScanner.DIRECTION> directionsToBeAffected = new List<VehicleScanner.DIRECTION>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            if (needPointsNumberControl)
            {
                Logging.Log("ColliderControlZoneTrigger: checking points....");
                VehicleUnit unit = collision.gameObject.GetComponent<VehicleUnit>();
                if (!CheckPointNumber(unit.GetCurrentPathPartPointNumbers(true)) && !CheckPointNumber(unit.GetCurrentPathPartPointNumbers(false)))
                {
                    Logging.Log("ColliderControlZoneTrigger: no suitable points!");
                    return;
                }
            }
            else if (needDirectionControl)
            {
                if (!CheckDirection(collision.gameObject.GetComponent<VehicleScanner>().Direction)) return;
            }
           
            // turning effect if needed
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            // turning effect if needed
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    bool CheckPointNumber(int pointNumberToCheck)
    {
        return pointsNumberToBeAffected.Contains(pointNumberToCheck);
    }

    bool CheckDirection(VehicleScanner.DIRECTION directionToCheck)
    {
        return directionsToBeAffected.Contains(directionToCheck);
    }

}
