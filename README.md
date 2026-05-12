# DopeToolsCollection

Useful Super Units and custom units for **Unity Visual Scripting** (formerly **Bolt**).

## Unity Package Manager install

1. Open **Window > Package Manager** in Unity 6.
2. Select **+ > Add package from git URL...**.
3. Enter:

   ```text
   https://github.com/thezoob/DopeToolsCollection.git
   ```

4. Let Unity resolve dependencies and compile.
5. Run **Tools > Bolt > Build Unit Options...** so DopeTools units appear in the Visual Scripting Fuzzy Finder.

## Unity packages

This package targets Unity 6 and declares **Visual Scripting** (`com.unity.visualscripting`) 1.9.5+ as a Package Manager dependency.

Optional integrations require extra packages only if you use those modules:

- **Mobile Notifications** (`com.unity.mobile.notifications`) 2.4.0+ for DopeNotifications
- **Advertisement Legacy** (`com.unity.ads`) 4.x for DopeAds

Install optional packages from Package Manager before using those units.

## Legacy Bolt 1.4 support

The checked-in package profile targets Unity 6 / Unity Visual Scripting. The C# units also support legacy Asset Store Bolt via conditional imports, including the Unity 6 Advertisement Legacy and Mobile Notifications integrations.

For Bolt 1.4.x projects:

1. Copy this package into `Assets/DopeToolsCollection` or another non-UPM asset layout instead of using the Git URL profile.
2. Copy the replacement asmdefs from [`Documentation~/legacy-bolt-asmdefs`](Documentation~/legacy-bolt-asmdefs) over the matching package files.
3. Add `DOPETOOLS_BOLT_LEGACY` to **Project Settings > Player > Scripting Define Symbols**.
4. Install `com.unity.ads` 4.x and/or `com.unity.mobile.notifications` 2.4+ if you use Ads or Notifications.
5. Run Bolt's unit option rebuild command.

Only use one asmdef profile at a time: default Unity Visual Scripting asmdefs, or the legacy Bolt replacements.

## Module/dependency notes

DopeTween and the pure macro folders are isolated from optional package integrations with assembly definitions. When embedded locally, DopeAds and DopeNotifications C# code is guarded so missing optional SDKs should not break DopeTween.

- DopeAds C# units compile only when Unity's **Advertisement Legacy** package (`com.unity.ads` 4.x) is installed.
- DopeNotifications C# units compile only when Unity's **Mobile Notifications** package (`com.unity.mobile.notifications` 2.4+) is installed.
- Bolt / Unity Visual Scripting is still required for the custom units.

> Remove folders for tools you do not need only when using an embedded/local copy of the package. Direct Git installs keep all modules present; optional Ads/Notifications assemblies are guarded and compile only when their packages are installed.

## Documentation and demos

- Unity 6 setup: [`Documentation~/unity-6-setup.md`](Documentation~/unity-6-setup.md)
- Legacy Bolt setup: [`Documentation~/legacy-bolt-setup.md`](Documentation~/legacy-bolt-setup.md)
- Demo import guidance: [`Samples~/README.md`](Samples~/README.md)

The existing `_DopeTools Demos` folder is intentionally kept at its original path to avoid GUID and graph-reference churn. Copy/import those demos manually when you need them.

## DopeTween

Tweening units based on animate.js and Tweening SuperUnits to add juice to your game.

![DopeTween](https://i.imgur.com/1gmIuYv.png)
![DopeTween Types](https://i.imgur.com/zKdjd0E.png)
![DopeTween Inspector](https://i.imgur.com/bAGMwOf.png)

## Dope Notifications

Unity 6 update: requires **Mobile Notifications** (`com.unity.mobile.notifications`) 2.4+.

Cross-platform Visual Scripting units for initializing, requesting permission, scheduling notifications, and receiving notification payload events on Android/iOS.

![Dope Notifications Android](https://i.imgur.com/HF1bZBp.png)

## DopeAds Unity

Unity 6 update: requires **Advertisement Legacy** (`com.unity.ads`) 4.x.

Adds Unity 6-compatible Initialize / Load / Show / Banner units plus callback event units using the modern listener-based Ads API.

![DopeAds](https://i.imgur.com/o1Z4x47.png)

## DopeTouch

A set of SuperUnits to make touch interaction easier.

![DopeTouch](https://i.imgur.com/FvzREVo.png)

## Super Units that don't belong

![Super Units](https://i.imgur.com/Sj23mGG.png)
