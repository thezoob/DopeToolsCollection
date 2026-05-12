#if DOPETOOLS_HAS_UNITY_ADS
#if DOPETOOLS_BOLT_LEGACY
using Bolt;
using Ludiq;
#else
using Unity.VisualScripting;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Dopetools.DopeAds
{
    /// <summary>
    /// Unity 6 / Advertisement Legacy 4.x compatible Ads bridge for Bolt.
    ///
    /// Unity removed the old IUnityAdsListener / Advertisement.IsReady flow. The modern
    /// package uses explicit Initialize, Load and Show listeners, so this component keeps
    /// a small loaded-ad cache and forwards SDK callbacks as Bolt Custom Events.
    /// </summary>
    public sealed class DopeAdsCallbackEventArgs : EventArgs
    {
        public DopeAdsCallbackEventArgs(string eventName, string argument)
        {
            EventName = eventName;
            Argument = argument;
        }

        public string EventName { get; private set; }
        public string Argument { get; private set; }
    }

    public class DopeAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public const string EventInitializationComplete = "OnUnityAdsInitializationComplete";
        public const string EventInitializationFailed = "OnUnityAdsInitializationFailed";
        public const string EventAdLoaded = "OnUnityAdsReady";
        public const string EventLoadFailed = "OnUnityAdsLoadFailed";
        public const string EventShowStart = "OnUnityAdsDidStart";
        public const string EventShowClick = "OnUnityAdsShowClick";
        public const string EventRewardedComplete = "OnUnityAdsDidFinish";
        public const string EventInterstitialComplete = "onUnityAdsInterstitialFinish";
        public const string EventShowSkipped = "OnUnityAdsDidSkip";
        public const string EventError = "onUnityAdsDidError";
        public const string EventShowFailed = "OnUnityAdsShowFailed";
        public const string EventBannerLoaded = "OnUnityBannerLoaded";
        public const string EventBannerLoadFailed = "OnUnityBannerLoadFailed";
        public const string EventBannerShown = "OnUnityBannerShown";
        public const string EventBannerClicked = "OnUnityBannerClicked";
        public const string EventBannerHidden = "OnUnityBannerHidden";

        [Header("Game IDs")]
        [SerializeField] private string androidGameId;
        [SerializeField] private string iosGameId;
        [SerializeField] private bool testMode = true;
        [SerializeField] private bool initializeOnStart;

        [Header("Default Ad Unit IDs")]
        [SerializeField] private string androidInterstitialAdUnitId = "Interstitial_Android";
        [SerializeField] private string iosInterstitialAdUnitId = "Interstitial_iOS";
        [SerializeField] private string androidRewardedAdUnitId = "Rewarded_Android";
        [SerializeField] private string iosRewardedAdUnitId = "Rewarded_iOS";
        [SerializeField] private string androidBannerAdUnitId = "Banner_Android";
        [SerializeField] private string iosBannerAdUnitId = "Banner_iOS";

        private static readonly HashSet<string> loadedAdUnits = new HashSet<string>();
        private static readonly HashSet<string> pendingShowAdUnits = new HashSet<string>();
        public static event Action<DopeAdsCallbackEventArgs> CallbackReceived;

        private static DopeAds instance;

        public static DopeAds Instance
        {
            get
            {
                if (instance == null)
                {
#if UNITY_2022_1_OR_NEWER
                    instance = FindAnyObjectByType<DopeAds>();
#else
                    instance = FindObjectOfType<DopeAds>();
#endif
                }

                return instance;
            }
        }

        public static DopeAds GetOrCreate()
        {
            if (Instance != null)
            {
                return Instance;
            }

            var go = new GameObject("Dope Ads");
            instance = go.AddComponent<DopeAds>();
            DontDestroyOnLoad(go);
            return instance;
        }

        public static bool IsInitialized => Advertisement.isInitialized;
        public static bool IsSupported => Advertisement.isSupported;

        public static bool IsAdLoaded(string adUnitId)
        {
            return !string.IsNullOrEmpty(adUnitId) && loadedAdUnits.Contains(adUnitId);
        }

        private GameObject eventTarget => gameObject;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            if (initializeOnStart)
            {
                InitializeConfiguredAds();
            }
        }

        public void InitializeConfiguredAds()
        {
            Initialize(androidGameId, iosGameId, testMode);
        }

        public void Initialize(string androidId, string iOSId, bool useTestMode)
        {
            androidGameId = androidId;
            iosGameId = iOSId;
            testMode = useTestMode;

            var gameId = ResolvePlatformId(androidGameId, iosGameId);

            if (string.IsNullOrEmpty(gameId))
            {
                Trigger(EventInitializationFailed, "Missing Unity Ads Game ID for this platform.");
                return;
            }

            if (!Advertisement.isSupported)
            {
                Trigger(EventInitializationFailed, "Unity Ads is not supported on this platform.");
                return;
            }

            if (Advertisement.isInitialized)
            {
                Trigger(EventInitializationComplete, null);
                return;
            }

            Advertisement.Initialize(gameId, testMode, this);
        }

        public void LoadInterstitial()
        {
            LoadAd(ResolvePlatformId(androidInterstitialAdUnitId, iosInterstitialAdUnitId));
        }

        public void ShowInterstitial()
        {
            ShowAd(ResolvePlatformId(androidInterstitialAdUnitId, iosInterstitialAdUnitId));
        }

        public void LoadRewarded()
        {
            LoadAd(ResolvePlatformId(androidRewardedAdUnitId, iosRewardedAdUnitId));
        }

        public void ShowRewarded()
        {
            ShowAd(ResolvePlatformId(androidRewardedAdUnitId, iosRewardedAdUnitId));
        }

        public void LoadAd(string adUnitId)
        {
            if (!EnsureInitialized(EventLoadFailed))
            {
                return;
            }

            if (string.IsNullOrEmpty(adUnitId))
            {
                Trigger(EventLoadFailed, "Missing Ad Unit ID.");
                return;
            }

            Advertisement.Load(adUnitId, this);
        }

        public void ShowAd(string adUnitId)
        {
            if (!EnsureInitialized(EventShowFailed))
            {
                return;
            }

            if (string.IsNullOrEmpty(adUnitId))
            {
                Trigger(EventShowFailed, "Missing Ad Unit ID.");
                return;
            }

            if (!IsAdLoaded(adUnitId))
            {
                // Modern Unity Ads requires Load before Show. Queue this show request and
                // continue automatically from OnUnityAdsAdLoaded.
                pendingShowAdUnits.Add(adUnitId);
                LoadAd(adUnitId);
                return;
            }

            pendingShowAdUnits.Remove(adUnitId);
            loadedAdUnits.Remove(adUnitId);
            Advertisement.Show(adUnitId, this);
        }

        public void LoadBanner(BannerPosition position)
        {
            LoadBanner(ResolvePlatformId(androidBannerAdUnitId, iosBannerAdUnitId), position);
        }

        public void ShowBanner(BannerPosition position)
        {
            ShowBanner(ResolvePlatformId(androidBannerAdUnitId, iosBannerAdUnitId), position);
        }

        public void LoadBanner(string adUnitId, BannerPosition position)
        {
            if (!EnsureInitialized(EventBannerLoadFailed))
            {
                return;
            }

            if (string.IsNullOrEmpty(adUnitId))
            {
                Trigger(EventBannerLoadFailed, "Missing Banner Ad Unit ID.");
                return;
            }

            Advertisement.Banner.SetPosition(position);
            Advertisement.Banner.Load(adUnitId, new BannerLoadOptions
            {
                loadCallback = () =>
                {
                    loadedAdUnits.Add(adUnitId);
                    Trigger(EventBannerLoaded, adUnitId);
                },
                errorCallback = message => Trigger(EventBannerLoadFailed, message)
            });
        }

        public void ShowBanner(string adUnitId, BannerPosition position)
        {
            if (!EnsureInitialized(EventShowFailed))
            {
                return;
            }

            if (string.IsNullOrEmpty(adUnitId))
            {
                Trigger(EventShowFailed, "Missing Banner Ad Unit ID.");
                return;
            }

            Advertisement.Banner.SetPosition(position);

            if (!IsAdLoaded(adUnitId))
            {
                Advertisement.Banner.Load(adUnitId, new BannerLoadOptions
                {
                    loadCallback = () =>
                    {
                        loadedAdUnits.Add(adUnitId);
                        Trigger(EventBannerLoaded, adUnitId);
                        ShowLoadedBanner(adUnitId);
                    },
                    errorCallback = message => Trigger(EventBannerLoadFailed, message)
                });
                return;
            }

            ShowLoadedBanner(adUnitId);
        }

        private void ShowLoadedBanner(string adUnitId)
        {
            Advertisement.Banner.Show(adUnitId, new BannerOptions
            {
                showCallback = () => Trigger(EventBannerShown, adUnitId),
                clickCallback = () => Trigger(EventBannerClicked, adUnitId),
                hideCallback = () => Trigger(EventBannerHidden, adUnitId)
            });
        }

        public void HideBanner(bool destroy = false)
        {
            Advertisement.Banner.Hide(destroy);
            Trigger(EventBannerHidden, null);
        }

        public static string ResolvePlatformId(string androidId, string iOSId)
        {
#if UNITY_IOS
            return iOSId;
#elif UNITY_ANDROID
            return androidId;
#elif UNITY_EDITOR
            return !string.IsNullOrEmpty(androidId) ? androidId : iOSId;
#else
            return string.Empty;
#endif
        }

        private bool EnsureInitialized(string failureEvent)
        {
            if (Advertisement.isInitialized)
            {
                return true;
            }

            Trigger(failureEvent, "Unity Ads is not initialized. Call Dope Ads Initialize first.");
            return false;
        }

        private void Trigger(string eventName, object argument)
        {
            if (argument == null)
            {
                CustomEvent.Trigger(eventTarget, eventName);
            }
            else
            {
                CustomEvent.Trigger(eventTarget, eventName, argument);
            }

            var callbackReceived = CallbackReceived;
            if (callbackReceived != null)
            {
                callbackReceived(new DopeAdsCallbackEventArgs(eventName, argument == null ? string.Empty : argument.ToString()));
            }
        }

        public void OnInitializationComplete()
        {
            Trigger(EventInitializationComplete, null);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Trigger(EventInitializationFailed, $"{error}: {message}");
            Trigger(EventError, message);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            loadedAdUnits.Add(adUnitId);
            Trigger(EventAdLoaded, adUnitId);

            if (pendingShowAdUnits.Contains(adUnitId))
            {
                ShowAd(adUnitId);
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            loadedAdUnits.Remove(adUnitId);
            pendingShowAdUnits.Remove(adUnitId);
            Trigger(EventLoadFailed, $"{adUnitId}: {error}: {message}");
            Trigger(EventError, message);
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Trigger(EventShowFailed, $"{adUnitId}: {error}: {message}");
            Trigger(EventError, message);
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            Trigger(EventShowStart, adUnitId);
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
            Trigger(EventShowClick, adUnitId);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    if (IsRewardedAdUnit(adUnitId))
                    {
                        Trigger(EventRewardedComplete, adUnitId);
                    }
                    else
                    {
                        Trigger(EventInterstitialComplete, adUnitId);
                    }
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    Trigger(EventShowSkipped, adUnitId);
                    break;
                default:
                    Trigger(EventShowFailed, $"{adUnitId}: show completed with state {showCompletionState}");
                    break;
            }
        }

        private bool IsRewardedAdUnit(string adUnitId)
        {
            return !string.IsNullOrEmpty(adUnitId) &&
                   (adUnitId == androidRewardedAdUnitId ||
                    adUnitId == iosRewardedAdUnitId ||
                    adUnitId.ToLowerInvariant().Contains("reward"));
        }
    }
}
#endif
