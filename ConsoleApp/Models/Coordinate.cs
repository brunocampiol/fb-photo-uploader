using System.Runtime.InteropServices;

namespace ConsoleApp.Models;

[StructLayout(LayoutKind.Sequential)]
public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}