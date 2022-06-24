using UnityEngine;


namespace RepairSites
{
    public class RepairSite : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float targetVelocity = 1f;
        [SerializeField] private Vector2 targetPoint;
        [SerializeField] private float minDistanceToAffectVehicle = 1f;
        [SerializeField] private bool isDouble = false;

        public bool IsDouble { get => isDouble; }

        [Header("For Repair Car and Control")]
        [SerializeField] private TimePoint location;
        public TimePoint Location { get => location; }

        [Header("Components")]
        [SerializeField] private BoxCollider2D[] boxColliderComponents;

        int repairSiteIndex = 0;
        public int RepairSiteIndex { get => repairSiteIndex; set => repairSiteIndex = value; }

        private void Awake()
        {
            foreach (BoxCollider2D boxColliderComponent in boxColliderComponents)
                boxColliderComponent.enabled = false;
        }

        public void ActivateSite()
        {
            foreach (BoxCollider2D boxColliderComponent in boxColliderComponents)
                boxColliderComponent.enabled = true;
        }

        public void StopSite()
        {
            foreach (BoxCollider2D boxColliderComponent in boxColliderComponents)
                boxColliderComponent.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // force vehicle unit to reduce speed or stop if it is a repair car
            if (collision.gameObject.CompareTag("Car"))
            {
                if (Vector2.Distance(collision.gameObject.transform.position, targetPoint) > minDistanceToAffectVehicle)
                    collision.gameObject.GetComponent<VRepairSiteTag>().EnterRepairSite(targetPoint, targetVelocity, repairSiteIndex);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Car"))
            {
                collision.gameObject.GetComponent<VRepairSiteTag>().ExitRepairSite();
            }
        }

        public bool IsTheSameLocation(int startPoint, int endPoint)
        {
            return (location.RoadStartPointNumber == startPoint && location.RoadEndPointNumber == endPoint);
        }
    }
}

