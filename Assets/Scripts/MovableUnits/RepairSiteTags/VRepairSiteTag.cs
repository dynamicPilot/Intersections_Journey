using System.Collections.Generic;
using MovableUnits.MediatorAndComponents;
using UnityEngine;


public class VRepairSiteTag : MonoBehaviour, IGetDistanceInfo
{
    RepairSiteComponent component;

    protected Vector3 _point;
    float _velocity;
    int _index;

    bool _isInRepairSite = false;

    public void SetRepairSiteComponent(RepairSiteComponent _component)
    {
        component = _component;
    }

    public virtual void EnterRepairSite(Vector3 point, float velocity, int index)
    {
        _point = point;
        _velocity = velocity;
        _index = index;

        _isInRepairSite = true;
        component.DoInEnterRepairSite();
    }

    public virtual void ExitRepairSite()
    {
        _isInRepairSite = false;
        component.DoInExitRepairSite();
    }

    public virtual List<float> GetInfo()
    {
        if (_isInRepairSite)
            return new List<float> { _velocity, Vector2.Distance(transform.position, _point) };
        else
            return new List<float> { -100f, -100f };
    }
}
