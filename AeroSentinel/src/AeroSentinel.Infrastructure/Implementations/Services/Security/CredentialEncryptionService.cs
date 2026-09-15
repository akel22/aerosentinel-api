using Microsoft.AspNetCore.DataProtection;

namespace AeroSentinel.Infrastructure.Implementations.Services.Security;

public sealed class CredentialEncryptionService : ICredentialEncryptionService
{
    private readonly IDataProtector _protector;

    public CredentialEncryptionService(IDataProtectionProvider provider)
    {
        // Purpose string scopes this protector — never reuse it for unrelated data
        _protector = provider.CreateProtector("AircraftCredential.VerificationKey");
    }

    public string Encrypt(byte[] verificationKey)
    {
        var base64Key = Convert.ToBase64String(verificationKey);
        return _protector.Protect(base64Key);
    }

    public byte[] Decrypt(string encryptedValue)
    {
        var base64Key = _protector.Unprotect(encryptedValue);
        return Convert.FromBase64String(base64Key);
    }
}