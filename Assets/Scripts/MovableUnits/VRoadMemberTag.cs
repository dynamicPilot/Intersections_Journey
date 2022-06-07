using UnityEngine;

public interface IRoadInfoShearer
{
    public UnitsShearers GetUnitsToAddToVehicleToFollow(IDirectionShearer directionShearer);
}
/// <summary>
/// Hold link to Storage to get units on the same road.
/// </summary>
/// 
public class VRoadMemberTag : MonoBehaviour, IRoadInfoShearer
{
    private IGetUnitsOnRoad _roadInfo;
    private int _index;

    public UnitsShearers GetUnitsToAddToVehicleToFollow(IDirectionShearer directionShearer)
    {
        return _roadInfo.GetUnitsToAddToVehicleToFollow(directionShearer.GetDirection(), directionShearer.GetRoadStartPoint(), _index);
    }

    public void SetRoadInfo(IGetUnitsOnRoad roadInfo, int index)
    {
        _roadInfo = roadInfo;
        _index = index;
    }

}
