using IJ.Core.Ways;
using IJ.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayTester : MonoBehaviour
{
    [SerializeField] private Transform testPoint;

    [Header("Route")]
    [SerializeField] private int startPoint = 4;
    [SerializeField] private int endPoint = 8;

    [Header("Simulation")]
    [SerializeField] private bool needTesting = false;
    [SerializeField] private float velocity;
    [SerializeField] private float deltaT = 0.01f;

    [Header("Scripts")]
    [SerializeField] private WayCreator wayCreator;
    [SerializeField] private WayPoints wayPoints;

    float t = 0f;
    float angle = 0f;

    int pathPartIndex = 0;
    bool isLine = true;

    bool isTesting = false;

    List<Vector2> points = new List<Vector2>();
    List<Path> paths = new List<Path>();
    List<int> route = new List<int>();

    WaitForSeconds updateTimer;


    private void Start()
    {
        if (needTesting) StartSimulation();
    }

    private void FixedUpdate()
    {
        if (!isTesting)
        {
            return;
        }

        t += (float) Time.deltaTime * velocity / paths[pathPartIndex].CurveLength;
  
        if (Mathf.Abs(1f - t) < 0.001f || t > 1) t = 1f;

        Vector2 newPoint = new Vector2();

        // get new point
        if (isLine)
        {
            newPoint = Vector2.Lerp(points[0], points[1], t);
        }
        else
        {
            newPoint = Curves.GetBezierPoint(t, points);
        }

        //Logging.Log("WayTester: make step. Current t is " + t + " next point is " + newPoint);

        // calculate rotation angle 
        angle = Mathf.Atan2(newPoint.x - testPoint.position.x, newPoint.y - testPoint.position.y) * Mathf.Rad2Deg;

        // clockwise or not
        angle = 360 - angle;// Mathf.Sign((newPoint.x - testPoint.position.x));

        Quaternion newRotation = Quaternion.Euler(testPoint.rotation.x, testPoint.rotation.y, angle);

        //Logging.Log("TEST: t is " + t + " point is: x " + newPoint.x + " y " + newPoint.y + " angle is " + angle);
        testPoint.position = newPoint;

        //Logging.Log("TEST: new rotation " + newRotation.z + " current rotation " + testPoint.rotation.z);

        testPoint.rotation = newRotation;

        if (t >= 1)
        {
            isTesting = false;
            StartNewPathPart();
        }
    }

    private void StartSimulation()
    {
        Logging.Log("WayTester: send call to SearchForRoute");
        route = wayCreator.SearchForRoute(startPoint, endPoint);
        
        if (route == null) return;

        Logging.Log("WayTester: get route ... ");
        foreach(int point in route) Logging.Log("WayTester: point " + point);

        // full paths
        Logging.Log("WayTester: send call to GetPathsForRoute");
        paths = wayPoints.GetPathsForRoute(route);

        if (paths == null) return;
        else if (paths.Count < 1) return;

        // get the first
        pathPartIndex = -1;

        StartNewPathPart();
    }

    private void StartNewPathPart()
    {       
        pathPartIndex++;

        if (pathPartIndex >= paths.Count)
        {
            Logging.Log("WayTester: finish!");
            return;
        }

        // next path
        points = paths[pathPartIndex].CurvePoints;

        Logging.Log("WayTester: get points ... ");
        foreach (Vector2 point in points) Logging.Log("WayTester: point " + point.x + " " + point.y);

        isLine = (points.Count == 2);

        t = 0f;
        testPoint.transform.position = points[0];

        Logging.Log("WayTester: start moving next part ... is on line " + isLine);
        isTesting = true;
    }

    IEnumerator MoveAlongCurve()
    {
        yield return updateTimer;

        // get new point
        Vector2 newPoint = Curves.GetBezierPoint(t, points);


        // calculate rotation angle 
        angle = Mathf.Atan2(newPoint.x - testPoint.position.x, newPoint.y - testPoint.position.y) * Mathf.Rad2Deg;

        // clockwise or not
        angle = 360 - angle;// Mathf.Sign((newPoint.x - testPoint.position.x));

        Quaternion newRotation = Quaternion.Euler(testPoint.rotation.x, testPoint.rotation.y, angle);

        //Logging.Log("TEST: t is " + t + " point is: x " + newPoint.x + " y " + newPoint.y + " angle is " + angle);
        testPoint.position = newPoint;

        //Logging.Log("TEST: new rotation " + newRotation.z + " current rotation " + testPoint.rotation.z);

        testPoint.rotation = newRotation;

        t += deltaT;

        if (t <= 1f) StartCoroutine(MoveAlongCurve());
    }
}
