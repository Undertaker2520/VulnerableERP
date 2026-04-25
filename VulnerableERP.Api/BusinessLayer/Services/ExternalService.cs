using BusinessLayer.Interfaces;
namespace BusinessLayer.Services;

public class ExternalService : IExternalService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ExternalService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<string> ProxyRequestAsync(string url)
    {
        // VULNERABILITÀ SSRF (Server-Side Request Forgery)
        // L'asincronia rende il proxy molto efficiente, permettendo all'attaccante
        // di scansionare centinaia di porte interne al secondo senza bloccare il server.
        var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"Errore durante la chiamata: {ex.Message}";
        }
    }
}