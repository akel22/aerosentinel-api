namespace AeroSentinel.Domain.Exceptions;
public sealed class SignatureException
    : DomainException
{
    public SignatureException(
        string? signature,
        string reason)
        : base($"Signature format invalid '{signature}'. {reason}")
    {
    }
}