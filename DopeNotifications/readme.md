# DopeNotifications Unity 6

DopeNotifications now includes Unity 6-compatible unified units using Unity's **Mobile Notifications** package (`com.unity.mobile.notifications` 2.4+). Its C# units are isolated in optional runtime/editor assemblies and only compile when that package is installed.

## Requirements

1. Unity 6.
2. Package Manager > Unity Registry > **Mobile Notifications** (`com.unity.mobile.notifications` 2.4+) to enable the C# units. If it is missing, DopeNotifications scripts are skipped so other DopeTools modules still compile.
3. Bolt / Unity Visual Scripting with DopeTools imported.
4. Run **Tools > Bolt > Build Unit Options...** after importing/updating scripts.

## Why this changed

Modern Android and iOS notification behavior requires explicit setup:

- Android 8+ requires notification channels.
- Android 13+ can require runtime notification permission.
- iOS requires notification authorization.
- Mobile Notifications 2.3+ provides a cross-platform `Unity.Notifications.NotificationCenter` API, which is the preferred simple path for Unity 6.

## New unified Bolt units

Find these in the Fuzzy Finder under `DopeTools/Notifications`:

- **Initialize** — creates/defaults the Android channel and initializes `NotificationCenter`.
- **Request Permission** — coroutine unit; outputs `granted`, `denied`, `finished`, and permission `status`.
- **Schedule Delayed** — schedules a notification after N seconds.
- **Schedule Date Time** — schedules a notification at a parsed date/time string.
- **Cancel Notification**
- **Cancel All Notifications**
- **Open Settings**
- **Notification Received** — event unit based on `Unity.Notifications.NotificationCenter.OnNotificationReceived`; outputs `received`, `title`, `text`, `data`, and `identifier`.

## Recommended graph flow

1. On app start: `Dope Notifications / Initialize`.
2. Then: `Dope Notifications / Request Permission`.
3. Only schedule notifications after the `granted` output.
4. Use `Dope Notifications / Notification Received` anywhere you need foreground/tapped notification payloads inside a Bolt graph.

## Defaults

Default Android channel:

- ID: `dope_default`
- Name: `Dope Notifications`
- Description: `Notifications scheduled from DopeTools.`

## Legacy Android/iOS units

The older platform-specific units are still present under:

- `DopeTools/Notifications/Android`
- `DopeTools/Notifications/iOS`

For Unity 6, prefer the new unified units unless you need platform-specific notification features.
