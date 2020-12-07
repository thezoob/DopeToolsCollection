using Ludiq;
using System;
using Bolt;

namespace Dopetools.Animation
{

    [FuzzyOption(typeof(DopeEasingUnit))]
    public class DopeEasingUnitOption : UnitOption<DopeEasingUnit>
    {
        public DopeEasingType type;

        [Obsolete()]
        public DopeEasingUnitOption() : base() { }

        public DopeEasingUnitOption(DopeEasingUnit unit) : base(unit)
        {

        }


        protected override string Label(bool human)
        {
            //return $"{LudiqGUIUtility.DimString("DopeCurves")} { unit._dope?.GetType().Name }";
            return unit.dopeEasingType.ToString();
        }

        protected override UnitCategory Category()
        {
            return new UnitCategory("DopeTools/Animation/Easeing");
        }
    }

}