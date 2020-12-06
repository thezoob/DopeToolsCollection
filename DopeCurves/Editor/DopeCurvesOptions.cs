using Ludiq;
using System;
using Bolt;

namespace Dopetools.Animation
{

    [FuzzyOption(typeof(DopeCurves))]
    public class DopeCurvesUnitOption : UnitOption<DopeCurves>
    {
        private DopeCurvesType type;

        [Obsolete()]
        public DopeCurvesUnitOption() : base() { }

        public DopeCurvesUnitOption(DopeCurves unit) : base(unit)
        {

        }


        protected override string Label(bool human)
        {
            //return $"{LudiqGUIUtility.DimString("DopeCurves")} { unit._dope?.GetType().Name }";
            return unit.dopeCurvesType.ToString();
        }

        protected override UnitCategory Category()
        {
            return new UnitCategory("DopeTools/Animation");
        }
    }

}