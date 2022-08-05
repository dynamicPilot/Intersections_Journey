using System.Collections.Generic;
using UnityEngine;

public interface ICrossroadsSpeedLimit
{
    public void CrossroadsSpeedLimit(bool isOn);
}
public interface IVelocityShearer
{
    public abstract float GetVelocity();
    public abstract bool GetInCrashAndNonActive();
}
public interface IVMover
{
    public abstract bool CalculateVelocity(float deltaT, ISetDistanceToMove movable, IGetDistanceInfo[] infos);
    public void InitialVelocity();
}


public class VMover : MonoBehaviour, IVelocityShearer, IVMover, ICrossroadsSpeedLimit
{
    [Header("Parameters")]
    [SerializeField] private float maxVelocity;
    [SerializeField] private float maxTurningVelocity;
    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float normalAcceleration;
    [SerializeField] private float turningAcceleration;

    [SerializeField] private float _velocity;
    [SerializeField] private float _prevVelocity = 0f;
    [SerializeField] private float _velocitySensitivity = 0.01f;

    [SerializeField] private float _acceleration;

    [SerializeField] private VMoverState _state = new VMoverState();
    [SerializeField] private bool _needLog = false;
    public VMoverState State { get => _state; }

    public float GetVelocity()
    {
        if (_state.IsInCrash) return 0f;
        return _velocity;
    }

    public bool CalculateVelocity(float deltaT, ISetDistanceToMove movable, IGetDistanceInfo[] infos)
    {
        _prevVelocity = _velocity;
        float newAcceleration = _maxAcceleration;

        for (int i = 0; i < infos.Length; i++)
        {
            if (i == infos.Length - 1 && !_state.InRepairSite) break;

            newAcceleration = CorrectAccelerationAccordingToInfo(infos[i], newAcceleration);
        }

        _acceleration = newAcceleration;
        _velocity += _acceleration * deltaT;

        VelocityLimiter();
        _state.CheckZeroVelocityState(_prevVelocity, _velocity);

        movable.SetMovingParams(GetMediumStepVelocity(), _acceleration, deltaT);

        return (_velocity != 0);
    }

    float CorrectAccelerationAccordingToInfo(IGetDistanceInfo info, float newAcceleration)
    {
        List<float> scannerInfo = info.GetInfo();

        for (int i = 0; i < scannerInfo.Count; i += 2)
        {
            newAcceleration = Mathf.Min(CalculateAcceleration(scannerInfo[i], scannerInfo[i + 1]), newAcceleration);
            if (_needLog)
            {
                Logging.Log("   new acceleration " + newAcceleration);
            }
        }

        return newAcceleration;
    }

    float CalculateAcceleration(float unitVelocity, float unitDistance)
    {
        if (_needLog)
        {
            Logging.Log("   unit velocity " + unitVelocity + "    unit distance " + unitDistance);
        }
        
        float possibleAcceleration = MaxPossibleAcceleration();

        if (unitDistance < -10f || unitVelocity < 0)
        {
            return possibleAcceleration;
        }
        else if (unitDistance <= 0)
        {
            if (_velocity > _velocitySensitivity) return -1 * _maxAcceleration;
            else return possibleAcceleration;
        }
        //else if (_velocity == 0 && unitVelocity > 1f && unitDistance > 0)
        //{
        //    return possibleAcceleration;
        //}

        return (Mathf.Pow(unitVelocity, 2) - Mathf.Pow(_velocity, 2)) / (2 * unitDistance);
    }

    float GetMediumStepVelocity()
    {
        return (_velocity + _prevVelocity) / 2;
    }

    void VelocityLimiter()
    {
        if (_velocity < _velocitySensitivity && _velocity != 0)
        {
            _velocity = 0;
            _acceleration = 0;
        }
        else if (_state.InTurnOrHaveLimit() && _velocity > maxTurningVelocity)
        {
            _velocity = maxTurningVelocity;
        }
        else if (_velocity > maxVelocity)
        {
            _velocity = maxVelocity;
        }
    }

    float MaxPossibleAcceleration()
    {
        if (_state.AtAnyState()) return turningAcceleration;
        
        return normalAcceleration;
    }

    public void CrossroadsSpeedLimit(bool isOn)
    {
        _state.CrossroadsSpeedLimit(isOn);
    }

    public void InitialVelocity()
    {
        _velocity = maxVelocity / 2;
        _acceleration = 0f;
    }

    public bool GetInCrashAndNonActive()
    {
        return _state.IsInCrash && !gameObject.activeSelf;
    }
}