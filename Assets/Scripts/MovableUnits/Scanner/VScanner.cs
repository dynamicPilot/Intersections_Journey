using UnityEngine;

public enum DIRECTION { nord, west, south, east, none, south_east, nord_east, nord_west, south_west }

public interface IDirectionShearer
{
    public abstract DIRECTION GetDirection();
    public abstract void SetDirection(DIRECTION direction, int roadStartPoint, bool needDirectionControl);
    public abstract int GetRoadStartPoint();
    public float GetTotalTimeOnRoad();
    public void SetRoadStartPointNumber(int number = -1);

    public delegate void DirectionControlChange(DIRECTION _direction, bool _directionControl);
    public event DirectionControlChange OnDirectionControlChangeFromNone;
    public event DirectionControlChange OnDirectionControlChangeToNone;
}
public interface ICrossBorder
{
    public void CrossBorder();
}

[RequireComponent(typeof(VRoadMemberTag))]
[RequireComponent(typeof(VRoadMember))]
public class VScanner : MonoBehaviour, ICrossBorder, IDirectionShearer
{
    [Header("Settings")]
    [SerializeField] private float gap = 0.7f;
    [SerializeField] private float maxDistanceToDetect = 5f;
    [SerializeField] private bool isSelfTrain = false;

    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    [SerializeField] private VScannerUnitsInfo _unitsInfo;
    public VScannerUnitsInfo UnitsInfo { get => _unitsInfo; }

    [SerializeField] private VScannerTrafficLightInfo _trafficLightInfo;
    public VScannerTrafficLightInfo TrafficLightInfo { get => _trafficLightInfo; }
    
    private IPositionShearer _positionShearer;
    public IPositionShearer PositionShearer { get => _positionShearer; }

    private ObjectsDetector _detector;
    [SerializeField] private DIRECTION _direction = DIRECTION.none;

    private bool _isIntoCrossroads;
    private bool haveTrafficLightToFollow = false;
    private bool _needUpdateTotalTime = true;

    [SerializeField] private int _roadStartPointNumber = -1;
    [SerializeField] private float totalTimeOnRoad = 0f;

    public event IDirectionShearer.DirectionControlChange OnDirectionControlChangeFromNone;
    public event IDirectionShearer.DirectionControlChange OnDirectionControlChangeToNone;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        _positionShearer = new VPositionShearer(gap, polygonCollider.bounds.extents, GetComponent<Rigidbody2D>(), boxCollider);
    }

    private void OnDestroy()
    {
        _unitsInfo.Destroy();
    }

    public void StartScanner(IStartAndEndPathPoints _routerInfo)
    {
        GetComponent<VRoadMember>().SetInfo(_routerInfo);

        _unitsInfo = new VScannerUnitsInfo(_positionShearer, maxDistanceToDetect, this, GetComponent<VRoadMemberTag>());
        _trafficLightInfo = new VScannerTrafficLightInfo(_positionShearer);

        _detector = new ObjectsDetector(_routerInfo, _unitsInfo, _trafficLightInfo);
        haveTrafficLightToFollow = false;
        boxCollider.enabled = true;
        polygonCollider.enabled = true;
        _needUpdateTotalTime = true;

        totalTimeOnRoad = 0;
    }

    public void StopScanner()
    {
        boxCollider.enabled = false;
        polygonCollider.enabled = false;
        _isIntoCrossroads = false;
        _needUpdateTotalTime = false;
        _direction = DIRECTION.none;

        _unitsInfo.ClearInfo();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrafficLight") && !_isIntoCrossroads && !haveTrafficLightToFollow)
        {
            haveTrafficLightToFollow = _detector.DetectTrafficLight(collision);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Car") && !isSelfTrain)
        {
            _detector.DetectCar(collision, _direction);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Train"))
        {
            _detector.DetectTrain(collision, _direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrafficLight") && haveTrafficLightToFollow)
        {
            haveTrafficLightToFollow = _detector.UndetectTrafficLight(collision);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Car") && !isSelfTrain)
        {
            _detector.UndetectCar(collision, _direction);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Train"))
        {
            _detector.UndetectTrain(collision, _direction);
        }
    }

    void ChangeDirection(DIRECTION direction, int roadStartPoint, bool directionControl = false)
    {
        DIRECTION prevDirection = _direction;
        _direction = direction;

        // nothing changes
        if (_direction == prevDirection && _direction == DIRECTION.none) return;

        // new direction is not NONE -> check all vehicle and remove with another direction
        if (_direction != DIRECTION.none && prevDirection == DIRECTION.none)
        {
            // new values
            _roadStartPointNumber = roadStartPoint;
            if (OnDirectionControlChangeFromNone != null) OnDirectionControlChangeFromNone.Invoke(direction, directionControl);
        }
        // new direction is NONE but prev is not
        else if (prevDirection != DIRECTION.none && _direction == DIRECTION.none)
        {
            _roadStartPointNumber = -1;
            if (OnDirectionControlChangeToNone != null) OnDirectionControlChangeToNone.Invoke(direction, false);
        }
    }

    public DIRECTION GetDirection()
    {
        return _direction;
    }

    public void CrossBorder()
    {
        _isIntoCrossroads = !_isIntoCrossroads;
        _needUpdateTotalTime = false;
    }

    public bool UpdateTotalTime(float deltaT)
    {
        totalTimeOnRoad += deltaT;
        return _needUpdateTotalTime;
    }

    public int GetRoadStartPoint()
    {
        return _roadStartPointNumber;
    }

    public float GetTotalTimeOnRoad()
    {
        return totalTimeOnRoad;
    }

    public void SetRoadStartPointNumber(int number = -1)
    {
        _roadStartPointNumber = number;
    }

    public void SetDirection(DIRECTION direction, int roadStartPoint, bool needDirectionControl)
    {
        ChangeDirection(direction, roadStartPoint, needDirectionControl);
    }
}
