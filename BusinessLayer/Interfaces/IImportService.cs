namespace BusinessLayer.Interfaces;

public interface IImportService
{
    Task<string> ImportSettingsAsync(string jsonConfig);
}