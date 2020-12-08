using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dopetools.Animation
{
    [InitializeAfterPlugins]
    public static class Options
    {
        static Options()
        {
            UnitBase.staticUnitsExtensions.Add(GetDopeCurvesOptions);
            UnitBase.staticUnitsExtensions.Add(GetDopeEaseingOptions);
        }


        
        private static IEnumerable<IUnitOption> GetDopeCurvesOptions()
        {
            
            //yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.EaseIn)); //hiding because default shows up in fuzzyfinder
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.EaseOut));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.EasyEase));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.Linear));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.Bounce));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.BounceTwo));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.Spring));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.SpringTwo));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.Overshoot));
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.Wobble));

        }

        private static IEnumerable<IUnitOption> GetDopeEaseingOptions()
        {
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInBack));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutBack));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutBack));

            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInBounce));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutBounce));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutBounce));

            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInCirc));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutCirc));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutCirc));

            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInCubic));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutCubic));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutCubic));

            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInElastic));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutElastic));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutElastic));

            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInExpo));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutExpo));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutExpo));

            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInQuad));
            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutQuad));
            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutQuad));

            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInQuart));
            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutQuart));
            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutQuart));

            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInQuint));
            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutQuint));
            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutQuint));

            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInSine));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseOutSine));
            yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.EaseInOutSine));

            //yield return new DopeEasingUnitOption(new DopeEasingUnit(DopeEasingType.Linear)); //hiding because default shows up in fuzzyfinder

        }

    } //Options Class

} //namespace