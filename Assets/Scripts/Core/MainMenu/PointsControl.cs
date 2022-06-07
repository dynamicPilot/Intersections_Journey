using UnityEngine;
using TMPro;

public class PointsControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Animator animator;

    [Header("Scripts")]
    [SerializeField] private DataSaveAndLoad dataSaveAndLoad;

    private PlayerState playerState;

    private void OnDestroy()
    {
        if (playerState != null)
        {
            // unsubscribe from delegate
            playerState.OnPointsNumberChanged -= UpdatePoints;
        }
    }

    public void SetPointsControl(PlayerState newPlayerState)
    {
        playerState = newPlayerState;
        UpdatePoints(playerState.TotalPointsNumber);

        // subscribe to delegate
        playerState.OnPointsNumberChanged += UpdatePoints;    
    }

    void UpdatePoints(int points)
    {
        pointsText.SetText(points.ToString());

        animator.SetTrigger("OnChange");
    }

    public bool HaveSomePoints(int amount)
    {
        return playerState.TotalPointsNumber >= amount;
    }

    public void ChangePointsAmount(int amount, bool needSave = true)
    {
        bool haveChanged = playerState.AddPointsToTotalNumer(amount);

        if (needSave & haveChanged)
        {
            dataSaveAndLoad.SaveData(playerState);
        }
    }
}
