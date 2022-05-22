using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    // Path class --> contains data for all pathes on the crossroads scheme
    public enum TURN { left, right, none, leftBend, rightBend }

    [Header("Graph data")]
    [SerializeField] private int startPointNumber;
    public int StartPointNumber { get => startPointNumber; }

    [SerializeField] private int endPointNumber;
    public int EndPointNumber { get => endPointNumber; }

    [SerializeField] private float weight;
    public float Weight { get => weight; }

    [SerializeField] private List<Transform> additionalPoints;
    //public List<Transform> AdditionalPoints { get => additionalPoints; }

    [SerializeField] private List<Vector2> curvePoints;
    public List<Vector2> CurvePoints { get => curvePoints; }

    [SerializeField] private float curveLength = 0f;

    public float CurveLength { get => curveLength; }

    [SerializeField] private TURN turn = TURN.none;
    public TURN Turn { get => turn; }
    [Header("Corrections")]
    //[SerializeField] private bool needMove = false;
    [SerializeField] private Vector2 positionDelta = Vector2.zero;
    //[SerializeField] private bool isLine = true;
    //public bool IsLine { get => isLine; }

    public void FillCurvePoints(Vector2 startPointPosition, Vector2 endPointPosition)
    {
        curvePoints = new List<Vector2>();
        curvePoints.Add(startPointPosition);

        if (additionalPoints != null)
        {
            foreach (Transform addPoint in additionalPoints)
            {
                curvePoints.Add(new Vector2(addPoint.position.x + positionDelta.x, addPoint.position.y + positionDelta.y));
            }
                
        }
        
        curvePoints.Add(endPointPosition);

        curveLength = Curves.GetCurveLength(curvePoints);
    }


}
