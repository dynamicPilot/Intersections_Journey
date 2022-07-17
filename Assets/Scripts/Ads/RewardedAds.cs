using UnityEngine.Advertisements;
using UnityEngine;
using IJ.UIElements;

namespace IJ.Ads
{
    [RequireComponent(typeof(AdsFlow))]
    public class RewardedAds : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        [SerializeField] DisableButtonWithIcon _showAdButton;
        [SerializeField] private string _androidAdUnitId = "RewardedStar_Android";

        AdsFlow _flow;
        string _adUnitId = null;

        void Awake()
        {
            _flow = GetComponent<AdsFlow>();
            _adUnitId = _androidAdUnitId;
//#if UNITY_IOS
//        _adUnitId = _iOSAdUnitId;
//#elif UNITY_ANDROID
//        _adUnitId = _androidAdUnitId;
//#endif

            _showAdButton.Interactive(false);
        }

        public void LoadAd()
        {
            Advertisement.Load(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (adUnitId.Equals(_adUnitId))
            {
                _showAdButton.Interactive(true);
            }
        }

        public void ShowAd()
        {
            Logging.Log("--- show Ads ---");
            _showAdButton.Interactive(false);
            _flow.StartShow();
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Logging.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            _showAdButton.gameObject.SetActive(false);
            //_flow.Failed();
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Logging.Log("Unity Ads Rewarded Ad Completed");

                _flow.EndShow(true);

                Advertisement.Load(_adUnitId, this);
            }
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Logging.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            _showAdButton.gameObject.SetActive(false);
            _flow.EndShow(false);
            _flow.Failed();
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        //void OnDestroy()
        //{
        //    _showAdButton.onClick.RemoveAllListeners();
        //}
    }
}
