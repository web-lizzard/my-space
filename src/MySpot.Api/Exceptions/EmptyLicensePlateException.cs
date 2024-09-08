namespace MySpot.Api.Exceptions;

public sealed class EmptyLicensePlateException : CustomException
{
    public EmptyLicensePlateException() : base("License Plate is Empty")
    {
    }
}