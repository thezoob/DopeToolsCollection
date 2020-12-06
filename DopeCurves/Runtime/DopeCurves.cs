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

        public DopeCurves() : base() { }

        //constructor
        public DopeCurves(DopeCurvesType dopeCurvesType) : base() 
        {
            this.dopeCurvesType = dopeCurvesType;
        }

        public DopeCurves(DopeCurves dopeCurves)
        {
            this.dopeCurves = dopeCurves;
        }

        //Bolt Input and Outputs
        [DoNotSerialize]
        public ControlInput start;

        [DoNotSerialize]
        public ValueOutput CurveOut;

        
        [UnitHeaderInspectable] //Shows Type Enum in the Unit header
        [Inspectable,InspectorToggleLeft] //Shows Type Enum in the Graph Inspector
        [Serialize]
        [InspectorLabel("Type")] // Sets a custom label for the Type
        public DopeCurvesType dopeCurvesType;

        private AnimationCurve EaseIn;
        private AnimationCurve EaseOut;
        private AnimationCurve EasyEase;
        private AnimationCurve Linear;
        private AnimationCurve Bounce;
        private AnimationCurve BounceTwo;
        //private AnimationCurve BounceThree;
        private AnimationCurve Spring;
        private AnimationCurve SpringTwo;
        private AnimationCurve Overshoot;
        private AnimationCurve Wobble;
        private DopeCurves dopeCurves;
      

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
                    Bounce.AddKey(new Keyframe(0f, 0f, 0.005653797f, 0.005653797f, 0f, 0.3173561f));
                    Bounce.AddKey(new Keyframe(0.5505112f, 0.9988624f, 7.667589f, -7.447864f, 0.1135627f, 0.1615442f));
                    Bounce.AddKey(new Keyframe(0.8715746f, 1f, 12.94001f, -5.531136f, 0.07680575f, 0.2061866f));
                    Bounce.AddKey(new Keyframe(1f, 1f, 6.345931f, 6.345931f, 0.1818196f, 0f));

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
                    Overshoot.AddKey(new Keyframe(0.5145634f, 0.9986308f, -9.947139f, -9.947139f, 0.2570528f, 0.308486f));
                    Overshoot.AddKey(new Keyframe(0.708802f, 1.00238f, 4.208149f, 4.208149f, 0.1875436f, 0.4025052f));
                    Overshoot.AddKey(new Keyframe(1f, 1f, -0.009726197f, -0.009726197f, 0.863602f, 0f));

                    return Overshoot;
                }

                else if (dopeCurvesType == DopeCurvesType.Wobble)
                {
                    Wobble = new AnimationCurve();
                    Wobble.AddKey(new Keyframe(0f, 1f, 6.580297f, 7.377839f, 0f, 0.4721865f));
                    Wobble.AddKey(new Keyframe(0.25f, 1.001412f, -5.292815f, -5.292815f, 0.3164361f, 0.3506964f));
                    Wobble.AddKey(new Keyframe(0.5f, 1f, 5.295044f, 5.295044f, 0.4449989f, 0.4682603f));
                    Wobble.AddKey(new Keyframe(0.72007f, 1.005647f, -4.864528f, -4.864528f, 0.03870767f, 0.2156601f));
                    Wobble.AddKey(new Keyframe(1f, 1f, 0.01301962f, -0.01633183f, 0.6326667f, 0f));

                    return Wobble;
                }

                return null; });


        }
    }
}