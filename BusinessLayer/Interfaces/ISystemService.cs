namespace BusinessLayer.Interfaces;

public interface ISystemService
{
    IDictionary<string, string> GetEnvironmentInfo();
}