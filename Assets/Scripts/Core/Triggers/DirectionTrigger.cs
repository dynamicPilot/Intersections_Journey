using UnityEngine;

namespace IJ.Core.Triggers
{
    /// <summary>
    /// Trigger to change units direction on the road ans start direction control.
    /// </summary>
    public class DirectionTrigger : MonoBehaviour
    {
        [SerializeField] int triggerStartPointNumber = -1;

        [Header("Direction")]
        [SerializeField] private bool onEnter = true;
        [SerializeField] private bool needSetDirection = false;
        [SerializeField] private bool needCarToFollowDirectionControl = false;
        [SerializeField] private VehicleScanner.DIRECTION directionToSet = VehicleScanner.DIRECTION.none;
        [SerializeField] private DIRECTION _directionToSet = DIRECTION.none;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!onEnter) return;

            if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
            {
                //Logging.Log("BorderTrigger: car start turn!");
                if (needSetDirection)
                {
                    collision.gameObject.GetComponent<VScanner>().SetDirection(_directionToSet, triggerStartPointNumber, needCarToFollowDirectionControl);
                    //Logging.Log("DirrectionTrigger: set direction to car " + collision.gameObject.GetComponent<VehicleScanner>().Direction);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (onEnter) return;

            if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
            {
                //Logging.Log("BorderTrigger: car start turn!");
                if (needSetDirection)
                {
                    collision.gameObject.GetComponent<VScanner>().SetDirection(_directionToSet, triggerStartPointNumber, needCarToFollowDirectionControl);
                    //collision.gameObject.GetComponent<VehicleScanner>().ChangeDirection(directionToSet, triggerStartPointNumber, needCarToFollowDirectionControl);
                    //Logging.Log("DirrectionTrigger: set direction to car " + collision.gameObject.GetComponent<VehicleScanner>().Direction);
                }
            }
        }
    }
}
