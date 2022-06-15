using IJ.Core.Managers.GameManagers;
using UnityEngine;

namespace IJ.Core.Menus.MainMenu
{
    [RequireComponent(typeof(PointsControl))]
    [RequireComponent(typeof(PanelsControl))]
    public class MenuMaster : MonoBehaviour, ISetPlayerState
    {
        [Header("Scripts")]
        [SerializeField] private MenuGameManager _gameManager;

        private PointsControl _pointsControl;
        private PanelsControl _panelsControl;
        private LocationWorkflow _workflow;

        private void Awake()
        {
            _pointsControl = GetComponent<PointsControl>();
            _panelsControl = GetComponent<PanelsControl>();
        }

        public void SetPlayerState(PlayerState playerState)
        {
            _workflow = new LocationWorkflow(playerState, _pointsControl);
            _panelsControl.OpenMap();
        }

        public void OpenLevelsPanelForLocation(Location location)
        {
            if (!_workflow.GetLocationAvailability(location)) return;
            _panelsControl.TransitToLevels(location);
        }

        public bool MakeLocationAvailable(Location location)
        {
            if (!_workflow.CanBeLocationAvailable(location)) return false;

            _workflow.MakeLocationAvailable(location);
            _panelsControl.TransitToOpening(location.LocationIndex);

            return true;
        }

        public bool CanBeLocationAvailable(Location location)
        {
            return _workflow.CanBeLocationAvailable(location);
        }
    }
}
