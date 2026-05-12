#if DOPETOOLS_HAS_UNITY_ADS
using Unity.VisualScripting;
using System;
using UnityEngine.Advertisements;

namespace Dopetools.DopeAds
{
    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Initialize Ads")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsInitialize : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput androidGameId { get; private set; }
        [DoNotSerialize] public ValueInput iosGameId { get; private set; }
        [DoNotSerialize] public ValueInput testMode { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            androidGameId = ValueInput<string>(nameof(androidGameId), string.Empty);
            iosGameId = ValueInput<string>(nameof(iosGameId), string.Empty);
            testMode = ValueInput<bool>(nameof(testMode), true);

            Requirement(androidGameId, enter);
            Requirement(iosGameId, enter);
            Requirement(testMode, enter);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            DopeAds.GetOrCreate().Initialize(
                flow.GetValue<string>(androidGameId),
                flow.GetValue<string>(iosGameId),
                flow.GetValue<bool>(testMode));
            return exit;
        }
    }

    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Load Ad")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsLoadAd : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput androidAdUnitId { get; private set; }
        [DoNotSerialize] public ValueInput iosAdUnitId { get; private set; }
        [DoNotSerialize] public ValueOutput adUnitId { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            androidAdUnitId = ValueInput<string>(nameof(androidAdUnitId), "Interstitial_Android");
            iosAdUnitId = ValueInput<string>(nameof(iosAdUnitId), "Interstitial_iOS");
            adUnitId = ValueOutput<string>(nameof(adUnitId));

            Requirement(androidAdUnitId, enter);
            Requirement(iosAdUnitId, enter);
            Assignment(enter, adUnitId);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            var resolvedAdUnitId = DopeAds.ResolvePlatformId(
                flow.GetValue<string>(androidAdUnitId),
                flow.GetValue<string>(iosAdUnitId));
            flow.SetValue(adUnitId, resolvedAdUnitId);
            DopeAds.GetOrCreate().LoadAd(resolvedAdUnitId);
            return exit;
        }
    }

    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Show Ad")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsShowAd : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput androidAdUnitId { get; private set; }
        [DoNotSerialize] public ValueInput iosAdUnitId { get; private set; }
        [DoNotSerialize] public ValueOutput adUnitId { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            androidAdUnitId = ValueInput<string>(nameof(androidAdUnitId), "Interstitial_Android");
            iosAdUnitId = ValueInput<string>(nameof(iosAdUnitId), "Interstitial_iOS");
            adUnitId = ValueOutput<string>(nameof(adUnitId));

            Requirement(androidAdUnitId, enter);
            Requirement(iosAdUnitId, enter);
            Assignment(enter, adUnitId);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            var resolvedAdUnitId = DopeAds.ResolvePlatformId(
                flow.GetValue<string>(androidAdUnitId),
                flow.GetValue<string>(iosAdUnitId));
            flow.SetValue(adUnitId, resolvedAdUnitId);
            DopeAds.GetOrCreate().ShowAd(resolvedAdUnitId);
            return exit;
        }
    }

    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Is Ad Loaded")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsIsAdLoaded : Unit
    {
        [DoNotSerialize] public ValueInput androidAdUnitId { get; private set; }
        [DoNotSerialize] public ValueInput iosAdUnitId { get; private set; }
        [DoNotSerialize] public ValueOutput isLoaded { get; private set; }

        protected override void Definition()
        {
            androidAdUnitId = ValueInput<string>(nameof(androidAdUnitId), "Interstitial_Android");
            iosAdUnitId = ValueInput<string>(nameof(iosAdUnitId), "Interstitial_iOS");
            isLoaded = ValueOutput<bool>(nameof(isLoaded), IsLoaded);

            Requirement(androidAdUnitId, isLoaded);
            Requirement(iosAdUnitId, isLoaded);
        }

        private bool IsLoaded(Flow flow)
        {
            return DopeAds.IsAdLoaded(DopeAds.ResolvePlatformId(
                flow.GetValue<string>(androidAdUnitId),
                flow.GetValue<string>(iosAdUnitId)));
        }
    }

    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Show Banner")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsShowBanner : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput androidAdUnitId { get; private set; }
        [DoNotSerialize] public ValueInput iosAdUnitId { get; private set; }
        [DoNotSerialize] public ValueInput position { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            androidAdUnitId = ValueInput<string>(nameof(androidAdUnitId), "Banner_Android");
            iosAdUnitId = ValueInput<string>(nameof(iosAdUnitId), "Banner_iOS");
            position = ValueInput(nameof(position), BannerPosition.BOTTOM_CENTER);

            Requirement(androidAdUnitId, enter);
            Requirement(iosAdUnitId, enter);
            Requirement(position, enter);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            var resolvedAdUnitId = DopeAds.ResolvePlatformId(
                flow.GetValue<string>(androidAdUnitId),
                flow.GetValue<string>(iosAdUnitId));
            DopeAds.GetOrCreate().ShowBanner(resolvedAdUnitId, flow.GetValue<BannerPosition>(position));
            return exit;
        }
    }

    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Hide Banner")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsHideBanner : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput destroy { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            destroy = ValueInput<bool>(nameof(destroy), false);

            Requirement(destroy, enter);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            DopeAds.GetOrCreate().HideBanner(flow.GetValue<bool>(destroy));
            return exit;
        }
    }

    [UnitSurtitle("Dope Ads")]
    [UnitTitle("Ads Events")]
    [UnitCategory("DopeTools/Ads")]
    public sealed class DopeAdsEvents : Unit, IGraphElementWithData, IGraphEventListener
    {
        public sealed class Data : IGraphElementData
        {
            public bool isListening;
            public Delegate callback;
            public string eventName = string.Empty;
            public string argument = string.Empty;
        }

        [DoNotSerialize] public ControlOutput initializationComplete { get; private set; }
        [DoNotSerialize] public ControlOutput initializationFailed { get; private set; }
        [DoNotSerialize] public ControlOutput adLoaded { get; private set; }
        [DoNotSerialize] public ControlOutput loadFailed { get; private set; }
        [DoNotSerialize] public ControlOutput showStart { get; private set; }
        [DoNotSerialize] public ControlOutput showClick { get; private set; }
        [DoNotSerialize] public ControlOutput rewardedComplete { get; private set; }
        [DoNotSerialize] public ControlOutput interstitialComplete { get; private set; }
        [DoNotSerialize] public ControlOutput showSkipped { get; private set; }
        [DoNotSerialize] public ControlOutput error { get; private set; }
        [DoNotSerialize] public ControlOutput showFailed { get; private set; }
        [DoNotSerialize] public ControlOutput bannerLoaded { get; private set; }
        [DoNotSerialize] public ControlOutput bannerLoadFailed { get; private set; }
        [DoNotSerialize] public ControlOutput bannerShown { get; private set; }
        [DoNotSerialize] public ControlOutput bannerClicked { get; private set; }
        [DoNotSerialize] public ControlOutput bannerHidden { get; private set; }
        [DoNotSerialize] public ValueOutput eventName { get; private set; }
        [DoNotSerialize] public ValueOutput argument { get; private set; }

        protected override void Definition()
        {
            isControlRoot = true;

            initializationComplete = ControlOutput(nameof(initializationComplete));
            initializationFailed = ControlOutput(nameof(initializationFailed));
            adLoaded = ControlOutput(nameof(adLoaded));
            loadFailed = ControlOutput(nameof(loadFailed));
            showStart = ControlOutput(nameof(showStart));
            showClick = ControlOutput(nameof(showClick));
            rewardedComplete = ControlOutput(nameof(rewardedComplete));
            interstitialComplete = ControlOutput(nameof(interstitialComplete));
            showSkipped = ControlOutput(nameof(showSkipped));
            error = ControlOutput(nameof(error));
            showFailed = ControlOutput(nameof(showFailed));
            bannerLoaded = ControlOutput(nameof(bannerLoaded));
            bannerLoadFailed = ControlOutput(nameof(bannerLoadFailed));
            bannerShown = ControlOutput(nameof(bannerShown));
            bannerClicked = ControlOutput(nameof(bannerClicked));
            bannerHidden = ControlOutput(nameof(bannerHidden));
            eventName = ValueOutput<string>(nameof(eventName), GetEventName);
            argument = ValueOutput<string>(nameof(argument), GetArgument);
        }

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
            Action<DopeAdsCallbackEventArgs> callback = args => Trigger(reference, args);
            DopeAds.CallbackReceived += callback;
            data.callback = callback;
            data.isListening = true;
        }

        public void StopListening(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);

            if (!data.isListening)
            {
                return;
            }

            DopeAds.CallbackReceived -= (Action<DopeAdsCallbackEventArgs>)data.callback;
            data.callback = null;
            data.isListening = false;
        }

        public bool IsListening(GraphPointer pointer)
        {
            return pointer.GetElementData<Data>(this).isListening;
        }

        private string GetEventName(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).eventName;
        }

        private string GetArgument(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).argument;
        }

        private void Trigger(GraphReference reference, DopeAdsCallbackEventArgs args)
        {
            using (var flow = Flow.New(reference))
            {
                var data = flow.stack.GetElementData<Data>(this);
                data.eventName = args.EventName;
                data.argument = args.Argument;

                var output = GetOutput(args.EventName);
                if (output != null)
                {
                    flow.Invoke(output);
                }
            }
        }

        private ControlOutput GetOutput(string name)
        {
            switch (name)
            {
                case DopeAds.EventInitializationComplete: return initializationComplete;
                case DopeAds.EventInitializationFailed: return initializationFailed;
                case DopeAds.EventAdLoaded: return adLoaded;
                case DopeAds.EventLoadFailed: return loadFailed;
                case DopeAds.EventShowStart: return showStart;
                case DopeAds.EventShowClick: return showClick;
                case DopeAds.EventRewardedComplete: return rewardedComplete;
                case DopeAds.EventInterstitialComplete: return interstitialComplete;
                case DopeAds.EventShowSkipped: return showSkipped;
                case DopeAds.EventError: return error;
                case DopeAds.EventShowFailed: return showFailed;
                case DopeAds.EventBannerLoaded: return bannerLoaded;
                case DopeAds.EventBannerLoadFailed: return bannerLoadFailed;
                case DopeAds.EventBannerShown: return bannerShown;
                case DopeAds.EventBannerClicked: return bannerClicked;
                case DopeAds.EventBannerHidden: return bannerHidden;
                default: return null;
            }
        }
    }

}
#endif
