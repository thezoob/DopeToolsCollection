# DopeAds Unity 6

DopeAds now targets Unity 6 with Unity's **Advertisement Legacy** package (`com.unity.ads` 4.x). Its C# units are isolated in an optional assembly and only compile when that package is installed.

> Unity Ads direct integration is now considered legacy by Unity. For long-term monetization Unity recommends LevelPlay mediation. This update keeps the original DopeAds workflow working against the current Advertisement Legacy API.

## Requirements

1. Unity 6.
2. Package Manager > Unity Registry > **Advertisement Legacy** (`com.unity.ads` 4.x) to enable the C# units. If it is missing, DopeAds scripts are skipped so other DopeTools modules still compile.
3. Bolt / Unity Visual Scripting with DopeTools imported.
4. Run **Tools > Bolt > Build Unit Options...** after importing/updating scripts.

## What changed

The old API used `IUnityAdsListener`, `Advertisement.AddListener`, and `Advertisement.IsReady`. Those are no longer the modern flow.

The Unity 6-compatible flow is now:

1. Initialize SDK.
2. Load an Ad Unit ID.
3. Wait for `OnUnityAdsReady`.
4. Show the loaded Ad Unit ID.

The new **Show Ad** unit will also queue a show request and auto-load first if needed, but explicit load/wait/show is still the clearest production flow.

## New Bolt units

Find these in the Fuzzy Finder under `DopeTools/Ads`:

- **Initialize Ads**
- **Load Ad**
- **Show Ad**
- **Is Ad Loaded**
- **Show Banner**
- **Hide Banner**

Use platform-specific Ad Unit IDs. Defaults follow Unity's current sample naming:

- `Interstitial_Android` / `Interstitial_iOS`
- `Rewarded_Android` / `Rewarded_iOS`
- `Banner_Android` / `Banner_iOS`

## Callback events

Attach `DopeAds` to an object or let the units create a `Dope Ads` object automatically. SDK callbacks are forwarded as Bolt Custom Events on that object:

- `OnUnityAdsInitializationComplete`
- `OnUnityAdsInitializationFailed`
- `OnUnityAdsReady`
- `OnUnityAdsLoadFailed`
- `OnUnityAdsDidStart`
- `OnUnityAdsShowClick`
- `OnUnityAdsDidFinish` — rewarded completed
- `onUnityAdsInterstitialFinish` — interstitial completed
- `OnUnityAdsDidSkip`
- `onUnityAdsDidError`
- `OnUnityAdsShowFailed`
- `OnUnityBannerLoaded`
- `OnUnityBannerLoadFailed`
- `OnUnityBannerShown`
- `OnUnityBannerClicked`
- `OnUnityBannerHidden`

## Legacy super units

The old `.asset` super units may still reference removed Unity Ads members such as `Advertisement.IsReady`. Prefer the new C# Bolt units above for Unity 6.
