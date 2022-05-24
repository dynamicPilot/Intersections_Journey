using UnityEngine;

public class EffectsControl : MonoBehaviour
{
    [SerializeField] private WheelSmoke wheelSmoke;
    [SerializeField] private TurningEffect turningEffect;
    [SerializeField] private VehicleViewControl viewControl;

    [Header("Specials")]
    [SerializeField] private Cargo cargo;
    [SerializeField] private VehicleTimer vehicleTimer;
    [SerializeField] private TrainEffects trainEffects;

    // color control
    public void SetVehicleView(VehicleSpritePack pack)
    {
        if (viewControl != null) viewControl.SetVehicleView(pack);
    }

    public void SetDefaultSortingLayer()
    {
        if (viewControl != null)
        {
            viewControl.SetDefaultSortingLayer();
            
            if (turningEffect != null) turningEffect.SetSortingLayerById(viewControl.GetDefaultSortingLayerID());
            if (cargo != null) cargo.SetSortingLayerById(viewControl.GetDefaultSortingLayerID());
        }
    }

    public void SetSortingLayerById(int newID)
    {
        if (viewControl != null)
        {
            viewControl.SetNewSortingLayerByID(newID);

            if (turningEffect != null) turningEffect.SetSortingLayerById(newID);
            if (cargo != null) cargo.SetSortingLayerById(newID);
        }
    }

    // wheel smoke
    public void MakeWheelSmoke()
    {
        if (wheelSmoke != null)
        {
            wheelSmoke.StartEffect();
        }
    }

    // turning effect
    public void TurnOnOffLights(bool mode)
    {
        if (turningEffect != null) turningEffect.TurnOnOffLights(mode);
    }

    public void StopTurningEffect()
    {
        if (turningEffect != null) turningEffect.StopTurningEffect();
    }

    public void StartTurningEffect(Path.TURN turn)
    {
        if (turningEffect != null) turningEffect.StartTurningEffect(turn);
    }

    // cargo effect

    public void MakeCargo(bool mode)
    {
        if (cargo != null)
        {
            cargo.SetCargoState(mode);

        }
    }

    // timer effect
    public void TimerEnableOrNot(bool mode)
    {
        if (vehicleTimer != null)
            vehicleTimer.enabled = mode;
    }

    public void SetNewVehicleParametersForTimer(EmergencyCar newVehicle, VehicleManager vehicleManager)
    {
        if (vehicleTimer != null)
            vehicleTimer.SetNewVehicleParameters(newVehicle, vehicleManager);
    }

    public void  CheckNewVelocityForTimer(float newVelocity, float deltaTime)
    {
        if (vehicleTimer != null)
            vehicleTimer.CheckNewVelocity(newVelocity, deltaTime);
    }

    public void HideMarkAndTimerForTimer()
    {
        if (vehicleTimer != null)
            vehicleTimer.HideMarkAndTimer();
    }

    // train effect
    public bool CheckContactPositionToBeInCrashZone(Vector2 contactPosition)
    {
        if (trainEffects != null)
        {
            return trainEffects.IsContactPointInCrashZone(contactPosition);
        }

        return false;
    }
}
