using Ludiq;
using Bolt;
using Unity.Notifications.Android;

[UnitSurtitle("Dope Notifications Android")]
[UnitTitle("Schedule Notification")]
[UnitCategory("DopeNotifications/Android")]
[Inspectable]
public sealed class DopeAndroidScheduleNotification : Unit
{
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput enter;

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlOutput exit;

    [DoNotSerialize]
    public ValueInput Text;

    [DoNotSerialize]
    public ValueInput Title;

    [DoNotSerialize]
    public ValueInput DateTimeValue;

    [DoNotSerialize]
    public ValueInput ImportanceValue;

    [DoNotSerialize]
    [PortLabelHidden]
    [PortLabel("id")]
    public ValueOutput Notification;


    private Importance iV = Importance.High;
    private string title;
    private string text;
    private DopeAndroidNotify dope = new DopeAndroidNotify();
    private string dateTimeValue;

    private AndroidNotification notification;

    protected override void Definition()
    {
        enter = ControlInput("enter", (Flow flow) =>
        {
            dateTimeValue = flow.GetValue<string>(DateTimeValue);
            iV = flow.GetValue<Importance>(ImportanceValue);
            title = flow.GetValue<string>(Title);
            text = flow.GetValue<string>(Text);
            
            return exit;

        });


        exit = ControlOutput("exit");

        Title = ValueInput("Title", string.Empty);
        Text = ValueInput("Text", string.Empty);
        DateTimeValue = ValueInput("Date and Time", string.Empty);
        ImportanceValue = ValueInput("Importance", Importance.High);

        Notification = ValueOutput<int>("Notification", (Flow flow) => { return dope.InvokeAndroidNotification(title, text, iV, dateTimeValue); });

        Succession(enter, exit);
    }

}
