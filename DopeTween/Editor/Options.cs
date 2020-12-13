using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dopetools.Tweening
{
    [InitializeAfterPlugins]
    public static class Options
    {
        static Options()
        {
            UnitBase.staticUnitsExtensions.Add(GetDopeTweenOptions);
        }

        private static IEnumerable<IUnitOption> GetDopeTweenOptions()
        {
            yield return new DopeTweenUnitOption(new DopeTweenUnit(DopeTweenInputValueType.Color));
            yield return new DopeTweenUnitOption(new DopeTweenUnit(DopeTweenInputValueType.Float));
            yield return new DopeTweenUnitOption(new DopeTweenUnit(DopeTweenInputValueType.String));
            yield return new DopeTweenUnitOption(new DopeTweenUnit(DopeTweenInputValueType.Vector2));
            yield return new DopeTweenUnitOption(new DopeTweenUnit(DopeTweenInputValueType.Vector3));
            yield return new DopeTweenUnitOption(new DopeTweenUnit(DopeTweenInputValueType.Quaternion));


        }

    } //Options Class

} //namespace