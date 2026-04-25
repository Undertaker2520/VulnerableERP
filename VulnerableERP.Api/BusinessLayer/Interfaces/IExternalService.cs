namespace BusinessLayer.Interfaces;

public interface IExternalService
{
    Task<string> ProxyRequestAsync(string url);
}