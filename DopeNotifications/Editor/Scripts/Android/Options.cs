#if DOPETOOLS_HAS_MOBILE_NOTIFICATIONS
#if UNITY_ANDROID
#if DOPETOOLS_BOLT_LEGACY
using Bolt;
using Ludiq;
#else
using Unity.VisualScripting;
#endif
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dopetools.DopeNotifications
{
    [InitializeAfterPlugins]
    public static class Options
    {
        static Options()
        {
            UnitBase.staticUnitsExtensions.Add(GetStaticOptions);
        }



        private static IEnumerable<IUnitOption> GetStaticOptions()
        {

            yield return new DopeAndroidNotificationsStatusUnitOption(new DopeAndroidNotificationsStatus(AndroidStatusType.Delivered));
            //yield return new DopeAndroidNotificationsStatusUnitOption(new DopeAndroidNotificationsStatus(AndroidStatusType.Scheduled)); //hiding because default shows up in fuzzyfinder
            yield return new DopeAndroidNotificationsStatusUnitOption(new DopeAndroidNotificationsStatus(AndroidStatusType.Unknown));

        }

    } //Options Class




} //namespace

#endif
#endif
