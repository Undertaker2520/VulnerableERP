using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.Invoice;
using Common.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services;

public class SystemService : ISystemService
{
    private readonly IConfiguration _configuration;

    public SystemService(IConfiguration configuration) => _configuration = configuration;
    public IDictionary<string, string> GetEnvironmentInfo()
    {
        //VULNERABILITA: Security Misconfiguration
        //espongo l'intero set di configurazioni
        var info = new Dictionary<string, string>();

        foreach (var config in _configuration.AsEnumerable())
        {
            {
                if (config.Value != null)
                {
                    info[config.Key] = config.Value;
                }
            }
        }

        return info;
    }
}