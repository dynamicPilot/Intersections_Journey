using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VEndMover : MonoBehaviour
{
    [SerializeField] private List<int> _unitsToMoveState = new List<int>();
    [SerializeField] private List<int> _unitsIndexes = new List<int>();
    [SerializeField] private List<VInfo> _unitsInfos = new List<VInfo>();

    [SerializeField] private Vector2 _inititalPosition = new Vector2(-50f, 50f);
    [SerializeField] private bool _coroutineIsRunning = false;
    private VehicleManager _vehicleManager;

    private void Awake()
    {
        _vehicleManager = GetComponent<VehicleManager>();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void AddUnitToMove(int unitIndex, VInfo unitInfo)
    {    
        if (!_unitsIndexes.Contains(unitIndex))
        {
            int index = _unitsIndexes.Count;
            _unitsIndexes.Add(unitIndex);
            _unitsInfos.Add(unitInfo);
            _unitsToMoveState.Add(1);

            if (!_coroutineIsRunning) StartCoroutine(UpdateMoveState());
        }       
    }

    private IEnumerator UpdateMoveState()
    {
        _coroutineIsRunning = true;

        if (CheckUnitsState())
        {            
            yield return null;
            StartCoroutine(UpdateMoveState());
        }
        else
        {
            _coroutineIsRunning = false;
            yield break;
        }
    }

    private bool CheckUnitsState()
    {
        List<int> removeUnits = new List<int>();
        for (int i = 0; i < _unitsToMoveState.Count; i++)
        {
            if (_unitsToMoveState[i] <= 0)
            {
                removeUnits.Add(i);
            }
            else
            {
                _unitsToMoveState[i]--;
            }
        }

        for(int i = 0; i < removeUnits.Count; i++)
        {
            MoveUnitAndSetFree(removeUnits[i]);

            _unitsToMoveState.RemoveAt(removeUnits[i]);
            _unitsInfos.RemoveAt(removeUnits[i]);
            _unitsIndexes.RemoveAt(removeUnits[i]);
        }

        return _unitsToMoveState.Count > 0;
    }

    private void MoveUnitAndSetFree(int index)
    {
        _unitsInfos[index].gameObject.transform.position = _inititalPosition;

        _vehicleManager.SetUnitAsFree(_unitsIndexes[index]);
    }

}
