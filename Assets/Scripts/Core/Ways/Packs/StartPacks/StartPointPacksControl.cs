using IJ.Core.Ways;
using UnityEngine;

public class StartPointPacksControl : MonoBehaviour
{
    [SerializeField] private StartPointPack[] packs;
    [SerializeField] private WayPoints wayPoints;

    public void SetLevel(StartPointPackCorrection[] corrections)
    {
        foreach (StartPointPackCorrection correction in corrections)
        {
            wayPoints.MakePointCorrections(packs[correction.PackIndex].StartPointNumber, correction.NewStartPointPosition);
            correction.SetRoadTriggerPosition(packs[correction.PackIndex].RoadTrigger);
        }

        wayPoints.CalculatePaths();
    }
}
