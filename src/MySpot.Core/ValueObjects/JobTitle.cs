namespace MySpot.Core.ValueObjects;
public sealed record JobTitle
{
    public string Value { get; }

    public const string Employee = nameof(Employee);
    public const string Manager = nameof(Manager);
    public const string Boss = nameof(Boss);

    private JobTitle(string value)
    {
        Value = value;
    }


    public static implicit operator string(JobTitle title) => title.Value;
    public static implicit operator JobTitle(string title) => new(title);


}