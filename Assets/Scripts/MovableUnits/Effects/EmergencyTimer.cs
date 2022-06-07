using UnityEngine;

public class EmergencyTimer
{
    private RoadMark mark;
    private RectTransform markTransform;
    float timer = 0f;
    float timerValue;
    float allertTimerValue;
    bool isAllertOn = false;

    public delegate void TimerIsOver();
    public event TimerIsOver OnTimerIsOver;

    public EmergencyTimer(RoadMark _mark, RectTransform _markTransform, float _timerValue, float _allertTimerValue)
    {
        mark = _mark;
        markTransform = _markTransform;
        timerValue = _timerValue;
        allertTimerValue = _allertTimerValue;

        isAllertOn = false;
        timer = 0f;
        DestroyTimer();
    }

    void CorrectMarkRotationToStayVertical(Transform transform)
    {
        markTransform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -transform.rotation.z);
        //markRectTransform.position = transform.position;
    }

    public void UpdateTimer(float deltaTime)
    {
        timer += deltaTime;
        

        if (timer >= timerValue)
        {
            if (!isAllertOn)
            {
                // start allert
                mark.MoveToAllert();
                isAllertOn = true;
            }
            else if (timer >= timerValue + allertTimerValue)
            {
                // game over
                if (OnTimerIsOver != null) OnTimerIsOver.Invoke();
            }
            else
            {
                mark.UpdateIndicatorValue((timer - timerValue) / allertTimerValue, true);
            }
        }
        else
        {
            mark.UpdateIndicatorValue(timer / timerValue, false);
        }
    }

    public void OnStop(Transform transform)
    {
        if (mark.CheckForStartIndicator())
        {
            mark.gameObject.SetActive(true);
            CorrectMarkRotationToStayVertical(transform);
            mark.StartIndicator();
        }
    }

    public void OnStart()
    {
        if (mark.gameObject.activeSelf)
        {
            mark.StopIndicator();
        }
    }

    //public void HideMarkAndTimer()
    //{
    //    OnStart();
    //    timer = 0f;
    //}

    public void DestroyTimer()
    {
        //OnVehicleStart();
        mark.gameObject.SetActive(false);
        timer = 0f;
    }
}
