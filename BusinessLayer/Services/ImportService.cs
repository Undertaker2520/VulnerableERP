using BusinessLayer.Interfaces;
using Newtonsoft.Json;

namespace BusinessLayer.Services;

public class ImportService : IImportService
{
    public async Task<string> ImportSettingsAsync(string jsonConfig)
    {
        // VULNERABILITÀ: Insecure Deserialization (OWASP A08)
        // TypeNameHandling.All è il "sacro graal" degli attaccanti .NET.
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        try
        {
            // Eseguiamo la deserializzazione. In un caso reale, qui potrebbe 
            // esserci un'attesa di I/O (es. lettura da un blob storage), 
            // per ora simuliamo il pattern async.
            object? deserializedData = await Task.Run(() => JsonConvert.DeserializeObject(jsonConfig, settings));

            return $"Configurazione caricata con successo: {deserializedData?.GetType().Name}";
        }
        catch (Exception ex)
        {
            // Nota: Restituire il messaggio dell'eccezione è un'altra piccola 
            // Information Disclosure che aiuta l'attaccante a fare il debug del payload.
            return $"Errore di importazione: {ex.Message}";
        }
    }
}