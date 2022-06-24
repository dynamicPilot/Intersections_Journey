namespace IJ.MovableUnits.MediatorAndComponents
{
    public interface IMediatorConstructor
    {
        public void CreateMediator();
        public RouterComponent GetRouterComponent();
    }

    /// <summary>
    /// Abstract Class to declare FactoryMethod.
    /// </summary>
    abstract class Creator
    {
        public abstract IMediatorConstructor FactoryMethod();
        public IMediatorConstructor CreateMediator()
        {
            // get correct creator
            var constructor = FactoryMethod();

            // create mediator
            constructor.CreateMediator();

            return constructor;
        }
    }


    /// <summary>
    /// Concrete creator for UnitMediator.
    /// </summary>
    class UnitMediatorCreator : Creator
    {
        public override IMediatorConstructor FactoryMethod()
        {
            return new UnitMediatorContructor();
        }
    }

    /// <summary>
    /// Concrete creator for EmergencyUnitMediator.
    /// </summary>
    class EmergencyUnitMediatorCreator : Creator
    {
        public override IMediatorConstructor FactoryMethod()
        {
            return new EmergencyUnitMediatorContructor();
        }
    }

    /// <summary>
    /// Concrete creator for RepairCarMediator.
    /// </summary>
    class RepairCarMediatorCreator : Creator
    {
        public override IMediatorConstructor FactoryMethod()
        {
            return new RepairCarMediatorContructor();
        }
    }

    /// <summary>
    /// Concrete creator for TrainMediator.
    /// </summary>
    class TrainMediatorCreator : Creator
    {
        public override IMediatorConstructor FactoryMethod()
        {
            return new TrainMediatorContructor();
        }
    }
}