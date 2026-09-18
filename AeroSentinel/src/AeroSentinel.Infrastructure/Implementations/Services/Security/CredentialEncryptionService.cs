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
        var hexadecimalKey = Convert.ToHexString(verificationKey);
        return _protector.Protect(hexadecimalKey);
    }
 
    public byte[] Decrypt(string encryptedValue)
    {
        var hexadecimalKey = _protector.Unprotect(encryptedValue);

        return Convert.FromHexString(hexadecimalKey);
        
       
    }
}