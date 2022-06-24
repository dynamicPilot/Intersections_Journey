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
        if (sortingLayerControl!= null) SetSortingLayerById(SortingLayer.NameToID(defaultSortingLayer));
    }

    public virtual void StopEffect()
    {
        TurnOnOffLights(false);
    }

    public void InEnterCrash()
    {
        if (wheelSmoke != null)
        {
            wheelSmoke.StartEffect();
        }
    }

    public void ChangeIsInTurn(bool _isInTurn, Path.TURN turn)
    {
        if (turningEffect == null) return;

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
