using System.IO;
using System.Text.Json;
using HybridOverlay.Manager.Config.Models;

namespace HybridOverlay.Manager.Config;
public class ConfigManager : IConfigManager
{
    private static readonly string _configFileName = "AppConfig.json";

    public async Task<AppConfig?> ReadConfigAsync()
    {
        if (!File.Exists(_configFileName))
            return default;

        var fileContent = await File.ReadAllTextAsync(_configFileName);
        return JsonSerializer.Deserialize<AppConfig>(fileContent);
    }
}
