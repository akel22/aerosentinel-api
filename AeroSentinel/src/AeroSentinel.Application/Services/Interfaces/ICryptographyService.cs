namespace AeroSentinel.Application.Services.Interfaces;

public interface ICryptographyService
{
    public bool VerifyPayloadSignature(RawPayloadDTO payload, byte[] sharedSecretKey);
   
}