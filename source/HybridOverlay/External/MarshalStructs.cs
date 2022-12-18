namespace HybridOverlay.External;

[StructLayout(LayoutKind.Sequential)]
public struct Rect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}

[StructLayout(LayoutKind.Sequential)]
public struct Point
{
    public int X;
    public int Y;
}

public enum WindowShowState : int
{
#pragma warning disable CA1069 // Enums values should not be duplicated
    SW_HIDE = 0,
    SW_SHOWNORMAL = 1,
    SW_NORMAL = 1,
    SW_SHOWMINIMIZED = 2,
    SW_SHOWMAXIMIZED = 3,
    SW_MAXIMIZE = 3,
    SW_SHOWNOACTIVATE = 4,
    SW_SHOW = 5,
    SW_MINIMIZE = 6,
    SW_SHOWMINNOACTIVE = 7,
    SW_SHOWNA = 8,
    SW_RESTORE = 9,
    SW_SHOWDEFAULT = 10,
    SW_FORCEMINIMIZE = 11,
#pragma warning restore CA1069 // Enums values should not be duplicated
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct MonitorInfo
{
    public int Size;
    public Rect Monitor;
    public Rect WorkArea;
    public uint Flags;

    public MonitorInfo() => Size = Marshal.SizeOf(typeof(MonitorInfo));
}

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct WindowPlacementData
{
    /// <summary>
    /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
    /// <para>
    /// GetWindowPlacement and SetWindowPlacement fail if this member is not set correctly.
    /// </para>
    /// </summary>
    public int Length;

    public int Flags;

    public WindowShowState ShowCmd;

    public Point MinPosition;
    public Point MaxPosition;

    /// <summary>
    /// The window's coordinates when the window is in the restored position.
    /// </summary>
    public Rect NormalPosition;

    public WindowPlacementData() => Length = Marshal.SizeOf(typeof(WindowPlacementData));
}
