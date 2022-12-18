using HybridOverlay.Manager.Config.Models;

namespace HybridOverlay.Manager.Config;

public interface IConfigManager
{
    Task<AppConfig?> ReadConfigAsync();
}