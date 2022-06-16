using IJ.MovableUnits.MediatorAndComponents;

public class MakeUnitMediator
{
    protected IMediatorConstructor _constructorInterface;

    public RouterComponent CreateAndSet(ref IHoldMoverComponent moverHolder, ref IHoldEffectsComponent effectsHolder, 
        ref VRepairSiteTag tag, ref VCrasher crasher)
    {
        GetContructorInterface();
        UnitMediatorContructor _constractor = _constructorInterface as UnitMediatorContructor;

        _constractor.SetHolderForMoverComponent(ref moverHolder);
        _constractor.SetHolderForEffectsComponent(ref effectsHolder);
        _constractor.SetComponentToRepairSiteTag(ref tag);
        _constractor.SetComponentToVCrasher(ref crasher);

        return _constructorInterface.GetRouterComponent();
    }

    protected virtual void GetContructorInterface()
    {
        Creator _creator = new UnitMediatorCreator();
        _constructorInterface = _creator.CreateMediator();
    }
}

public class MakeEmergencyUnitMediator : MakeUnitMediator
{
    protected override void GetContructorInterface()
    {
        Creator _creator = new EmergencyUnitMediatorCreator();
        _constructorInterface = _creator.CreateMediator();
    }

    public void SetHolderForEmergencyComponent(ref IHoldEmergencyUnitComponent holder)
    {
        EmergencyUnitMediatorContructor _constractor = _constructorInterface as EmergencyUnitMediatorContructor;
        _constractor.SetHolderForEmergencyComponent(ref holder);
    }
}

public class MakeRepairCarMediator : MakeUnitMediator
{
    protected override void GetContructorInterface()
    {
        Creator _creator = new RepairCarMediatorCreator();
        _constructorInterface = _creator.CreateMediator();
    }

    public void SetHolderForReapirCarComponent(ref IHoldRepairCarComponent holder)
    {
        RepairCarMediatorContructor _constractor = _constructorInterface as RepairCarMediatorContructor;
        _constractor.SetHolderForRepairCarComponent(ref holder);
    }
}
