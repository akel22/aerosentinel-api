namespace AeroSentinel.Application.Services.Interfaces;
public interface IReplayProtectionService
{
    public void ValidateSequence(DateTime timeStamp, long incomingSequence, long? lastAcceptedSequence);
    
    

}