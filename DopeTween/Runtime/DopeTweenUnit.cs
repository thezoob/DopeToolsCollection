#if DOPETOOLS_BOLT_LEGACY
using Bolt;
using Ludiq;
#else
using Unity.VisualScripting;
#endif
using System;
using UnityEngine;

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

        [DoNotSerialize]
        public ControlInput pause { get; private set; }

        [DoNotSerialize]
        public ControlInput resume { get; private set; }

        [DoNotSerialize]
        public ControlInput cancel { get; private set; }

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

        [DoNotSerialize]
        public ControlOutput paused { get; private set; }

        [DoNotSerialize]
        public ControlOutput resumed { get; private set; }

        [DoNotSerialize]
        public ControlOutput cancelled { get; private set; }

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


        [DoNotSerialize]
        public ValueOutput elapsedSeconds { get; private set; }

        [DoNotSerialize]
        public ValueOutput elapsedRatio { get; private set; }

        [DoNotSerialize]
        public ValueOutput remainingSeconds { get; private set; }

        [DoNotSerialize]
        public ValueOutput remainingRatio { get; private set; }

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
            pause = ControlInput(nameof(pause), Pause);
            resume = ControlInput(nameof(resume), Resume);
            cancel = ControlInput(nameof(cancel), Cancel);

            duration = ValueInput(nameof(duration), 1f);
            unscaledTime = ValueInput(nameof(unscaledTime), false);

            started = ControlOutput(nameof(started));
            paused = ControlOutput(nameof(paused));
            resumed = ControlOutput(nameof(resumed));
            cancelled = ControlOutput(nameof(cancelled));
            tick = ControlOutput(nameof(tick));
            completed = ControlOutput(nameof(completed));

            ResultValue = ValueOutput(dopeTweenInputValueType == DopeTweenInputValueType.Color ? typeof(Color) : 
                dopeTweenInputValueType == DopeTweenInputValueType.Float ? typeof(float) : 
                dopeTweenInputValueType == DopeTweenInputValueType.String ? typeof(string) : 
                dopeTweenInputValueType == DopeTweenInputValueType.Vector2 ? typeof(Vector2) : 
                dopeTweenInputValueType == DopeTweenInputValueType.Vector3 ? typeof(Vector3) :
                dopeTweenInputValueType == DopeTweenInputValueType.Quaternion ? typeof(Quaternion) : null, "ResultValue");

            elapsedSeconds = ValueOutput<float>(nameof(elapsedSeconds), GetElapsedSeconds);
            elapsedRatio = ValueOutput<float>(nameof(elapsedRatio), GetElapsedRatio);
            remainingSeconds = ValueOutput<float>(nameof(remainingSeconds), GetRemainingSeconds);
            remainingRatio = ValueOutput<float>(nameof(remainingRatio), GetRemainingRatio);

            Succession(start, started);
            Succession(start, tick);
            Succession(start, completed);
            Succession(pause, paused);
            Succession(resume, resumed);
            Succession(cancel, cancelled);
            Requirement(A, start);
            if (dopeTweenInputValueType != DopeTweenInputValueType.String) { Requirement(B, start); }
            Requirement(DopeEasingTypeValue, start);
            Requirement(duration, start);
            Requirement(unscaledTime, start);
            Assignment(start, ResultValue);
            Assignment(start, elapsedSeconds);
            Assignment(start, elapsedRatio);
            Assignment(start, remainingSeconds);
            Assignment(start, remainingRatio);

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

            else if (dopeTweenInputValueType == DopeTweenInputValueType.String) { data.A = flow.GetValue<string>(A); data.B = null; }

            else if (dopeTweenInputValueType == DopeTweenInputValueType.Quaternion) { data.A = flow.GetValue<Vector3>(A); data.B = flow.GetValue<Vector3>(B); }

            AssignMetrics(flow, data);

            return started;
        }

        private ControlOutput Pause(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (data.active)
            {
                data.paused = true;
            }

            return paused;
        }

        private ControlOutput Resume(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (data.active)
            {
                data.paused = false;
            }

            return resumed;
        }

        private ControlOutput Cancel(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);

            data.active = false;
            data.paused = false;

            return cancelled;
        }

        private float GetElapsedSeconds(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).elapsed;
        }

        private float GetElapsedRatio(Flow flow)
        {
            return ElapsedRatio(flow.stack.GetElementData<Data>(this));
        }

        private float GetRemainingSeconds(Flow flow)
        {
            var data = flow.stack.GetElementData<Data>(this);
            return Mathf.Max(0f, data.duration - data.elapsed);
        }

        private float GetRemainingRatio(Flow flow)
        {
            return 1f - ElapsedRatio(flow.stack.GetElementData<Data>(this));
        }

        private static float ElapsedRatio(Data data)
        {
            if (data.duration <= 0f)
            {
                return 1f;
            }

            return Mathf.Clamp01(data.elapsed / data.duration);
        }

        private void AssignMetrics(Flow flow, Data data)
        {
            data.dopeEasingType = flow.GetValue<DopeEasingType>(DopeEasingTypeValue);

            var ratio = ElapsedRatio(data);
            var easedValue = EasedValue(data.dopeEasingType, ratio);
            var value = dopeTweenInputValueType == DopeTweenInputValueType.Float ? Mathf.LerpUnclamped((float)data.A, (float)data.B, easedValue) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Vector3 ? Vector3.LerpUnclamped((Vector3)data.A, (Vector3)data.B, easedValue) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Quaternion ? Quaternion.LerpUnclamped(Quaternion.Euler((Vector3)data.A), Quaternion.Euler((Vector3)data.B), easedValue) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Vector2 ? Vector2.LerpUnclamped((Vector2)data.A, (Vector2)data.B, easedValue) :
                        dopeTweenInputValueType == DopeTweenInputValueType.Color ? Color.LerpUnclamped((Color)data.A, (Color)data.B, easedValue) :
                        dopeTweenInputValueType == DopeTweenInputValueType.String ? (string)data.A : data.A;

            data.resultValue = value;
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

        private float EasedValue(DopeEasingType easingType, float inputTime)
        {
            switch (easingType)
            {
                case DopeEasingType.Linear:
                    return Linear.Lin(inputTime);
                case DopeEasingType.EaseInBack:
                    return Back.In(inputTime);
                case DopeEasingType.EaseOutBack:
                    return Back.Out(inputTime);
                case DopeEasingType.EaseInOutBack:
                    return Back.InOut(inputTime);
                case DopeEasingType.EaseInBounce:
                    return Bounce.In(inputTime);
                case DopeEasingType.EaseOutBounce:
                    return Bounce.Out(inputTime);
                case DopeEasingType.EaseInOutBounce:
                    return Bounce.InOut(inputTime);
                case DopeEasingType.EaseInCirc:
                    return Circular.In(inputTime);
                case DopeEasingType.EaseOutCirc:
                    return Circular.Out(inputTime);
                case DopeEasingType.EaseInOutCirc:
                    return Circular.InOut(inputTime);
                case DopeEasingType.EaseInCubic:
                    return Cubic.In(inputTime);
                case DopeEasingType.EaseOutCubic:
                    return Cubic.Out(inputTime);
                case DopeEasingType.EaseInOutCubic:
                    return Cubic.InOut(inputTime);
                case DopeEasingType.EaseInElastic:
                    return Elastic.In(inputTime, smooth, spring);
                case DopeEasingType.EaseOutElastic:
                    return Elastic.Out(inputTime, smooth, spring);
                case DopeEasingType.EaseInOutElastic:
                    return Elastic.InOut(inputTime, smooth, spring);
                case DopeEasingType.EaseInExpo:
                    return Exponential.In(inputTime);
                case DopeEasingType.EaseOutExpo:
                    return Exponential.Out(inputTime);
                case DopeEasingType.EaseInOutExpo:
                    return Exponential.InOut(inputTime);
                case DopeEasingType.EaseInSine:
                    return Sinusoidal.In(inputTime);
                case DopeEasingType.EaseOutSine:
                    return Sinusoidal.Out(inputTime);
                case DopeEasingType.EaseInOutSine:
                    return Sinusoidal.InOut(inputTime);
                default:
                    return 0f;
            }
        }


    }
}