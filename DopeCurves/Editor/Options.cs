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
            
            yield return new DopeCurvesUnitOption(new DopeCurves(DopeCurvesType.EaseIn));
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



        //private static IEnumerable<IUnitOption> GetStaticOptions()
        //{
        //    List<Type> result = new List<Type>();
        //    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        //    for (int assembly = 0; assembly < assemblies.Length; assembly++)
        //    {
        //        Type[] types = assemblies[assembly].GetTypes();

        //        for (int type = 0; type < types.Length; type++)
        //        {
        //            if (!types[type].IsAbstract)
        //            {
        //                if (typeof(IDopeCurves).IsAssignableFrom(types[type]))
        //                {
        //                    yield return new DopeCurvesUnitOption(new DopeCurves());
        //                    //yield return new DopeCurvesUnitOption(new ActionInvokeUnit(Activator.CreateInstance(types[type] as System.Type) as IAction));
        //                }

        //            }
        //        }
        //    }

    } //Options Class

    


} //namespace