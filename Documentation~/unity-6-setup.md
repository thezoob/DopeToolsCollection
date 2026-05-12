# Unity 6 Setup

DopeTools Collection is packaged for Unity Package Manager while keeping the original asset folders in place for compatibility with existing Visual Scripting graphs and Unity `.meta` GUIDs.

## Requirements

- Unity 6 (`6000.0`) or newer.
- Unity Visual Scripting (`com.unity.visualscripting`) 1.9.5 or newer.
- Mobile Notifications (`com.unity.mobile.notifications`) 2.4.0 or newer for DopeNotifications.
- Advertisement Legacy (`com.unity.ads`) 4.x for DopeAds.

The package manifest declares these dependencies so Unity can resolve them when the package is installed from a Git URL. If your project already has newer compatible versions, keep the newer versions.

## Install from Git URL

1. Open **Window > Package Manager**.
2. Select **+ > Add package from git URL...**.
3. Enter:

   ```text
   https://github.com/thezoob/DopeToolsCollection.git
   ```

4. Let Unity import and compile the package.
5. Run **Tools > Bolt > Build Unit Options...** (or the equivalent Unity Visual Scripting unit regeneration command) so the DopeTools units appear in the Fuzzy Finder.

## Using the units

- DopeTween units appear under `DopeTween` / DopeTools categories depending on the unit.
- DopeNotifications Unity 6 units are under `DopeTools/Notifications`.
- DopeAds Unity 6 units are under `DopeTools/Ads`.
- SuperUnit assets remain in their original folders such as `DopeTouch`, `DopeUI`, `SuperUnits_General`, and each feature folder.

## Samples and demos

The existing `_DopeTools Demos` folder is intentionally kept at its original path in this repository. It is not moved into `Samples~` because moving it would change asset paths and increase the chance of broken graph references.

For demo guidance, see `Samples~/README.md` in this package.

## Updating an existing project

1. Back up the project or commit your current work.
2. Install/update this package.
3. Verify the required Unity packages are installed.
4. Run **Build Unit Options** for Visual Scripting.
5. Open existing graphs and resolve any project-specific missing references.

## Optional modules

If you do not use Ads or Notifications, you can remove the corresponding folders from an embedded copy of the package. When installed directly from Git, those modules remain present and their Unity Registry package dependencies are required for compilation.
