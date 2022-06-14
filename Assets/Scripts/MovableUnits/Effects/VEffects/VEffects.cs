using IJ.Utilities;
using IJ.MovableUnits.MediatorAndComponents;
using UnityEngine;

public interface IViewSortingLayer
{
    public void SetSortingLayerById(int id);
}

public class VEffects : MonoBehaviour, IHoldEffectsComponent
{
    [SerializeField] private WheelSmoke wheelSmoke;
    [SerializeField] private TurningEffect turningEffect;
    
    protected private IViewSortingLayer sortingLayerControl;

    protected private string defaultSortingLayer = "Cars";

    private void Awake()
    {
        sortingLayerControl = GetComponent<IViewSortingLayer>();
    }

    public virtual void StartEffects()
    {
        TurnOnOffLights(true);
        SetSortingLayerById(SortingLayer.NameToID(defaultSortingLayer));
    }

    public virtual void StopEffect()
    {
        TurnOnOffLights(false);
    }

    // wheel smoke
    public void MakeWheelSmoke()
    {
        if (wheelSmoke != null)
        {
            wheelSmoke.StartEffect();
        }
    }

    public void ChangeIsInTurn(bool _isInTurn, Path.TURN turn)
    {
        if (_isInTurn)
        {
            if (turn != Path.TURN.none) StartTurningEffect(turn);
        }
        else
        {
            StopTurningEffect();
        }
    }

    public virtual void SetSortingLayerById(int id)
    {
        if (turningEffect != null) turningEffect.SetSortingLayerById(id);
        if (sortingLayerControl != null) sortingLayerControl.SetSortingLayerById(id);
    }

    #region turning effect
    // turning effect
    void TurnOnOffLights(bool mode)
    {
        if (turningEffect != null) turningEffect.TurnOnOffLights(mode);
    }

    void StopTurningEffect()
    {
        if (turningEffect != null) turningEffect.StopTurningEffect();
    }

    void StartTurningEffect(Path.TURN turn)
    {
        if (turningEffect != null) turningEffect.StartTurningEffect(turn);
    }

    #endregion
}





//public void SetDefaultSortingLayer()
//{
//    //if (viewControl != null)
//    //{
//    //    //viewControl.SetDefaultSortingLayer();

//    //    //if (turningEffect != null) turningEffect.SetSortingLayerById(viewControl.GetDefaultSortingLayerID());
//    //    //if (cargo != null) cargo.SetSortingLayerById(viewControl.GetDefaultSortingLayerID());
//    //}
//}


// wheel smoke




// cargo effect

//public void MakeCargo(bool mode)
//{
//    if (cargo != null)
//    {
//        cargo.SetCargoState(mode);

//    }
//}

//// timer effect
//public void TimerEnableOrNot(bool mode)
//{
//    if (vehicleTimer != null)
//        vehicleTimer.enabled = mode;
//}

//public void SetNewVehicleParametersForTimer() //EmergencyCar newVehicle
//{
//    //if (vehicleTimer != null)
//        //vehicleTimer.SetNewVehicleParameters(newVehicle);
//}

//public void  CheckNewVelocityForTimer(float newVelocity, float deltaTime)
//{
//    if (vehicleTimer != null)
//        vehicleTimer.CheckNewVelocity(newVelocity, deltaTime);
//}

//public void HideMarkAndTimerForTimer()
//{
//    if (vehicleTimer != null)
//        vehicleTimer.HideMarkAndTimer();
//}

//// train effect
//public bool CheckContactPositionToBeInCrashZone(Vector2 contactPosition)
//{
//    if (trainEffects != null)
//    {
//        return trainEffects.IsContactPointInCrashZone(contactPosition);
//    }

//    return false;
//}




//[Header("Sorting Layers")]
//[SerializeField] string defaultSortingLayer = "Cars";

//public void SetVehicleView(VSpritePack spritePack)
//{
//    //if (spritePack.body != null && body != null) body.sprite = spritePack.body;
//    //if (spritePack.leftLightCover != null && leftLightCover != null) leftLightCover.sprite = spritePack.leftLightCover;
//    //if (spritePack.rightLightCover != null && rightLightCover != null) rightLightCover.sprite = spritePack.rightLightCover;
//}

//public void SetNewSortingLayerByID(int newID)
//{
//    //if (body != null) body.sortingLayerID = newID;
//    //if (leftLightCover != null) leftLightCover.sortingLayerID = newID;
//    //if (rightLightCover != null) rightLightCover.sortingLayerID = newID;

//    //foreach (RendererPackUnit pack in packs)
//    //{
//    //    pack.SetRendererSortingLayerByID(newID);
//    //}
//}

//public void SetDefaultSortingLayer()
//{
//    //if (body != null) body.sortingLayerID = SortingLayer.NameToID(defaultSortingLayer);
//    //if (leftLightCover != null) leftLightCover.sortingLayerID = SortingLayer.NameToID(defaultSortingLayer);
//    //if (rightLightCover != null) rightLightCover.sortingLayerID = SortingLayer.NameToID(defaultSortingLayer);

//    //SetNewSortingLayerByID(SortingLayer.NameToID(defaultSortingLayer));
//}

//public int GetDefaultSortingLayerID()
//{
//    return SortingLayer.NameToID(defaultSortingLayer);
//}