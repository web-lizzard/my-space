namespace MySpot.Api.ValueObjects;
public sealed record Date
{
    public DateTimeOffset Value { get; }

    public Date(DateTimeOffset date)
    {
        Value = date;
    }

    public Date AddDays(int days) => new(Value.AddDays(days));

    public static implicit operator DateTimeOffset(Date date) => date.Value;
    public static implicit operator Date(DateTimeOffset date) => new(date);
    public static bool operator <(Date date1, Date date2) => date1.Value < date2.Value;
    public static bool operator >(Date date1, Date date2) => date1.Value > date2.Value;
    public static bool operator >=(Date date1, Date date2) => date1.Value >= date2.Value;
    public static bool operator <=(Date date1, Date date2) => date1.Value <= date2.Value;
    public static Date Now() => new(DateTimeOffset.Now);




}