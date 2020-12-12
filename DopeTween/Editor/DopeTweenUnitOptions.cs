using Ludiq;
using System;
using Bolt;

namespace Dopetools.Tweening
{
    [RenamedFrom("Dopetools.Animation.DopeTweenUnitOption")]
    [FuzzyOption(typeof(DopeTweenUnit))]
    public class DopeTweenUnitOption : UnitOption<DopeTweenUnit>
    {
        public DopeTweenInputValueType type;

        [Obsolete()]
        public DopeTweenUnitOption() : base() { }

        public DopeTweenUnitOption(DopeTweenUnit unit) : base(unit)
        {

        }


        protected override string Label(bool human)
        {
            //return $"{LudiqGUIUtility.DimString("DopeCurves")} { unit._dope?.GetType().Name }";
            return "DopeTween." + unit.dopeTweenInputValueType.ToString();
        }

        protected override UnitCategory Category()
        {
            return new UnitCategory("DopeTools/Tween");
        }
    }

}