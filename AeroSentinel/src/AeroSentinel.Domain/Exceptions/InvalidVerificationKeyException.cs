public sealed class InvalidVerificationKeyException
    : DomainException
{
    public InvalidVerificationKeyException(
        string? aircraftId,
        string reason)
        : base($"Invalid verification key for aircraft '{aircraftId}'. {reason}")
    {
    }
}