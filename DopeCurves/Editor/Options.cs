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
            UnitBase.staticUnitsExtensions.Add(GetStaticOptions);
        }


        
        private static IEnumerable<IUnitOption> GetStaticOptions()
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

    } //Options Class

} //namespace