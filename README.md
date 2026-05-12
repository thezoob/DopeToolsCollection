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

## Required Unity packages

This package targets Unity 6 and declares these Package Manager dependencies:

- **Visual Scripting** (`com.unity.visualscripting`) 1.9.5+
- **Mobile Notifications** (`com.unity.mobile.notifications`) 2.4.0+ for DopeNotifications
- **Advertisement Legacy** (`com.unity.ads`) 4.x for DopeAds

If you install from Git, Unity should resolve the declared versions. If your project already uses newer compatible versions, keep the newer versions.

## Module/dependency notes

DopeTween and the pure macro folders are isolated from optional package integrations with assembly definitions. When embedded locally, DopeAds and DopeNotifications C# code is guarded so missing optional SDKs should not break DopeTween.

- DopeAds C# units compile only when Unity's **Advertisement Legacy** package (`com.unity.ads` 4.x) is installed.
- DopeNotifications C# units compile only when Unity's **Mobile Notifications** package (`com.unity.mobile.notifications` 2.4+) is installed.
- Bolt / Unity Visual Scripting is still required for the custom units.

> Remove folders for tools you do not need only when using an embedded/local copy of the package. Direct Git installs keep all modules present, and their declared package dependencies are required for compilation.

## Documentation and demos

- Unity 6 setup: [`Documentation~/unity-6-setup.md`](Documentation~/unity-6-setup.md)
- Demo import guidance: [`Samples~/README.md`](Samples~/README.md)

The existing `_DopeTools Demos` folder is intentionally kept at its original path to avoid GUID and graph-reference churn. Copy/import those demos manually when you need them.

## DopeTween

Tweening units based on animate.js and Tweening SuperUnits to add juice to your game.

![DopeTween](https://i.imgur.com/1gmIuYv.png)
![DopeTween Types](https://i.imgur.com/zKdjd0E.png)
![DopeTween Inspector](https://i.imgur.com/bAGMwOf.png)

## Dope Notifications

Unity 6 update: requires **Mobile Notifications** (`com.unity.mobile.notifications`) 2.4+.

Cross-platform Visual Scripting units for initializing, requesting permission, and scheduling notifications on Android/iOS.

![Dope Notifications Android](https://i.imgur.com/HF1bZBp.png)

## DopeAds Unity

Unity 6 update: requires **Advertisement Legacy** (`com.unity.ads`) 4.x.

Adds Unity 6-compatible Initialize / Load / Show / Banner units using the modern listener-based Ads API.

![DopeAds](https://i.imgur.com/o1Z4x47.png)

## DopeTouch

A set of SuperUnits to make touch interaction easier.

![DopeTouch](https://i.imgur.com/FvzREVo.png)

## Super Units that don't belong

![Super Units](https://i.imgur.com/Sj23mGG.png)
