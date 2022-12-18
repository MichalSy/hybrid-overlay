using HybridOverlay.External;

namespace HybridOverlay.Manager.Windows;

public interface IWindowsManager
{
    void BringTaskbarsToFront();
    MonitorInfo? getScreenInformationFromMousePosition();
    void SetWindowPlacemnet(Window window, int left, int top, int width, int height);
}