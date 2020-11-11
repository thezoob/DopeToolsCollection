using Ludiq;
using Bolt;
using UnityEngine;

[UnitSurtitle("theZoob")]
[UnitTitle("DopeCurves")]
[UnitSubtitle("Custom Animations for UI and more")]
[UnitCategory("DopeUI/Animation")]
[RenamedFrom("zoobAnimUnit")]

[TypeIcon(typeof(AnimationCurve))]

[Inspectable]
public class theZoobAnimates : Unit
{
  
    [DoNotSerialize]
    public ControlInput start;

    [DoNotSerialize]
    public ControlOutput exit;

    [DoNotSerialize]
    public ValueOutput easeIn;

    [DoNotSerialize]
    public ValueOutput easeOut;

    [DoNotSerialize]
    public ValueOutput easyEase;

    [DoNotSerialize]
    public ValueOutput Linear;

    [DoNotSerialize]
    public ValueOutput Bounce;

    [DoNotSerialize]
    public ValueOutput BounceTwo;

    [DoNotSerialize]
    public ValueOutput Spring;

    private AnimationCurve ZeaseIn;
    private AnimationCurve ZeaseOut;
    private AnimationCurve ZeasyEase;
    private AnimationCurve Zlinear;
    private AnimationCurve ZBounce;
    private AnimationCurve ZBounceTwo;
    private AnimationCurve ZSpring;


    protected override void Definition()
    {
        //start = ControlInput("start", (flow) => {
        //    return exit;
        //});

        //Ease In
        easeIn = ValueOutput<AnimationCurve>("Ease In", (flow) => 
        {  
            
            ZeaseIn = new AnimationCurve();
            ZeaseIn.AddKey(new Keyframe(0, 0, 0, 1f, 0.25f, 0.25f));
            ZeaseIn.AddKey(new Keyframe(1, 1, 0, 0, 1f, 1f));

            return ZeaseIn; });

        //Ease Out
        easeOut = ValueOutput<AnimationCurve>("Ease Out", (flow) => 
        {
            ZeaseOut = new AnimationCurve();
            ZeaseOut.AddKey(new Keyframe(0, 0, 0, 0, 1f, 1f));
            ZeaseOut.AddKey(new Keyframe(1, 1, .1f, 0.1f, 0.25f, 0.25f));

            return ZeaseOut; });

        //EasyEase
        easyEase = ValueOutput<AnimationCurve>("Easy Ease", (flow) =>
        {
            ZeasyEase = new AnimationCurve();
            ZeasyEase.AddKey(new Keyframe(0, 0, 0, 0));
            ZeasyEase.AddKey(new Keyframe(1, 1, 0, 0));

            return ZeasyEase;
        });

        //Linear
        Linear = ValueOutput<AnimationCurve>("Linear", (flow) => 
        {
            Zlinear = new AnimationCurve();
            Zlinear.AddKey(new Keyframe(0, 0, 0, 0, 0, 0));
            Zlinear.AddKey(new Keyframe(1, 1, 0, 0, 0, 0));

            return Zlinear; });

        

        //Bounce01
        Bounce = ValueOutput<AnimationCurve>("Bounce", (flow) => 
        {
            ZBounce = new AnimationCurve();
            ZBounce.AddKey(new Keyframe(0, 0, 0, 0, 0, .225f));
            ZBounce.AddKey(new Keyframe(.55f, 1, 10, -16, .1f, .1f));
            ZBounce.AddKey(new Keyframe(.9f, 1, 10, -16, .1f, .1f));
            ZBounce.AddKey(new Keyframe(1, 1, 10, -16, .1f, .1f));

            return ZBounce; });

        //Bounce02
        BounceTwo = ValueOutput<AnimationCurve>("Bounce 2", (flow) =>
        {
            ZBounceTwo = new AnimationCurve();
            ZBounceTwo.AddKey(new Keyframe(0, 0, 0, 0, 0, .225f));
            ZBounceTwo.AddKey(new Keyframe(.55f, 1, 10, -16, .1f, .1f));
           //ZBounceTwo.AddKey(new Keyframe(.9f, 1, 10, -16, .1f, .1f));
            ZBounceTwo.AddKey(new Keyframe(1, 1, 10, -16, .1f, .1f));

            return ZBounceTwo;
        });

        //Spring01
        Spring = ValueOutput<AnimationCurve>("Spring", (flow) =>
        {
            ZSpring = new AnimationCurve();
            ZSpring.AddKey(new Keyframe(0, 0, 4, 4, .1f, .1f));
            ZSpring.AddKey(new Keyframe(0.24f, 1, 4, 4, .1f, .38f));
            ZSpring.AddKey(new Keyframe(0.55f, 1, 2.33f, 2.33f, .44f, .44f));
            ZSpring.AddKey(new Keyframe(0.73f, 1, 2.22f, 2.22f, .35f, .24f));
            ZSpring.AddKey(new Keyframe(1, 1, 0, 0, .49f, 0.1f));

            return ZSpring;
        });

        //exit = ControlOutput("exit");

        //Succession(start, exit);


    }
}