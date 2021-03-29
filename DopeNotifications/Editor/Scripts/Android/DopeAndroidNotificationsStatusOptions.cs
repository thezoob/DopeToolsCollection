#if UNITY_ANDROID
using Unity.VisualScripting;
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