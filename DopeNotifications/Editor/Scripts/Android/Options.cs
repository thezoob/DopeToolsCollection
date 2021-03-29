#if UNITY_ANDROID
using Unity.VisualScripting;
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