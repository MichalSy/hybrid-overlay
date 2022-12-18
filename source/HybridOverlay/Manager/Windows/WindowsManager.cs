using HybridOverlay.External;
using static HybridOverlay.External.ExternalUser32;

namespace HybridOverlay.Manager.Windows;
public class WindowsManager : IWindowsManager
{
    public MonitorInfo? getScreenInformationFromMousePosition()
    {
        if (!GetCursorPos(out External.Point mousePosition))
        {
            return null;
        }

        // Detect monitor by mouse position
        IntPtr monitorHandle = MonitorFromPoint(mousePosition, 2);
        if (monitorHandle == IntPtr.Zero)
        {
            return null;
        }

        MonitorInfo monitorInfo = new();
        if (!GetMonitorInfo(monitorHandle, ref monitorInfo))
        {
            return null;
        }

        return monitorInfo;
    }

    public void BringTaskbarsToFront()
    {
        var mainTaskbar = FindWindow("Shell_TrayWnd", null);
        if (mainTaskbar != 0)
        {
            BringWindowToTop(mainTaskbar);
        }

        var secondTaskbar = FindWindow("Shell_SecondaryTrayWnd", null);
        if (secondTaskbar != 0)
        {
            BringWindowToTop(secondTaskbar);
        }
    }

    public void SetWindowPlacemnet(Window window, int left, int top, int width, int height)
    {
        var windowHandle = new WindowInteropHelper(window).Handle;

        WindowPlacementData placement = new();
        GetWindowPlacement(windowHandle, ref placement);

        placement.NormalPosition.Top = top;
        placement.NormalPosition.Left = left;
        placement.NormalPosition.Right = left + width;
        placement.NormalPosition.Bottom = top + height;

        SetWindowPlacement(windowHandle, ref placement);
    }
}
