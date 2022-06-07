using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum MODE {  start, inGame, end }

    [Header("Settings")]
    [SerializeField] private float startSize = 5f;
    [SerializeField] private float onStartEffectSizeDelta = 1f;
    [SerializeField] private float effectTotalTime = 1f;
    [SerializeField] private float endSize = 5f;
    [SerializeField] private Vector2 levelStartPoint;

    [Header("Scripts")]
    [SerializeField] private InputControl inputControl;
    [SerializeField] private LevelFlow _levelFlow;

    Camera mainCamera;
    MODE mode = MODE.start;

    Vector2 targetPoint = Vector2.zero;
    Vector2 startPoint = Vector2.zero;

    float totalEffectTimePassed = 0f;
    float effectStartSize = 0f;

    bool pauseUpdate = true;

    private void Awake()
    {
        mainCamera = Camera.main;
        pauseUpdate = true;
        mainCamera.orthographicSize = startSize + onStartEffectSizeDelta;

        mainCamera.transform.position = new Vector3(levelStartPoint.x, levelStartPoint.y, -10f);
    }

    private void Update()
    {
        if (pauseUpdate) return;

        if (mode == MODE.start)
        {
            totalEffectTimePassed += Time.deltaTime;
            MovingInStartMove();
        }
        else if (mode == MODE.end)
        {
            totalEffectTimePassed += Time.unscaledDeltaTime;
            MovingInEndMode();
        }
        
    }

    public void OnLevelStart()
    {
        mode = MODE.start;
        MoveInOrOut();
    }

    public void OnGameOverEndLevel(Vector2 movingToPoint)
    {
        //Time.timeScale = 0f;

        inputControl.EndInputControl();
        targetPoint = movingToPoint;
        startPoint = mainCamera.transform.position;
        effectStartSize = mainCamera.orthographicSize;

        mode = MODE.end;
        MoveInOrOut();
    }

    void MoveInOrOut()
    {
        //mainCamera.orthographicSize = startSize + onStartEffectSizeDelta;
        totalEffectTimePassed = 0f;
        pauseUpdate = false;
    }

    void MovingInStartMove()
    {
        if (totalEffectTimePassed / effectTotalTime >= 1f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize + onStartEffectSizeDelta, startSize, 1f);
            OnStartLevelEffectEnd();
            return;
        }
        else
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize + onStartEffectSizeDelta, startSize, totalEffectTimePassed / effectTotalTime);
        }
    }

    void MovingInEndMode()
    {
        // moving camera to game over reason object
        Vector2 position;
        if (totalEffectTimePassed / effectTotalTime >= 1f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(effectStartSize, endSize, 1f);
            position = Vector2.Lerp(startPoint, targetPoint, 1f);
            mainCamera.transform.position = new Vector3( position.x, position.y, mainCamera.transform.position.z);
            OnEndLevelEffectEnd();
            return;
        }
        else
        {
            mainCamera.orthographicSize = Mathf.Lerp(effectStartSize, endSize, totalEffectTimePassed / effectTotalTime);
            position = Vector2.Lerp(startPoint, targetPoint, totalEffectTimePassed / effectTotalTime);
            mainCamera.transform.position = new Vector3(position.x, position.y, mainCamera.transform.position.z);
            //mainCamera.transform.position = new Vector3 (Vector2.Lerp(startPoint, targetPoint, totalEffectTimePassed / effectTotalTime), ma;
        }
    }

    void OnStartLevelEffectEnd()
    {
        pauseUpdate = true;
        mode = MODE.inGame;
        inputControl.StartInputControl();
        
    }

    void OnEndLevelEffectEnd()
    {
        pauseUpdate = true;
        mode = MODE.inGame;

        // open game over menu
        _levelFlow.OpenGameOverMenu();
    }
}
