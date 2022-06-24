namespace IJ.Core.Menus.MainMenu
{
    public class LocationWorkflow
    {
        private PlayerState _playerState;
        private PointsControl _pointsControl;


        public LocationWorkflow(PlayerState playerState, PointsControl pointsControl)
        {
            _playerState = playerState;
            _pointsControl = pointsControl;
        }

        public bool GetLocationAvailability(Location location)
        {
            return _playerState.LocationsProgress[location.LocationIndex].IsAvailable;
        }

        public void MakeLocationAvailable(Location location)
        {
            // make available
            _playerState.MakeLocationAvailableOrNot(location.LocationIndex, true);

            // spend points
            _pointsControl.ChangePointsAmount(-location.PointsToMakeAvailable, true);
        }

        public bool CanBeLocationAvailable(Location location)
        {
            if (!_pointsControl.HaveSomePoints(location.PointsToMakeAvailable)) return false;

            if (location.LocationsToMakeAvailable == null)
            {
                return true;
            }
            // check locations to be available
            foreach (Location loc in location.LocationsToMakeAvailable)
            {
                if (!_playerState.IsLocationAvailable(loc.LocationIndex)) return false;
            }

            return true;
        }
    }
}

