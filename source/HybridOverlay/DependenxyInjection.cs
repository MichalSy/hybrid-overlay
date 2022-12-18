using HybridOverlay.Manager.Config;
using HybridOverlay.Manager.Windows;
using HybridOverlay.Windows.Main;

namespace HybridOverlay;
public static class DependenxyInjection
{
    public static void Configure(ServiceCollection services)
    {
        services.AddSingleton<IWindowsManager, WindowsManager>();
        services.AddSingleton<IConfigManager, ConfigManager>();

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
    }
}
