# Samples and Existing Demos

DopeTools Collection keeps the existing demo content in `_DopeTools Demos` at the repository root to preserve Unity `.meta` files, GUIDs, and Visual Scripting graph references.

This `Samples~` folder intentionally contains guidance only; the demos were not moved here.

## Importing demos manually

When the package is installed from Git, copy the demo folder you want from the package cache or an embedded checkout into your project's `Assets` folder:

- `_DopeTools Demos/DopeAds Demo`
- `_DopeTools Demos/DopeNotifications Demo`
- `_DopeTools Demos/DopeTouch Demo`
- `_DopeTools Demos/DopeTween Demo`
- `_DopeTools Demos/DopeUI Demo`

Recommended workflow:

1. Install DopeTools Collection through Package Manager.
2. Install/verify the required Unity packages listed in `Documentation~/unity-6-setup.md`.
3. Copy the desired demo folder into `Assets/_DopeTools Demos/` in your project.
4. Let Unity import the copied assets.
5. Run **Tools > Bolt > Build Unit Options...** so Visual Scripting refreshes the available units.
6. Open the copied demo scene or graph.

If you embed this package in `Packages/net.thezoob.dopetoolscollection`, you can copy `_DopeTools Demos` directly from that embedded package folder.
