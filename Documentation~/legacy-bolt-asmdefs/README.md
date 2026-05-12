# Legacy Bolt 1.4 asmdef replacements

DopeTools ships with Unity 6 / Unity Visual Scripting assembly definitions by default. These replacements are for Unity 6 projects that still use production Bolt 1.4 graphs and want the same modern Ads / Mobile Notifications integrations.

For a project that still uses Asset Store Bolt 1.4.x (`Bolt` / `Ludiq` namespaces), copy this repo into `Assets/DopeToolsCollection` or use another non-UPM embedded asset layout, then copy these replacement `.asmdef` files over the matching files in the package. Do not use the default Git URL UPM profile for legacy Bolt because `package.json` targets Unity 6 / Unity Visual Scripting.

Also add this scripting define symbol in **Project Settings > Player > Scripting Define Symbols**:

```text
DOPETOOLS_BOLT_LEGACY
```

The replacement asmdefs reference the Bolt 1.4.15 assembly names from the local source:

- `Bolt.Core.Runtime`
- `Bolt.Flow.Runtime`
- `Ludiq.Core.Runtime`
- `Ludiq.Graphs.Runtime`
- plus the corresponding editor assemblies for descriptors/options/widgets.

Only use one profile at a time:

- Default checked-in asmdefs = Unity Visual Scripting.
- Files in this folder = legacy Bolt.

Legacy Bolt mode still compiles the Unity 6 unified Mobile Notifications units when `com.unity.mobile.notifications` 2.4+ is installed. The original Android/iOS notification units remain available too.

After switching, run Bolt's unit option rebuild command so the fuzzy finder refreshes.
