namespace Common.DTOs;

public class ImportRequest
{
    // L'attaccante invierà un JSON serializzato dentro questa stringa
    public string JsonConfig { get; set; } = string.Empty;
}