using IJ.MovableUnits.MediatorAndComponents;
using System.Collections;
using UnityEngine;

public class VRepairSiteTagForRepairCar : VRepairSiteTag, IHoldRepairCarComponent
{
    [SerializeField] private float _timeToRepair = 1f;

    private int _targetIndex;

    private bool _inTargetRepairSite = false;
    private bool _isRepairing = false;

    public delegate void RepairIsMade(int index, VRepairSiteTagForRepairCar tag);
    public event RepairIsMade OnRepairIsMade;

    public void SetTargetIndex(int targetIndex)
    {
        _targetIndex = targetIndex;
    }

    public override void EnterRepairSite(Vector3 point, float velocity, int index)
    {
        if (index == _targetIndex)
        {
            velocity = 0f;
            _inTargetRepairSite = true;
        }

        base.EnterRepairSite(point, velocity, index);
    }
    public void DoInStop()
    {
        if ( _inTargetRepairSite && !_isRepairing)
        {
            if (Mathf.Abs((transform.position - _point).magnitude) < 0.01f)
                StartCoroutine(RepairingTimer());
        }
    }

    IEnumerator RepairingTimer()
    {
        Logging.Log("RepairCar: start repairing");
        _isRepairing = true;
        yield return new WaitForSeconds(_timeToRepair);

        // hide repair side event
        _isRepairing = false;
        if (OnRepairIsMade != null) OnRepairIsMade.Invoke(_targetIndex, this);
    }
}
