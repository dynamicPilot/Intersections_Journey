using UnityEngine;

public interface IGetUnitIndex
{
    public int GetIndex();
}


public class VInfo : MonoBehaviour, IGetUnitIndex
{
    [Header("Settings")]
    [SerializeField] private TYPE type;
    public TYPE Type { get => type; }
    
    private int unitIndex;
    private VCrasher crasher;

    public delegate void FreeUnit(int selfIndex, int parkingIndex, VInfo info);
    public event FreeUnit OnFreeUnit;

    public delegate void StartCrashWithUnit(int selfIndex, int otherIndex, Vector3 contactPoint);
    public event StartCrashWithUnit OnStartCrashWithUnit;

    public delegate void GameOver( Vector3 contactPoint);
    public event GameOver OnGameOver;

    private void Awake()
    {
        crasher = GetComponent<VCrasher>();
        crasher.OnStartCollision += StartCrash;
    }

    private void OnDestroy()
    {
        crasher.OnStartCollision -= StartCrash;
    }

    public void SetInfo(int _unitIndex)
    {
        unitIndex = _unitIndex;
    }

    void StartCrash(Vector3 contactPoint, int otherIndex)
    {
        if (OnStartCrashWithUnit != null) OnStartCrashWithUnit.Invoke(unitIndex, otherIndex, contactPoint);
    }

    public void FreeUnitIndex(int targetPointNumber)
    {
        if (OnFreeUnit != null) OnFreeUnit.Invoke(unitIndex, targetPointNumber, this);
    }

    public void GameOverState(Vector3 position)
    {
        if (OnGameOver != null) OnGameOver.Invoke(position);
        Logging.Log("Game over by unit");
    }

    public int GetIndex()
    {
        return unitIndex;
    }
}
