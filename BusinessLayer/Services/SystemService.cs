using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services;

public class SystemService : ISystemService
{
    private readonly IConfiguration _configuration;

    public SystemService(IConfiguration configuration) => _configuration = configuration;

    public async Task<IDictionary<string, string>> GetEnvironmentInfoAsync()
    {
        // VULNERABILITÀ: Security Misconfiguration (OWASP A05)
        // Esposizione totale dei segreti di configurazione (DB string, JWT keys, etc.)
        return await Task.Run(() =>
        {
            var info = new Dictionary<string, string>();

            foreach (var config in _configuration.AsEnumerable())
            {
                if (config.Value != null)
                {
                    info[config.Key] = config.Value;
                }
            }

            return info;
        });
    }
}