#if DOPETOOLS_HAS_UNITY_ADS
using Bolt;
using Ludiq;
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
}
#endif
