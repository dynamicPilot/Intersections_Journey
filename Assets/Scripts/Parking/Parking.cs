using UnityEngine;

public class Parking : MonoBehaviour
{
    [SerializeField] private int number;
    public int Number { get => number; }
    [SerializeField] private Transform[] points;

    public Transform[] GetPoints()
    {
        return points;
    }

    //public void TurnOnOffParking(bool mode)
    //{
    //    ga
    //}
}
