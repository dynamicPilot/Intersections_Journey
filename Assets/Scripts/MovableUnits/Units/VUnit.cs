using System.Collections.Generic;
using IJ.Utilities;
using IJ.MovableUnits.MediatorAndComponents;
using UnityEngine;


namespace MovableUnits.Units
{
    public interface IVUnit
    {
        public void StartVehicle(List<Path> _paths, int _managerIndex, bool _stopIsParking = false);
    }

    [RequireComponent(typeof(VScanner))]
    [RequireComponent(typeof(VMover))]
    [RequireComponent(typeof(VCrasher))]
    public class VUnit : MonoBehaviour, IVUnit
    {
        private protected IVMover mover;
        private VCrasher crasher;
        private VScanner scanner;
        protected private VInfo info;
        private VEffects effects;
        private VRepairSiteTag repairSiteTag;
        private Rigidbody2D _rigidbody;
        private RouterComponent routerComponent;
        [SerializeField] private VRoute router;

        private protected bool pauseUpdate = true;
        private bool stopInParking = false;
        private protected bool needUpdateTotalTime = false;
        private bool needUpdateCounter = false;

        private int counter = 0;

        private MoveViaPath _movable;
        private void Awake()
        {
            mover = GetComponent<VMover>();
            crasher = GetComponent<VCrasher>();
            scanner = GetComponent<VScanner>();
            info = GetComponent<VInfo>();
            effects = GetComponent<VEffects>();
            repairSiteTag = GetComponent<VRepairSiteTag>();
            _rigidbody = GetComponent<Rigidbody2D>();
            MakeMediatorAndSet();
        }

        private void OnEnable()
        {
            crasher.OnStartCollisionNotify += StartCrash;
            crasher.OnStartCollision += DelayUpdaterPause;
            crasher.OnEndCollision += EndCrash;
        }

        private void OnDisable()
        {
            crasher.OnStartCollisionNotify -= StartCrash;
            crasher.OnStartCollision -= DelayUpdaterPause;
            crasher.OnEndCollision -= EndCrash;
        }

        private void OnDestroy()
        {
            try
            {
                router.OnPathEnded -= StopVehicel;
            }
            catch { };
        }

        private void FixedUpdate()
        {
            if (pauseUpdate) return;

            FixedUpdateStuff(Time.fixedDeltaTime);

            _movable.Reset();
            if (mover.CalculateVelocity(Time.fixedDeltaTime, _movable, new IGetDistanceInfo[3] { scanner.TrafficLightInfo, scanner.UnitsInfo, repairSiteTag }))
            {
                router.GetPathPointToMove(_movable);
                _movable.MoveAndRotate();
            }
        }

        public virtual void StartVehicle(List<Path> _paths, int _managerIndex, bool _stopIsParking = false)
        {
            stopInParking = _stopIsParking;
            SetRouter(_paths);
            SetUnitInStartPosition();

            info.SetInfo(_managerIndex);
            mover.InitialVelocity();
            scanner.StartScanner(router);
            effects.StartEffects();

            gameObject.SetActive(true);
            pauseUpdate = false;

            needUpdateTotalTime = true;
            needUpdateCounter = false;
        }

        protected virtual void StopVehicel()
        {
            pauseUpdate = true;
            if (router != null) router.OnPathEnded -= StopVehicel;

            //stop effects
            scanner.StopScanner();
            effects.StopEffect();

            if (!stopInParking)
            {
                gameObject.SetActive(false);
                info.FreeUnitIndex(router.TargetRoutePoint());
            }
        }

        void SetRouter(List<Path> _paths)
        {
            if (_paths == null && _paths.Count < 2) StopVehicel();
            router = new VRoute(_paths, routerComponent, !stopInParking);
            router.OnPathEnded += StopVehicel;
        }

        void SetUnitInStartPosition()
        {
            _movable = new MoveViaPath(_rigidbody);
            _movable.SetNewPosition(router.StartPointPosition());
            _movable.MoveAndRotate();
        }

        void StartCrash()
        {
            needUpdateCounter = false;
            pauseUpdate = true;
            needUpdateTotalTime = false;
        }

        void DelayUpdaterPause(Vector3 contactPosition, int otherIndex)
        {
            counter = 5;
            needUpdateCounter = true;
        }

        protected virtual void FixedUpdateStuff(float deltaT)
        {
            if (needUpdateCounter)
            {
                counter--;
                if (counter == 0) StartCrash();
            }

            if (needUpdateTotalTime) needUpdateTotalTime = scanner.UpdateTotalTime(Time.fixedDeltaTime);
        }

        public virtual void EndCrash()
        {
            pauseUpdate = true;
            stopInParking = false;
            needUpdateCounter = false;
            StopVehicel();
        }

        public void StartTurningEffect()
        {
            effects.ChangeIsInTurn(true, router.NextPartTurn());
        }

        protected void MakeMediatorBasic(MakeUnitMediator makeMediator)
        {
            VMover temp = mover as VMover;
            IHoldMoverComponent moverHolder = temp.State as IHoldMoverComponent;
            IHoldEffectsComponent effectsHolder = effects as IHoldEffectsComponent;
            routerComponent = makeMediator.CreateAndSet(ref moverHolder, ref effectsHolder, ref repairSiteTag, ref crasher);
        }

        protected virtual void MakeMediatorAndSet()
        {
            MakeMediatorBasic(new MakeUnitMediator());
        }
    }
}

