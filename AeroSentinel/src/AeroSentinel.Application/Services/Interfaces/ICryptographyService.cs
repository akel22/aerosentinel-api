namespace AeroSentinel.Application.Security;

public interface ICryptographyService
{
    public bool VerifyPayloadSignature(SendTelemetryCommand request, byte[] sharedSecretKey);
   
}