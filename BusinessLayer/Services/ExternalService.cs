using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.Invoice;
using Common.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services;

public class ExternalService : IExternalService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ExternalService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<string> ProxyRequest(string url)
    {
        //VULNERABILITA: SSRF (Server-Side Request Forgery)
        //Server prende URL da utente e lo chiama direttamente.
        //Non ho "whitelist" di URL, quindi un utente malintenzionato può fare richieste interne al server o a servizi interni.
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