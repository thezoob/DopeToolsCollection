#if DOPETOOLS_HAS_MOBILE_NOTIFICATIONS
#if UNITY_ANDROID
#if DOPETOOLS_BOLT_LEGACY
using Bolt;
using Ludiq;
#else
using Unity.VisualScripting;
#endif
using System;

namespace Dopetools.DopeNotifications
{

    [FuzzyOption(typeof(DopeAndroidNotificationsStatus))]
    public class DopeAndroidNotificationsStatusUnitOption : UnitOption<DopeAndroidNotificationsStatus>
    {
        private AndroidStatusType type;

        [Obsolete()]
        public DopeAndroidNotificationsStatusUnitOption() : base() { }

        public DopeAndroidNotificationsStatusUnitOption(DopeAndroidNotificationsStatus unit) : base(unit)
        {

        }


        protected override string Label(bool human)
        {
            //return "Status." + unit.androidStatusType.GetType().Name;
            return "Status." + unit.androidStatusType.ToString();
        }

        protected override UnitCategory Category()
        {
            return new UnitCategory("DopeTools/Notifications/Android");
        }
    }

}

#endif
#endif
