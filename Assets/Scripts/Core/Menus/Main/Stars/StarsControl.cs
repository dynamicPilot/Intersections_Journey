using UnityEngine;

public class StarsControl : MonoBehaviour
{
    [SerializeField] private Star[] stars;

    public void SetStars(int points, int maxPoints)
    {
        int starNumber = stars.Length * points / maxPoints;
        Logging.Log("StarsControl: points " + points + " max points " + maxPoints + " star number " + starNumber);
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < starNumber)
            {
                stars[i].SetStarNewState(Star.STATE.full);
                Logging.Log("Star state is full");
            }
            else stars[i].SetStarNewState(Star.STATE.empty);
        }
    }
}
