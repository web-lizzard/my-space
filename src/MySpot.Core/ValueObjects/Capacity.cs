
using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;


public sealed record Capacity
{
    public Capacity(int value)
    {
        if (value is < 0 or > 4)
        {
            throw new InvalidCapacityException(value);
        }

        Value = value;
    }

    public int Value { get; }


    public static implicit operator int(Capacity capacity) => capacity.Value;
    public static implicit operator Capacity(int value) => new(value);
}

