using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPointsContainer
{
    [SerializeField] private Transform wayPointsContainer;
    [SerializeField] private Vector2 originPoint = Vector2.zero;
    [SerializeField] private bool isSymmetricAgainstOx = false; // Are crossroads symmetrical against X axis?
    [SerializeField] private bool isSymmetricAgainstOy = false; // Are crossroads symmetrical against Y axis?


    public List<Vector2> ReadPoints()
    {
        List<Vector2> points = new List<Vector2>();
        foreach (Transform point in wayPointsContainer.GetComponentsInChildren<Transform>())
        {
            if (point.position.y == 0 && point.position.x == 0) continue;

            points.Add(point.position);

            // add symmetric point
            float deltaX = 0, deltaY = 0;
            if (isSymmetricAgainstOx && point.position.y != originPoint.y)
            {
                deltaY = originPoint.y - point.position.y;
                points.Add(new Vector2(point.position.x, point.position.y + 2 * deltaY));
            }
            if (isSymmetricAgainstOy && point.position.x != originPoint.x)
            {
                deltaX = originPoint.x - point.position.x;
                points.Add(new Vector2(point.position.x + 2 * deltaX, point.position.y));
            }

            if (isSymmetricAgainstOx && isSymmetricAgainstOy && point.position.y != originPoint.y && point.position.x != originPoint.x)
                points.Add(new Vector2(point.position.x + 2 * deltaX, point.position.y + 2* deltaY));
        }

        return points;
    }
}
