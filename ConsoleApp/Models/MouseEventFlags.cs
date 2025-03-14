namespace ConsoleApp.Models;

[Flags]
public enum MouseEventFlags : uint
{
    LEFTDOWN = 0x0002,
    LEFTUP = 0x0004,
    RIGHTDOWN = 0x0008,
    RIGHTUP = 0x0010,
    MIDDLEDOWN = 0x0020,
    MIDDLEUP = 0x0040,
    WHEEL = 0x0800,
    HWHEEL = 0x01000,
    ABSOLUTE = 0x8000
}