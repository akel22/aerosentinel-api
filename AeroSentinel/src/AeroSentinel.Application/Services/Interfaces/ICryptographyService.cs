public interface ICryptographyService
{
   
    string ComputeHashAsync(string input, CancellationToken cancellationToken = default);

}