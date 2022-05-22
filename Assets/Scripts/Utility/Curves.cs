using UnityEngine;
using System.Collections.Generic;

public static class Curves
{
    public static Vector2 GetBezierPoint(float t, List<Vector2> points)
    {
        // Return Vector2 point of Bezier curve determined be points and t parameter

        float u = 1 - t;
        Vector2 p = new Vector2();

        if (points.Count == 3)
        {
            // tree points curve
            p = t * t * points[2];
            p += u * t * points[1];
            p += u * u * points[0];

        }
        else if (points.Count == 4)
        {
            // four points curve
            p = u * u * u * points[0];
            p += 3 * u * u * t * points[1];
            p += 3 * u * t * t * points[2];
            p += t * t * t * points[3];
        }

        return p;
    }

    public static float GetCurveLength(List<Vector2> points)
    {
        float length = 0f;
        if (points.Count == 2)
        {
            // curve is a line
            length = Vector2.Distance(points[1], points[0]);
        }
        else if (points.Count > 2)
        {
            // curve is a bezier curve

            //length = InerateForBestSegmentsNumber(points);
            length = GetBezierCurveLength(11, points);

        }
        return length;
    }

    static float InerateForBestSegmentsNumber(List<Vector2> points)
    {
        // first approximation
        int segmentsNumber = points.Count;
        float length = GetBezierCurveLength(segmentsNumber, points);

        float sensitivity = 0.001f;
        
        // start inerating        

        while (true)
        {
            // increase segments number
            segmentsNumber++;
            // calculate new
            float newLength = GetBezierCurveLength(segmentsNumber, points);

            if (Mathf.Abs(newLength - length) / length <= sensitivity || segmentsNumber > 20)
            {
                length = newLength;
                break;
            }

            length = newLength;
        }

        return length;
    }


    static float GetBezierCurveLength(int segmentsNumber, List<Vector2> points)
    {
        float length = 0f;
        float tStep = 1f / segmentsNumber;
        float t = 0f;
        Vector2 currentCurvePoint = Vector2.zero;
        Vector2 nextCurvePoint = Vector2.zero;

        while (t <= 1)
        {
            currentCurvePoint = GetBezierPoint(t, points);
            nextCurvePoint = GetBezierPoint(t + tStep, points);
            length += Vector2.Distance(nextCurvePoint, currentCurvePoint);
            t += tStep;
        }

        return length;
    }
}
