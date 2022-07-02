using IJ.Core.Menus.MainMenu;
using IJ.Core.Menus.Others;
using UnityEngine;

namespace IJ.Ads
{
    public class AdsFlow : MonoBehaviour
    {
        [SerializeField] private SnapshotTransition _snapshotTransition;
        [SerializeField] private PointsControl _pointsControl;
        [SerializeField] private ErrorPageControl _error;
        public void StartShow()
        {
            _snapshotTransition.ToAds();
        }

        public void EndShow(bool state)
        {
            _snapshotTransition.ToMainMenu();

            if (state)
            {
                Logging.Log("Get 1 point!");
                _pointsControl.ChangePointsAmount(1, true);
            }
        }

        public void Failed()
        {
            _error.gameObject.SetActive(true);
            _error.OpenPage();
        }
    }
}
