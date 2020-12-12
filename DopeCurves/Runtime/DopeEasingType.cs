using Ludiq;
namespace Dopetools.Tweening
{
    [RenamedFrom("Dopetools.Animation.DopeEasingType")]
    public enum DopeEasingType
    {
        //Quadratic,
        //Cubic,
        //Quartic,
        //Quintic,
        //Sinusoidal,
        //Exponential,
        //Circular,
        //Elastic,
        //Back,
        //Bounce,
        Linear,
        EaseInSine, EaseOutSine, EaseInOutSine,
        EaseInCubic, EaseOutCubic, EaseInOutCubic,
        //EaseInQuint, EaseOutQuint, EaseInOutQuint,
        EaseInCirc, EaseOutCirc, EaseInOutCirc,
        EaseInElastic, EaseOutElastic, EaseInOutElastic,
        //EaseInQuad, EaseOutQuad, EaseInOutQuad,
        //EaseInQuart,EaseOutQuart, EaseInOutQuart,
        EaseInExpo, EaseOutExpo, EaseInOutExpo,
        EaseInBack, EaseOutBack, EaseInOutBack,
        EaseInBounce, EaseOutBounce, EaseInOutBounce
    }
}