using System.Collections.Generic;
using UnityEngine;

public static class ScannerUtilities
{
    private static float minVectorDotProductValueToView = -0.8f;
    public static float DistanceToSingleObjectWithZeroVelocity(Vector3 selfPosition, Vector3 objectPosition, float gap = 0f)
    {
        return Vector3.Distance(selfPosition, objectPosition) - gap;
    }

    public static float DistanceToSingleUnitObject(Vector3 selfPosition, Vector3 selfVector, IPositionShearer unitPosition, float gap = 0f)
    {
        if (unitPosition == null)
        {
            Logging.Log("NULL IPositionShearer");
        }
        Vector3 unitVector = unitPosition.GetSetToOriginVelocityVector();
        Vector2 unitSize = unitPosition.GetSize();

        if (Vector2.Dot(selfVector, unitVector) < minVectorDotProductValueToView)
        {
            return -100f;
        }

        float angleToInterpolation = Vector2.Angle(selfVector, unitVector) % 90f;
        float vehicleSize = Mathf.Lerp(unitSize.y, unitSize.x, angleToInterpolation / 90f);

        float newDistance = Vector2.Distance(selfPosition, unitPosition.GetPosition()) - vehicleSize - gap;
        return newDistance;
    }

    public static void RemoveUnitsAtIndexes(List<int> indexed, ref List<IPositionShearer> positions, ref List<IVelocityShearer> velocities)
    {
        for (int i = 0; i < indexed.Count; i++)
        {
            positions.RemoveAt(i);
            velocities.RemoveAt(i);
        }
    }

    public static void RemoveUnitAtIndex(int index, ref List<IPositionShearer> positions, ref List<IVelocityShearer> velocities)
    {
        positions.RemoveAt(index);
        velocities.RemoveAt(index);
    }

    public static void RemoveUnits(List<IPositionShearer> toRemove, ref List<IPositionShearer> positions, ref List<IVelocityShearer> velocities)
    {
        foreach (IPositionShearer position in toRemove)
        {
            if (positions.Contains(position))
            {
                RemoveUnitAtIndex(positions.IndexOf(position), ref positions, ref velocities);
            }
        }
    }

    public static void RemoveUnitsWithAnotherDirection(DIRECTION direction, ref List<IPositionShearer> positions, ref List<IVelocityShearer> velocities)
    {
        List<int> indexesToRemove = new List<int>();
        for (int i = 0; i < positions.Count; i++)
        {
            IDirectionShearer unitDirection = positions[i] as IDirectionShearer;
            if (unitDirection.GetDirection() != direction) indexesToRemove.Add(i);
        }
        RemoveUnitsAtIndexes(indexesToRemove, ref positions, ref velocities);
    }

    public static void AddUnits(List<IPositionShearer> newPositions, List<IVelocityShearer> newVelocities, ref List<IPositionShearer> positions, ref List<IVelocityShearer> velocities)
    {
        for(int i = 0; i < newPositions.Count; i++)
        {
            if (!positions.Contains(newPositions[i]))
            {
                positions.Add(newPositions[i]);
                velocities.Add(newVelocities[i]);
            }
        }
    }
}