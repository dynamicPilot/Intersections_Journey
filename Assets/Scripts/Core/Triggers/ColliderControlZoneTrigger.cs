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
    [SerializeField] private List<DIRECTION> _directionsToBeAffected = new List<DIRECTION>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            if (needPointsNumberControl)
            {
                VRoadMember unit = collision.gameObject.GetComponent<VRoadMember>();
                if (!CheckPointNumber(unit.StartPathPoints()) && !CheckPointNumber(unit.EndPathPoints())) return;
            }
            else if (needDirectionControl)
            {
                if (!CheckDirection(collision.gameObject.GetComponent<VScanner>().GetDirection())) return;
            }
           
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    bool CheckPointNumber(int pointNumberToCheck)
    {
        return pointsNumberToBeAffected.Contains(pointNumberToCheck);
    }

    bool CheckDirection(DIRECTION directionToCheck)
    {
        return _directionsToBeAffected.Contains(directionToCheck);
    }

}
