public sealed class InvalidVerificationKeyException
    : DomainException
{
    public InvalidVerificationKeyException(
        byte[]? sharedKey,
        string reason)
        : base($"Invalid verification key: ['{sharedKey}']. {reason}")
    {
    }
}