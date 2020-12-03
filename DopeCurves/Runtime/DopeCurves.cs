using Ludiq;
using Bolt;
using UnityEngine;

namespace Dopetools.Animation
{

    //[UnitSurtitle("DopeTools")]
    [UnitTitle("DopeCurves")]
    [UnitCategory("DopeTools/Animation")]
    [RenamedFrom("DopeCurves")]
    [TypeIcon(typeof(AnimationCurve))]

    public sealed class DopeCurves : Unit
    {

        [DoNotSerialize]
        public ControlInput start;

        [DoNotSerialize]
        public ValueOutput CurveOut;

        [UnitHeaderInspectable]
        [Inspectable]
        [InspectorToggleLeft]
        [Serialize]
        [InspectorLabel("Type")]
        public DopeCurvesType dopeCurvesType;

        private AnimationCurve EaseIn;
        private AnimationCurve EaseOut;
        private AnimationCurve EasyEase;
        private AnimationCurve Linear;
        private AnimationCurve Bounce;
        private AnimationCurve BounceTwo;
        private AnimationCurve Spring;
        private AnimationCurve SpringTwo;
        private AnimationCurve Overshoot;



        protected override void Definition()
        {

            CurveOut = ValueOutput<AnimationCurve>("Curve", (flow) => 
            {
                if (dopeCurvesType == DopeCurvesType.EaseIn)
                {
                EaseIn = new AnimationCurve();
                EaseIn.AddKey(new Keyframe(0, 0, 0, 1f, 0.25f, 0.25f));
                EaseIn.AddKey(new Keyframe(1, 1, 0, 0, 1f, 1f));

                return EaseIn;         
                }

                else if (dopeCurvesType == DopeCurvesType.EaseOut)
                {
                    EaseOut = new AnimationCurve();
                    EaseOut.AddKey(new Keyframe(0, 0, 0, 0, 1f, 1f));
                    EaseOut.AddKey(new Keyframe(1, 1, .1f, 0.1f, 0.25f, 0.25f));

                    return EaseOut;
                }

                //else if (dopeCurvesType == DopeCurvesType.EasyEase)
                //{
                //    easyEase = new AnimationCurve();
                //    easyEase.AddKey(new Keyframe(0, 0, 0, 0));
                //    easyEase.AddKey(new Keyframe(1, 1, 0, 0));

                //    return easyEase;
                //}

                else if (dopeCurvesType == DopeCurvesType.EasyEase)
                {
                    EasyEase = new AnimationCurve();
                    EasyEase.AddKey(new Keyframe(0f, 0f, 0.008976218f, 0.008976218f, 0f, 0.6962844f));
                    EasyEase.AddKey(new Keyframe(1f, 1f, 0.009869299f, 0.009869299f, 0.6332794f, 0f));

                    return EasyEase;
                }

                else if (dopeCurvesType == DopeCurvesType.Linear)
                {
                    Linear = new AnimationCurve();
                    Linear.AddKey(new Keyframe(0, 0, 0, 0, 0, 0));
                    Linear.AddKey(new Keyframe(1, 1, 0, 0, 0, 0));

                    return Linear;
                }

                else if (dopeCurvesType == DopeCurvesType.Bounce)
                {
                    Bounce = new AnimationCurve();
                    Bounce.AddKey(new Keyframe(0, 0, 0, 0, 0, .225f));
                    Bounce.AddKey(new Keyframe(.55f, 1, 10, -16, .1f, .1f));
                    Bounce.AddKey(new Keyframe(.9f, 1, 10, -16, .1f, .1f));
                    Bounce.AddKey(new Keyframe(1, 1, 10, -16, .1f, .1f));

                    return Bounce;
                }

                else if (dopeCurvesType == DopeCurvesType.BounceTwo)
                {
                    BounceTwo = new AnimationCurve();
                    BounceTwo.AddKey(new Keyframe(0, 0, 0, 0, 0, .225f));
                    BounceTwo.AddKey(new Keyframe(.55f, 1, 10, -16, .1f, .1f));
                    BounceTwo.AddKey(new Keyframe(1, 1, 10, -16, .1f, .1f));

                    return BounceTwo;
                }

                //else if (dopeCurvesType == DopeCurvesType.Spring)
                //{
                //    Spring = new AnimationCurve();
                //    Spring.AddKey(new Keyframe(0, 0, 4, 4, .1f, .1f));
                //    Spring.AddKey(new Keyframe(0.24f, 1, 4, 4, .1f, .38f));
                //    Spring.AddKey(new Keyframe(0.55f, 1, 2.33f, 2.33f, .44f, .44f));
                //    Spring.AddKey(new Keyframe(0.73f, 1, 2.22f, 2.22f, .35f, .24f));
                //    Spring.AddKey(new Keyframe(1, 1, 0, 0, .49f, 0.1f));

                //    return Spring;
                //}

                else if (dopeCurvesType == DopeCurvesType.Spring)
                {
                    Spring = new AnimationCurve();
                    Spring.AddKey(new Keyframe(0f, 0f, 6.580297f, 6.580297f, 0f, 0.4577363f));
                    Spring.AddKey(new Keyframe(0.2696471f, 0.649292f, -6.779356f, -6.779356f, 0.3745706f, 0.2428971f));
                    Spring.AddKey(new Keyframe(0.5139271f, 0.4862061f, 6.191417f, 6.191417f, 0.3927316f, 0.6516353f));
                    Spring.AddKey(new Keyframe(0.8208866f, 0.5081918f, 2.919304f, 2.919304f, 0.3156461f, 0.4711816f));
                    Spring.AddKey(new Keyframe(1f, 1f, 0.04363899f, 0.04363899f, 0.4646997f, 0f));

                    return Spring;
                }

                else if (dopeCurvesType == DopeCurvesType.SpringTwo)
                {
                    SpringTwo = new AnimationCurve();
                    SpringTwo.AddKey(new Keyframe(0f, 0f, 1.38364f, 1.38364f, 0f, 0.3681916f));
                    SpringTwo.AddKey(new Keyframe(0.2412047f, 1f, 3.750271f, 3.750271f, 0.3333333f, 0.2861784f));
                    SpringTwo.AddKey(new Keyframe(0.547001f, 0.9986308f, -2.482575f, -2.482575f, 0.3333333f, 0.3333333f));
                    SpringTwo.AddKey(new Keyframe(0.7322292f, 1f, 2.341092f, 2.341092f, 0.3573901f, 0.3333333f));
                    SpringTwo.AddKey(new Keyframe(1f, 1f, 0.7791052f, 0.7791052f, 0.391697f, 0f));

                    return SpringTwo;
                }

                else if (dopeCurvesType == DopeCurvesType.Overshoot)
                {
                    Overshoot = new AnimationCurve();
                    Overshoot.AddKey(new Keyframe(0f, 0f, 0.5196241f, 0.5196241f, 0f, 0.3483061f));
                    Overshoot.AddKey(new Keyframe(0.2412047f, 1.003699f, 8.351594f, 8.351594f, 0.3333333f, 0.4326186f));
                    Overshoot.AddKey(new Keyframe(0.547001f, 0.9986308f, -9.947139f, -9.947139f, 0.2570528f, 0.308486f));
                    Overshoot.AddKey(new Keyframe(0.7322292f, 1.00238f, 5.724898f, 5.724898f, 0.1875436f, 0.4645106f));
                    Overshoot.AddKey(new Keyframe(1f, 1f, -0.0125994f, -0.0125994f, 0.5247501f, 0f));

                    return Overshoot;
                }



                return null; });


        }
    }
}