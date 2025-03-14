using System.Runtime.InteropServices;

namespace ConsoleApp;

public static class ScreenService
{
    // Screen related constants and methods
    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    /// <summary>
    /// Gets the current screen width
    /// </summary>
    public static int ScreenWidth => GetSystemMetrics(SM_CXSCREEN);

    /// <summary>
    /// Gets the current screen height
    /// </summary>
    public static int ScreenHeight => GetSystemMetrics(SM_CYSCREEN);
}