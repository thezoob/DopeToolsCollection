using System;
using Ludiq;
using UnityEngine;
using Bolt;

namespace Dopetools.Tweening
{
    /// <summary>
    /// Runs a timer for Duration entered and outputs tweened values from A to B.
    /// </summary>
    [UnitCategory("DopeTools")]
    [UnitSurtitle("DopeTween")]
    [RenamedFrom("Dopetools.Animation.DopeTweenUnit")]
    public sealed class DopeTweenUnit : Unit, IGraphElementWithData, IGraphEventListener
    {
        public sealed class Data : IGraphElementData
        {
            public float elapsed;

            public float duration;

            public bool active;

            public bool paused;

            public bool unscaled;

            public Delegate update;

            public bool isListening;

            public DopeEasingType dopeEasingType;

            //public DopeTweenInputValueType dopeTweenInput;
            
            public object resultValue;

            public object A;
            public object B;
        }

        public DopeTweenUnit() : base() { }

        public DopeTweenUnit(DopeTweenInputValueType dopeTweenInputValueType) : base()
        {
            this.dopeTweenInputValueType = dopeTweenInputValueType;
        }

        //public DopeTweenUnit(DopeEasingType dopeEasingType, DopeTweenInputValueType dopeTweenInputValueType) : base()
        //{
        //    this.dopeTweenInputValueType = dopeTweenInputValueType;
        //}

        public DopeTweenUnit(DopeTweenUnit dopeTweenUnit)
        {
            this.dopeTweenUnit = dopeTweenUnit;
        }

        private DopeTweenUnit dopeTweenUnit;

        /// <summary>
        /// The moment at which to start the tween.
        /// </summary>
        [DoNotSerialize, PortLabelHidden]
        public ControlInput start { get; private set; }

        [Serialize, InspectorToggleLeft, Inspectable, InspectorWide, InspectorRange(.1f, 1f), InspectorLabel("Smooth (Elastic)")]
        private float smooth = .4f;

        [Serialize, InspectorToggleLeft, Inspectable, InspectorWide, InspectorRange(1f, 6f), InspectorLabel("Spring (Elastic)")]
        private float spring = 2f;

        //[Serialize, InspectorLabel("Ease"), InspectorWide]
        //[Inspectable, InspectorToggleLeft, UnitHeaderInspectable] //Show Type Options in Header.
        //public DopeEasingType dopeEasingType { get; private set; }

        [Serialize, InspectorLabel("Tween"), InspectorWide]
        [Inspectable, InspectorToggleLeft] //Show Type Options in Header.
        public DopeTweenInputValueType dopeTweenInputValueType { get; private set; }

        //<summary>
        //Starting Value
        //<summary>
        [DoNotSerialize]
        public ValueInput A;

        //<summary>
        //Ending Value
        //<summary>
        [DoNotSerialize]
        public ValueInput B;

        /// <summary>
        /// The total duration of the timer.
        /// </summary>
        [DoNotSerialize]
        public ValueInput duration { get; private set; }

        [DoNotSerialize]
        public ValueInput DopeEasingTypeValue { get; private set; }

        public DopeEasingType dopeEasingType { get; private set; }

        /// <summary>
        /// Whether to ignore the time scale.
        /// </summary>
        [DoNotSerialize]
        [PortLabel("Unscaled")]
        public ValueInput unscaledTime { get; private set; }

        /// <summary>
        /// Called when the timer is started.co
        /// </summary>
        [DoNotSerialize]
        public ControlOutput started { get; private set; }

        /// <summary>
        /// Called each frame while the timer is active.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput tick { get; private set; }

        /// <summary>
        /// Called when the timer completes.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput completed { get; private set; }

        [DoNotSerialize, PortLabelHidden]
        public ValueOutput ResultValue;


        public IUnitValuePort elapsedSeconds { get; private set; }
        public IUnitValuePort elapsedRatio { get; private set; }
        public IUnitValuePort remainingSeconds { get; private set; }
        public IUnitValuePort remainingRatio { get; private set; }

        //private bool check;

        protected override void Definition()
        {
            isControlRoot = true;
            DopeEasingTypeValue = ValueInput(nameof(dopeEasingType), DopeEasingType.Linear); //set valueinput to variable

            if (dopeTweenInputValueType == DopeTweenInputValueType.Vector3 || dopeTweenInputValueType == DopeTweenInputValueType.Quaternion)
            {
                A = ValueInput<Vector3>("A", new Vector3(0f, 0f, 0f));
                B = ValueInput<Vector3>("B", new Vector3(0f, 0f, 0f));

            }
            else if (dopeTweenInputValueType == DopeTweenInputValueType.Vector2)
            {
                A = ValueInput<Vector2>("A", new Vector2(0f, 0f));
                B = ValueInput<Vector2>("B", new Vector2(0f, 0f));
            }
            else if (dopeTweenInputValueType == DopeTweenInputValueType.Color)
            {
                A = ValueInput<Color>("A", new Color(0, 0, 0, 1));
                B = ValueInput<Color>("B", new Color(1, 1, 1, 1));
            }
            else if (dopeTweenInputValueType == DopeTweenInputValueType.Float)
            {
                A = ValueInput<float>("A", 0f);
                B = ValueInput<float>("B", 0f);
            }
            else if (dopeTweenInputValueType == DopeTweenInputValueType.String)
            {
                A = ValueInput<string>("A", string.Empty);
                //B = ValueInput<string>("B", string.Empty);
            }

            start = ControlInput(nameof(start), Start);

            duration = ValueInput(nameof(duration), 1f);
            unscaledTime = ValueInput(nameof(unscaledTime), false);

            started = ControlOutput(nameof(started));
            tick = ControlOutput(nameof(tick));
            completed = ControlOutput(nameof(completed));

            ResultValue = ValueOutput(dopeTweenInputValueType == DopeTweenInputValueType.Color ? typeof(Color) : 
                dopeTweenInputValueType == DopeTweenInputValueType.Float ? typeof(float) : 
                dopeTweenInputValueType == DopeTweenInputValueType.String ? typeof(string) : 
                dopeTweenInputValueType == DopeTweenInputValueType.Vector2 ? typeof(Vector2) : 
                dopeTweenInputValueType == DopeTweenInputValueType.Vector3 ? typeof(Vector3) :
                dopeTweenInputValueType == DopeTweenInputValueType.Quaternion ? typeof(Quaternion) : null, "ResultValue");

            Succession(start, started);
            Succession(start, tick);
            Succession(start, completed);
            Requirement(A, start);
            if (dopeTweenInputValueType != DopeTweenInputValueType.String) { Requirement(B, start); }
            Requirement(DopeEasingTypeValue, start);
            Requirement(duration, start);
            Requirement(unscaledTime, start);
            Assignment(start, ResultValue);

        } //Definition

        public IGraphElementData CreateData()
        {
            return new Data();
        }

        public void StartListening(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);

            if (data.isListening)
            {
                return;
            }

            var reference = stack.ToReference();
            var hook = new EventHook(EventHooks.Update, stack.machine);
            Action<EmptyEventArgs> update = args => TriggerUpdate(reference);
            EventBus.Register(hook, update);
            data.update = update;
            data.isListening = true;
        }

        public void StopListening(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);

            if (!data.isListening)
            {
                return;
            }

            var hook = new EventHook(EventHooks.Update, stack.machine);
            EventBus.Unregister(hook, data.update);
            data.update = null;
            data.isListening = false;
        }

        public bool IsListening(GraphPointer pointer)
        {
            return pointer.GetElementData<Data>(this).isListening;
        }

        private void TriggerUpdate(GraphReference reference)
        {
            using (var flow = Flow.New(reference))
            {
                Update(flow);
            }
        }

        private ControlOutput Start(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            data.elapsed = 0;
            data.duration = flow.GetValue<float>(duration);
            data.active = true;
            data.paused = false;
            data.unscaled = flow.GetValue<bool>(unscaledTime);
            data.dopeEasingType = flow.GetValue<DopeEasingType>(DopeEasingTypeValue);


            if (dopeTweenInputValueType == DopeTweenInputValueType.Float) { data.A = flow.GetValue<float>(A); data.B = flow.GetValue<float>(B); }

            else if (dopeTweenInputValueType == DopeTweenInputValueType.Vector3) { data.A = flow.GetValue<Vector3>(A); data.B = flow.GetValue<Vector3>(B); }

            else if (dopeTweenInputValueType == DopeTweenInputValueType.Vector2) { data.A = flow.GetValue<Vector2>(A); data.B = flow.GetValue<Vector2>(B); }

            else if (dopeTweenInputValueType == DopeTweenInputValueType.Color) { data.A = flow.GetValue<Color>(A); data.B = flow.GetValue<Color>(B); }

            else if (dopeTweenInputValueType == DopeTweenInputValueType.String) { data.A = flow.GetValue<string>(A); data.B = flow.GetValue<string>(B); }

            else if (dopeTweenInputValueType == DopeTweenInputValueType.Quaternion) { data.A = flow.GetValue<Vector3>(A); data.B = flow.GetValue<Vector3>(B); }

            AssignMetrics(flow, data);

            return started;
        }


        

        private void AssignMetrics(Flow flow, Data data)
        {
            dopeEasingType = flow.GetValue<DopeEasingType>(DopeEasingTypeValue);

            var ratio = Mathf.Clamp01(data.elapsed / data.duration);
            var value = dopeTweenInputValueType == DopeTweenInputValueType.Float ? Mathf.LerpUnclamped((float)data.A, (float)data.B, EasedValue(ratio)) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Vector3 ? Vector3.LerpUnclamped((Vector3)data.A, (Vector3)data.B, EasedValue(ratio)) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Quaternion ? Quaternion.LerpUnclamped(Quaternion.Euler((Vector3)data.A), Quaternion.Euler((Vector3)data.B), EasedValue(ratio)) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Vector2 ? Vector2.LerpUnclamped((Vector2)data.A, (Vector2)data.B, EasedValue(ratio)) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Color ? Color.LerpUnclamped((Color)data.A, (Color)data.B, EasedValue(ratio)) :
                        dopeTweenInputValueType == DopeTweenInputValueType.String ? (string)data.A : data.A; 

            flow.SetValue(ResultValue, value);

        }


        public void Update(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (!data.active || data.paused)
            {
                return;
            }

            data.elapsed += data.unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

            data.elapsed = Mathf.Min(data.elapsed, data.duration);

            AssignMetrics(flow, data);

            flow.Invoke(tick);

            if (data.elapsed >= data.duration)
            {
                data.active = false;
                //AssignMetrics(flow, data);
                flow.Invoke(completed);
            }
        }

        private float EasedValue(float inputTime)
        {

            if (dopeEasingType == DopeEasingType.Linear)
            {
                float ease = Linear.Lin(inputTime);
                return ease;
            }
            if (dopeEasingType == DopeEasingType.EaseInBack)
            {
                float ease = Back.In(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutBack)
            {
                float ease = Back.Out(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutBack)
            {
                float ease = Back.InOut(inputTime);
                return ease;
            }


            if (dopeEasingType == DopeEasingType.EaseInBounce)
            {
                float ease = Bounce.In(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutBounce)
            {
                float ease = Bounce.Out(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutBounce)
            {
                float ease = Bounce.InOut(inputTime);
                return ease;
            }


            if (dopeEasingType == DopeEasingType.EaseInCirc)
            {
                float ease = Circular.In(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutCirc)
            {
                float ease = Circular.Out(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutCirc)
            {
                float ease = Circular.InOut(inputTime);
                return ease;
            }


            if (dopeEasingType == DopeEasingType.EaseInCubic)
            {
                float ease = Cubic.In(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutCubic)
            {
                float ease = Cubic.Out(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutCubic)
            {
                float ease = Cubic.InOut(inputTime);
                return ease;
            }


            if (dopeEasingType == DopeEasingType.EaseInElastic)
            {
                float ease = Elastic.In(inputTime, smooth, spring);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutElastic)
            {
                float ease = Elastic.Out(inputTime, smooth, spring);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutElastic)
            {
                float ease = Elastic.InOut(inputTime, smooth, spring);
                return ease;
            }


            if (dopeEasingType == DopeEasingType.EaseInExpo)
            {
                float ease = Exponential.In(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutExpo)
            {
                float ease = Exponential.Out(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutExpo)
            {
                float ease = Exponential.InOut(inputTime);
                return ease;
            }


            if (dopeEasingType == DopeEasingType.EaseInSine)
            {
                float ease = Sinusoidal.In(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseOutSine)
            {
                float ease = Sinusoidal.Out(inputTime);
                return ease;
            }

            if (dopeEasingType == DopeEasingType.EaseInOutSine)
            {
                float ease = Sinusoidal.InOut(inputTime);
                return ease;
            }

            #region Visually Duplicate Ease
            //if (dopeEasingType == DopeEasingType.EaseInQuad)
            //{
            //    float ease = Quadratic.In(inputTime);
            //    return ease;
            //}

            //if (dopeEasingType == DopeEasingType.EaseOutQuad)
            //{
            //    float ease = Quadratic.Out(inputTime);
            //    return ease;
            //}

            //if (dopeEasingType == DopeEasingType.EaseInOutQuad)
            //{
            //    float ease = Quadratic.InOut(inputTime);
            //    return ease;
            //}


            //if (dopeEasingType == DopeEasingType.EaseInQuart)
            //{
            //    float ease = Quartic.In(inputTime);
            //    return ease;
            //}

            //if (dopeEasingType == DopeEasingType.EaseOutQuart)
            //{
            //    float ease = Quartic.Out(inputTime);
            //    return ease;
            //}

            //if (dopeEasingType == DopeEasingType.EaseInOutQuart)
            //{
            //    float ease = Quartic.InOut(inputTime);
            //    return ease;
            //}


            //if (dopeEasingType == DopeEasingType.EaseInQuint)
            //{
            //    float ease = Quintic.In(inputTime);
            //    return ease;
            //}

            //if (dopeEasingType == DopeEasingType.EaseOutQuint)
            //{
            //    float ease = Quintic.Out(inputTime);
            //    return ease;
            //}

            //if (dopeEasingType == DopeEasingType.EaseInOutQuint)
            //{
            //    float ease = Quintic.InOut(inputTime);
            //    return ease;
            //}
            #endregion

            return 0f;
        }


    }
}