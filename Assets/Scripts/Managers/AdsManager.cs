using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : Singleton<AdsManager> {

    #if UNITY_ANDROID
    private string gameId = "1744253";
    #endif

    private string placementIdSimple = "video";
    private string placementIdRewarded = "rewardedVideo";


    void Start() {
        if (Advertisement.isSupported) {
            Advertisement.Initialize(gameId, false);
        }
    }

    public bool IsReady(bool isRewardedAd) {
        if (isRewardedAd) {
            return Advertisement.IsReady(placementIdRewarded);
        } else {
            return Advertisement.IsReady(placementIdSimple);
        }
    }

    public void ShowSimpleAd() {
        Advertisement.Show(placementIdSimple);
    }

    public void ShowRewardedAd(System.Action<ShowResult> handleVideoCallback) {
        ShowOptions options = new ShowOptions();
        options.resultCallback = handleVideoCallback;

        Advertisement.Show(placementIdRewarded, options);
    }
}