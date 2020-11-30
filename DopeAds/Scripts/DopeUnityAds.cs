using UnityEngine;
using Ludiq;
using Bolt;
using UnityEngine.Advertisements;

namespace Dopetools.DopeAds
{

    public class DopeUnityAds : MachineEventUnit<EmptyEventArgs>, IGraphElementWithData, IUnityAdsListener
    {
        public new class Data : EventUnit<EmptyEventArgs>.Data
        {
            public string placementId;
        }

        public override IGraphElementData CreateData()
        {
            return new Data();
        }

        protected override string hookName => EventHooks.Update;

        [DoNotSerialize]
        [PortLabel("PlacementId")]
        public ValueInput placementIdValue { get; private set; }


        //[DoNotSerialize]
        //public int shouldTriggerNextUpdateTicker;


        //protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
        //{
        //    var data = flow.stack.GetElementData<Data>(this);

        //    if (!data.isListening)
        //    {
        //        return false;
        //    }

        //    if (shouldTriggerNextUpdateTicker > data.LastObservedUpdateTicker)
        //    {
        //        data.LastObservedUpdateTicker = shouldTriggerNextUpdateTicker;
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        protected override void Definition()
        {
            base.Definition();
            placementIdValue = ValueInput(nameof(placementIdValue), string.Empty);

        }

        public override void StartListening(GraphStack stack)
        {
            base.StartListening(stack);

            var data = stack.GetElementData<Data>(this);
        }

        protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
        {
            var data = flow.stack.GetElementData<Data>(this);

            if (!data.isListening) { return false; }
            else if (start) { return true; }
            else if (ready) { return true; }
            else if (finish) { return true; }
            else if (error) { return true; }
            return false;

        }

        public bool error;
        public bool finish;
        public bool start;
        public bool ready;

        public void OnUnityAdsDidError(string message)
        {
            error = true;
        }

        public void OnUnityAdsDidFinish(string placementIdValue, ShowResult showResult)
        {
            finish = true;
        }

        public void OnUnityAdsDidStart(string placementIdValue)
        {
            start = true;
        }

        public void OnUnityAdsReady(string placementIdValue)
        {
            ready = true;
        }
    }
}