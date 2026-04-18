using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.Invoice;
using Common.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BusinessLayer.Services;

public class ImportService : IImportService
{
    public string ImportSettings(string jsonConfig)
    {
        //VULNERABILITA': Insecure Deserialization
        // TypeNameHandling.All permette al JSON di definire quale tipo di oggetto creare.
        // Un attaccante può usare questo per forzare il caricamento di classi pericolose.
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        try
        {
            //il server prova a deserializzare "qualsiasi cosa" l'utente abbia inviato
            object? deserializedData = JsonConvert.DeserializeObject(jsonConfig, settings);
            return $"Configurazione caricata con successo: {deserializedData?.GetType().Name}";
        }
        catch (Exception ex)
        {
            return $"Errore di importazione: {ex.Message}";
        }
    }
}