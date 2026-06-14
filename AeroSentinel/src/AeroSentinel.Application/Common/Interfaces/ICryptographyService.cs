public interface ICryptographyService
{
    byte[] HashSecretKey(string secretKey);
    bool VerifySecretKey(string secretKey, byte[] hashedKey);
}