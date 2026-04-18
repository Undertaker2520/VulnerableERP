namespace BusinessLayer.Interfaces;

public interface ISystemService
{
    Task<IDictionary<string, string>> GetEnvironmentInfoAsync();
}