namespace AeroSentinel.Application.Security;

public interface ICryptographyService
{
    public bool VerifyPayloadSignature(RawPayloadDTO payload, byte[] sharedSecretKey);
   
}