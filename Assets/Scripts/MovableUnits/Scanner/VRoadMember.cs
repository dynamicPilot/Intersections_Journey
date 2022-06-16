using UnityEngine;

public class VRoadMember : MonoBehaviour, IStartAndEndPathPoints
{
    private IStartAndEndPathPoints _routerInfo;

    public void SetInfo(IStartAndEndPathPoints routerInfo)
    {
        _routerInfo = routerInfo;
    }
    public int EndPathPoints()
    {
        return _routerInfo.EndPathPoints();
    }

    public int StartPathPoints()
    {
        return _routerInfo.StartPathPoints();
    }
}
