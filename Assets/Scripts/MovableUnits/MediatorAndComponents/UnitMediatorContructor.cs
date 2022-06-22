namespace IJ.MovableUnits.MediatorAndComponents
{
    public class UnitMediatorContructor : IMediatorConstructor
    {
        protected RouterComponent _routerComponent;
        protected MoverComponent _moverComponent;
        protected EffectsComponent _effectComponent;
        protected RepairSiteComponent _repairSiteComponent;
        protected CrasherComponent _crasherComponent;

        protected void CreateBasics()
        {
            _routerComponent = new RouterComponent();
            _moverComponent = new MoverComponent();
            _effectComponent = new EffectsComponent();
            _repairSiteComponent = new RepairSiteComponent();
            _crasherComponent = new CrasherComponent();
        }

        public virtual void CreateMediator()
        {
            CreateBasics();

            Mediator _mediator = new Mediator(_moverComponent, _effectComponent);           
            _routerComponent.SetMediator(_mediator);
            _repairSiteComponent.SetMediator(_mediator);
            _crasherComponent.SetMediator(_mediator);
        }

        public void SetHolderForMoverComponent(ref IHoldMoverComponent holder)
        {
            _moverComponent.SetHolder(holder);
            holder.SetMoverComponent(_moverComponent);
        }

        public void SetHolderForEffectsComponent(ref IHoldEffectsComponent holder)
        {
            _effectComponent.SetHolder(holder);
        }

        public void SetComponentToRepairSiteTag(ref VRepairSiteTag tag)
        {
            tag.SetRepairSiteComponent(_repairSiteComponent);
        }

        public void SetComponentToVCrasher(ref VCrasher crasher)
        {
            crasher.SetCrasherComponent(_crasherComponent);
        }

        public RouterComponent GetRouterComponent()
        {
            return _routerComponent;
        }
    }

    public class EmergencyUnitMediatorContructor : UnitMediatorContructor
    {
        EmergencyUnitComponent _component;

        public override void CreateMediator()
        {
            CreateBasics();
            _component = new EmergencyUnitComponent();
            Mediator _mediator = new EmergencyUnitMediator(_moverComponent, _effectComponent, _component);
            _routerComponent.SetMediator(_mediator);
            _repairSiteComponent.SetMediator(_mediator);
            _crasherComponent.SetMediator(_mediator);
        }

        public void SetHolderForEmergencyComponent(ref IHoldEmergencyUnitComponent holder)
        {
            _component.SetHolder(holder);
        }
    }

    public class RepairCarMediatorContructor : UnitMediatorContructor
    {
        RepairCarComponent _component;

        public override void CreateMediator()
        {
            CreateBasics();
            _component = new RepairCarComponent();
            Mediator _mediator = new RepairCarMediator(_moverComponent, _effectComponent, _component);
            _routerComponent.SetMediator(_mediator);
            _repairSiteComponent.SetMediator(_mediator);
            _crasherComponent.SetMediator(_mediator);
        }

        public void SetHolderForRepairCarComponent(ref IHoldRepairCarComponent holder)
        {
            _component.SetHolder(holder);
        }
    }

    public class TrainMediatorContructor : UnitMediatorContructor
    {
        TrainUnitComponent _component;

        public override void CreateMediator()
        {
            CreateBasics();
            _component = new TrainUnitComponent();
            Mediator _mediator = new TrainMediator(_moverComponent, _effectComponent, _component);
            _routerComponent.SetMediator(_mediator);
            _repairSiteComponent.SetMediator(_mediator);
            _crasherComponent.SetMediator(_mediator);
        }

        public void SetHolderForTrainUnitComponent(ref IHoldTrainUnitComponent holder)
        {
            _component.SetHolder(holder);
        }
    }
}

