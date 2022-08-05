using IJ.MovableUnits.MediatorAndComponents;
using UnityEngine;

[System.Serializable]
public class VMoverState : IHoldMoverComponent
{
    [SerializeField] private bool _inRepairSite = false;
    public bool InRepairSite { get => _inRepairSite; }
    [SerializeField] private bool _inTurn = false;
    [SerializeField] private bool _isCrossroadsLimitOn = false;
    [SerializeField] private bool _isInCrash = false;
    public bool IsInCrash { get => _isInCrash; }

    MoverComponent _moverComponent;

    private float _inStopVelocitySensitivity = 0.02f;

    public void SetMoverComponent(MoverComponent moverComponent)
    {
        _moverComponent = moverComponent;
    }

    public bool InTurnOrHaveLimit()
    {
        return (_inTurn || _isCrossroadsLimitOn);
    }

    public bool AtAnyState()
    {
        return (_inTurn || _isCrossroadsLimitOn || _inRepairSite);
    }

    public void ChangeIsInTurn(bool isInTurn)
    {
        _inTurn = isInTurn;
    }

    public void ChangeIsInRepairSite(bool isInRepairSite)
    {
        _inRepairSite = isInRepairSite;
    }

    public void CrossroadsSpeedLimit(bool isOn)
    {
        _isCrossroadsLimitOn = isOn;
    }

    public void CheckZeroVelocityState(float prevVelocity, float velocity)
    {
        if (prevVelocity != 0 && velocity == 0)
        {
            _moverComponent.DoInStop();
        }
        else if (prevVelocity <= _inStopVelocitySensitivity && velocity > _inStopVelocitySensitivity)
        {
            _moverComponent.DoInRestart();
        }
    }

    public void ChangeIsInCrash(bool isInCrash)
    {
        _isInCrash = isInCrash;
    }
}

