namespace BusinessLayer.Interfaces;

public interface IExternalService
{
    Task<string> ProxyRequest(string url);
}