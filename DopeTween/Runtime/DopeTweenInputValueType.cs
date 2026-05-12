
#if DOPETOOLS_BOLT_LEGACY
using Bolt;
using Ludiq;
#else
using Unity.VisualScripting;
#endif
namespace Dopetools.Tweening
{
    [RenamedFrom("Dopetools.Animation.DopeTweenInputValueType")]
    public enum DopeTweenInputValueType
    {
        Color, Float, Quaternion, String, Vector2, Vector3 
    }
}