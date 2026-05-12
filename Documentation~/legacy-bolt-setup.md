# Legacy Bolt 1.4 Setup

DopeTools defaults to Unity 6 / Unity Visual Scripting, but the C# unit sources can also compile against Asset Store Bolt 1.4.x (`Bolt` / `Ludiq`) while still using Unity 6-era Ads and Mobile Notifications packages.

## Requirements

- Unity 6, or a Unity version where your Bolt 1.4.x production setup is already working.
- Bolt 1.4.x assemblies present in the project.
- Optional packages only for optional modules:
  - Mobile Notifications (`com.unity.mobile.notifications`) 2.4+ for DopeNotifications, including unified notification units.
  - Advertisement Legacy (`com.unity.ads`) 4.x for DopeAds.

## Setup

1. Copy DopeTools locally into `Assets/DopeToolsCollection` or another non-UPM asset layout. Do not use the Git URL UPM package profile for legacy Bolt because the checked-in `package.json` targets Unity 6 / Unity Visual Scripting.
2. Copy the files from `Documentation~/legacy-bolt-asmdefs/` over the matching files in the copied package.
3. Add this scripting define symbol:

   ```text
   DOPETOOLS_BOLT_LEGACY
   ```

4. If you use Ads or Notifications, install the matching Unity package and keep the existing package guard symbols/version defines.
5. Rebuild Bolt unit options so the fuzzy finder refreshes.

## Notes

- DopeTween behavior and serialized unit class names are kept stable.
- The default asmdefs reference `Unity.VisualScripting.*`; the legacy replacements reference `Bolt.*` and `Ludiq.*` assemblies.
- Only one asmdef profile should be active at once.
- Unity 6 unified notification units and the modern Ads units are available in legacy Bolt mode when the matching Unity packages are installed.
- If your Unity version is too old for asmdef `versionDefines`, define optional module symbols manually when needed:
  - `DOPETOOLS_HAS_UNITY_ADS`
  - `DOPETOOLS_HAS_MOBILE_NOTIFICATIONS`
