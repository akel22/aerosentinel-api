namespace AeroSentinel.Application.Services.Interfaces
{
    public interface ICredentialEncryptionService
    {
        public string Encrypt(byte[] verificationKey);
        public byte[] Decrypt(string encryptedValue);

    }
}