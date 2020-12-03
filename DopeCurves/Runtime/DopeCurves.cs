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

        public DopeCurvesType dopeCurvesType;

        private AnimationCurve ZeaseIn;
        private AnimationCurve ZeaseOut;
        private AnimationCurve ZeasyEase;
        private AnimationCurve Zlinear;
        private AnimationCurve ZBounce;
        private AnimationCurve ZBounceTwo;
        private AnimationCurve ZSpring;
        private AnimationCurve ZSpringTwo;

        

        protected override void Definition()
        {

            CurveOut = ValueOutput<AnimationCurve>("Curve", (flow) => 
            {
                if (dopeCurvesType == DopeCurvesType.EaseIn)
                {
                ZeaseIn = new AnimationCurve();
                ZeaseIn.AddKey(new Keyframe(0, 0, 0, 1f, 0.25f, 0.25f));
                ZeaseIn.AddKey(new Keyframe(1, 1, 0, 0, 1f, 1f));

                return ZeaseIn;         
                }

                else if (dopeCurvesType == DopeCurvesType.EaseOut)
                {
                    ZeaseOut = new AnimationCurve();
                    ZeaseOut.AddKey(new Keyframe(0, 0, 0, 0, 1f, 1f));
                    ZeaseOut.AddKey(new Keyframe(1, 1, .1f, 0.1f, 0.25f, 0.25f));

                    return ZeaseOut;
                }

                else if (dopeCurvesType == DopeCurvesType.EasyEase)
                {
                    ZeasyEase = new AnimationCurve();
                    ZeasyEase.AddKey(new Keyframe(0, 0, 0, 0));
                    ZeasyEase.AddKey(new Keyframe(1, 1, 0, 0));

                    return ZeasyEase;
                }

                else if (dopeCurvesType == DopeCurvesType.Linear)
                {
                    Zlinear = new AnimationCurve();
                    Zlinear.AddKey(new Keyframe(0, 0, 0, 0, 0, 0));
                    Zlinear.AddKey(new Keyframe(1, 1, 0, 0, 0, 0));

                    return Zlinear;
                }

                else if (dopeCurvesType == DopeCurvesType.Bounce)
                {
                    ZBounce = new AnimationCurve();
                    ZBounce.AddKey(new Keyframe(0, 0, 0, 0, 0, .225f));
                    ZBounce.AddKey(new Keyframe(.55f, 1, 10, -16, .1f, .1f));
                    ZBounce.AddKey(new Keyframe(.9f, 1, 10, -16, .1f, .1f));
                    ZBounce.AddKey(new Keyframe(1, 1, 10, -16, .1f, .1f));

                    return ZBounce;
                }

                else if (dopeCurvesType == DopeCurvesType.Bounce02)
                {
                    ZBounceTwo = new AnimationCurve();
                    ZBounceTwo.AddKey(new Keyframe(0, 0, 0, 0, 0, .225f));
                    ZBounceTwo.AddKey(new Keyframe(.55f, 1, 10, -16, .1f, .1f));
                    ZBounceTwo.AddKey(new Keyframe(1, 1, 10, -16, .1f, .1f));

                    return ZBounceTwo;
                }

                else if (dopeCurvesType == DopeCurvesType.Spring)
                {
                    ZSpring = new AnimationCurve();
                    ZSpring.AddKey(new Keyframe(0, 0, 4, 4, .1f, .1f));
                    ZSpring.AddKey(new Keyframe(0.24f, 1, 4, 4, .1f, .38f));
                    ZSpring.AddKey(new Keyframe(0.55f, 1, 2.33f, 2.33f, .44f, .44f));
                    ZSpring.AddKey(new Keyframe(0.73f, 1, 2.22f, 2.22f, .35f, .24f));
                    ZSpring.AddKey(new Keyframe(1, 1, 0, 0, .49f, 0.1f));

                    return ZSpring;
                }

                else if (dopeCurvesType == DopeCurvesType.SpringTwo)
                {
                    
                    ZSpringTwo = new AnimationCurve();
                    ZSpringTwo.AddKey(new Keyframe(0f, 0f, 0.4748435f, 0.4748435f, 0f, 0.1590377f));
                    ZSpringTwo.AddKey(new Keyframe(0.2517943f, 1f, 4.138161f, 4.138161f, 0.04589567f, 0f));
                    ZSpringTwo.AddKey(new Keyframe(0.4259982f, 1.004308f, -3.489567f, -3.489567f, 0.3333333f, 0.1270812f));
                    ZSpringTwo.AddKey(new Keyframe(0.5461915f, 1.002129f, 2.534065f, 2.534065f, 0.1185575f, 0.1054101f));
                    ZSpringTwo.AddKey(new Keyframe(0.6899607f, 1.008241f, -2.281703f, -2.281703f, 0.3333333f, 0.1704702f));
                    ZSpringTwo.AddKey(new Keyframe(0.7968872f, 1f, 1.967899f, 1.967899f, 0.1427524f, 0.3603327f));
                    ZSpringTwo.AddKey(new Keyframe(0.8914288f, 0.999182f, -1.533596f, -1.533596f, 0.3381487f, 0.2208212f));
                    ZSpringTwo.AddKey(new Keyframe(1f, 1f, 0.02278714f, 0.02278714f, 0.3567416f, 0f));

                    return ZSpringTwo;
                }


                return null; });


        }
    }
}