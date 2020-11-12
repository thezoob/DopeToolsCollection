using UnityEngine;
using UnityEngine.Advertisements;
using Bolt;

public class Ads : MonoBehaviour, IUnityAdsListener
{
    private string rewardedVideoAd = "rewardedVideo" ;

    private GameObject adManager;

    private void OnEnable()
    {
        adManager = this.gameObject;
    }
    private void Start()
    {
        Advertisement.AddListener(this);
    }

    public void OnUnityAdsDidError(string message)
    {
        CustomEvent.Trigger(adManager, "onUnityAdsDidError", message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Finished:
                if (placementId == rewardedVideoAd)
                { CustomEvent.Trigger(adManager, "OnUnityAdsDidFinish", null); }
                else  { CustomEvent.Trigger(adManager, "onUnityAdsInterstitialFinish", null); }
                break;
            case ShowResult.Skipped:
                CustomEvent.Trigger(adManager, "OnUnityAdsDidSkip", null);
                break;
        }
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        CustomEvent.Trigger(adManager, "OnUnityAdsDidStart", null);
    }

    public void OnUnityAdsReady(string placementId)
    {
        CustomEvent.Trigger(adManager, "OnUnityAdsReady", null);
    }


}
