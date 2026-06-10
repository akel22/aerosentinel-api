public sealed class InvalidCallsignException
    : DomainException
{
    public InvalidCallsignException(
        string? callsign,
        string reason)
        : base($"Invalid callsign '{callsign}'. {reason}")
    {
    }
}