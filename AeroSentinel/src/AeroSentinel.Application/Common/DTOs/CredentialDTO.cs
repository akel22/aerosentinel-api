namespace AeroSentinel.Application.Common.DTOs;

    public sealed record CredentialDTO
    (
         Guid CredentialId, string ICAO24, byte[] VerificationKey,
         CredentialStatus Status, DateTime CreatedUtc
    );