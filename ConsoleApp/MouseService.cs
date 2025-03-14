using ConsoleApp.Models;
using System.Runtime.InteropServices;

namespace ConsoleApp;

public static class MouseService
{
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Coordinate lpPoint);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, int dwExtraInfo);

    /// <summary>
    /// Gets the current cursor position
    /// </summary>
    public static Coordinate CursorPosition
    {
        get
        {
            Coordinate point;
            GetCursorPos(out point);
            return point;
        }
    }

    /// <summary>
    /// Performs a human randomized time left-click at the current cursor position
    /// </summary>
    public static void LeftClickHumanRandom()
    {
        var random = new Random();
        var delay = random.Next(50, 200);

        // Simulate left mouse button down
        mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);

        // Wait for a randomized period to simulate human behavior
        Thread.Sleep(delay);

        // Simulate left mouse button up
        mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// Performs a left-click at the current cursor position
    /// </summary>
    /// <param name="delayBetweenActions">Delay between down and up actions in milliseconds</param>
    public static void LeftClick(int delayBetweenActions = 50)
    {
        // Simulate left mouse button down
        mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);

        // Wait for a short period to simulate human behavior
        Thread.Sleep(delayBetweenActions);

        // Simulate left mouse button up
        mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// Performs a human randomized time right-click at the current cursor position
    /// </summary>
    /// <param name="delayBetweenActions">Delay between down and up actions in milliseconds</param>
    public static void RightClickHumanRandom()
    {
        var random = new Random();
        var delay = random.Next(50, 200);

        // Simulate right mouse button down
        mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);

        // Wait for a short period to simulate human behavior
        Thread.Sleep(delay);

        // Simulate right mouse button up
        mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// Performs a right-click at the current cursor position
    /// </summary>
    /// <param name="delayBetweenActions">Delay between down and up actions in milliseconds</param>
    public static void RightClick(int delayBetweenActions = 50)
    {
        // Simulate right mouse button down
        mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);

        // Wait for a short period to simulate human behavior
        Thread.Sleep(delayBetweenActions);

        // Simulate right mouse button up
        mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// Scrolls the mouse wheel
    /// </summary>
    /// <param name="scrollAmount">Amount to scroll: positive values scroll up, negative values scroll down</param>
    /// <param name="speed">Speed factor: lower values = slower scrolling (default 1.0)</param>
    public static void Scroll(int scrollAmount, double speed = 1.0)
    {
        // In mouse_event, the wheel delta is 120 units per notch
        const int WHEEL_DELTA = 120;

        // Use a smaller increment per step for smoother scrolling
        // The smaller this value, the smoother but slower the scrolling
        int incrementSize = Math.Max(1, (int)(3 * speed));

        // Calculate base delay between scroll actions
        int baseDelay = (int)(100 / speed);

        // Scroll in increments to simulate natural scrolling
        int remainingAmount = Math.Abs(scrollAmount);
        int direction = scrollAmount > 0 ? 1 : -1;

        Random random = new();

        while (remainingAmount > 0)
        {
            // Determine how much to scroll in this step - keep it small for smoother scrolling
            int currentScroll = Math.Min(remainingAmount, incrementSize);
            remainingAmount -= currentScroll;

            // Execute the scroll action with a smaller delta for finer control
            mouse_event(
                (uint)MouseEventFlags.WHEEL,
                0,
                0,
                currentScroll * direction * WHEEL_DELTA,
                0
            );

            // Add a longer delay between scroll actions for more natural behavior
            if (remainingAmount > 0)
            {
                // Use a consistent random object and increase the delay range
                int delay = random.Next(baseDelay, baseDelay + 50);
                Thread.Sleep(delay);
            }
        }
    }

    /// <summary>
    /// Scrolls the mouse wheel up
    /// </summary>
    /// <param name="clicks">Number of scroll clicks (notches)</param>
    /// <param name="speed">Speed factor: lower values = slower scrolling (default 1.0)</param>
    public static void ScrollUp(int clicks, double speed = 1.0)
    {
        Scroll(clicks, speed);
    }

    /// <summary>
    /// Scrolls the mouse wheel down
    /// </summary>
    /// <param name="clicks">Number of scroll clicks (notches)</param>
    /// <param name="speed">Speed factor: lower values = slower scrolling (default 1.0)</param>
    public static void ScrollDown(int clicks, double speed = 1.0)
    {
        Scroll(-clicks, speed);
    }

    /// <summary>
    /// Sets the cursor position to the specified coordinates
    /// </summary>
    public static bool SetCursorPosition(int x, int y)
    {
        return SetCursorPos(x, y);
    }

    /// <summary>
    /// Moves the cursor in a square pattern
    /// </summary>
    public static void MoveCursorInSquare(int sideLength, int loops, int delay = 5)
    {
        for (int loop = 0; loop < loops; loop++)
        {
            Console.WriteLine($"Loop {loop + 1} of {loops}");

            // Move right
            for (int i = 0; i < sideLength; i++)
            {
                SetCursorPosition(CursorPosition.X + 1, CursorPosition.Y);
                Thread.Sleep(delay);
            }

            // Move down
            for (int i = 0; i < sideLength; i++)
            {
                SetCursorPosition(CursorPosition.X, CursorPosition.Y + 1);
                Thread.Sleep(delay);
            }

            // Move left
            for (int i = 0; i < sideLength; i++)
            {
                SetCursorPosition(CursorPosition.X - 1, CursorPosition.Y);
                Thread.Sleep(delay);
            }

            // Move up
            for (int i = 0; i < sideLength; i++)
            {
                SetCursorPosition(CursorPosition.X, CursorPosition.Y - 1);
                Thread.Sleep(delay);
            }
        }
    }

    /// <summary>
    /// Moves the cursor from one point to another in a human-like manner
    /// </summary>
    /// <param name="startX">Starting X coordinate</param>
    /// <param name="startY">Starting Y coordinate</param>
    /// <param name="endX">Ending X coordinate</param>
    /// <param name="endY">Ending Y coordinate</param>
    /// <param name="humanFactor">Factor controlling how human-like the movement is (0.0-1.0, default 0.5)</param>
    /// <param name="speed">Movement speed factor (1.0 = normal, higher = faster)</param>
    /// <param name="overshoot">Whether cursor might slightly overshoot the target sometimes (more human-like)</param>
    public static void MoveMouseHumanlike(int startX, int startY, int endX, int endY,
                                         double humanFactor = 0.5, double speed = 1.0,
                                         bool overshoot = true)
    {
        // Validate parameters
        humanFactor = Math.Clamp(humanFactor, 0.0, 1.0);
        speed = Math.Max(0.1, speed);

        // Store initial position if we're not starting from a specific position
        if (startX < 0 || startY < 0)
        {
            var currentPos = CursorPosition;
            startX = currentPos.X;
            startY = currentPos.Y;
        }

        // Calculate distance
        double distance = Math.Sqrt(Math.Pow(endX - startX, 2) + Math.Pow(endY - startY, 2));

        // Determine the number of steps based on distance and speed
        int steps = Math.Max(10, (int)(distance / (10 * speed)));

        // Generate control points for Bézier curve to create human-like movement
        Random rand = new();

        // Control point deviation is influenced by humanFactor
        double maxDeviation = distance * (0.1 + 0.3 * humanFactor);

        // Create control points for a quadratic Bézier curve
        // More human movement tends to deviate from straight lines
        double controlX = (startX + endX) / 2.0 + (rand.NextDouble() * 2 - 1) * maxDeviation;
        double controlY = (startY + endY) / 2.0 + (rand.NextDouble() * 2 - 1) * maxDeviation;

        // Add slight variation to end position if overshoot is enabled
        double targetX = endX;
        double targetY = endY;
        if (overshoot && rand.NextDouble() < 0.2 * humanFactor)
        {
            // Occasionally overshoot by a little
            targetX = endX + (rand.NextDouble() * 2 - 1) * 5 * humanFactor;
            targetY = endY + (rand.NextDouble() * 2 - 1) * 5 * humanFactor;
        }

        // Calculate time intervals with variability for more human-like movement
        List<int> timeIntervals = new();
        int baseDelay = (int)(15 / speed);

        for (int i = 0; i < steps; i++)
        {
            // Add random variability to timing based on humanFactor
            double variability = 1.0 + (rand.NextDouble() * humanFactor - humanFactor / 2) * 0.5;
            timeIntervals.Add((int)(baseDelay * variability));
        }

        // Move along the curve
        for (int i = 0; i <= steps; i++)
        {
            // Parameter t goes from 0 to 1
            double t = (double)i / steps;

            // Quadratic Bézier formula: B(t) = (1-t)²P₀ + 2(1-t)tP₁ + t²P₂
            double oneMinusT = 1 - t;
            double oneMinusTSquared = oneMinusT * oneMinusT;
            double tSquared = t * t;

            // Calculate x and y coordinates
            double x = oneMinusTSquared * startX + 2 * oneMinusT * t * controlX + tSquared * targetX;
            double y = oneMinusTSquared * startY + 2 * oneMinusT * t * controlY + tSquared * targetY;

            // Move mouse to this position
            SetCursorPosition((int)Math.Round(x), (int)Math.Round(y));

            // Variable delay
            if (i < timeIntervals.Count)
                Thread.Sleep(timeIntervals[i]);
        }

        // Always ensure we end exactly at the requested coordinates
        if (overshoot)
        {
            Thread.Sleep(rand.Next(50, 150));
            SetCursorPosition(endX, endY);
        }
    }

    /// <summary>
    /// Overload that moves from current position to target position
    /// </summary>
    /// <param name="endX">Target X coordinate</param>
    /// <param name="endY">Target Y coordinate</param>
    /// <param name="humanFactor">Factor controlling how human-like the movement is (0.0-1.0, default 0.5)</param>
    /// <param name="speed">Movement speed factor (1.0 = normal, higher = faster)</param>
    /// <param name="overshoot">Whether cursor might slightly overshoot the target sometimes (more human-like)</param>
    public static void MoveMouseHumanlike(int endX, int endY, double humanFactor = 0.5,
                                         double speed = 1.0, bool overshoot = true)
    {
        var current = CursorPosition;
        MoveMouseHumanlike(current.X, current.Y, endX, endY, humanFactor, speed, overshoot);
    }

    /// <summary>
    /// Moves the cursor from one point to another using WinPoint structs
    /// </summary>
    /// <param name="start">Starting point</param>
    /// <param name="end">Ending point</param>
    /// <param name="humanFactor">Factor controlling how human-like the movement is (0.0-1.0, default 0.5)</param>
    /// <param name="speed">Movement speed factor (1.0 = normal, higher = faster)</param>
    /// <param name="overshoot">Whether cursor might slightly overshoot the target sometimes (more human-like)</param>
    public static void MoveMouseHumanlike(Coordinate start, Coordinate end, double humanFactor = 0.5,
                                         double speed = 1.0, bool overshoot = true)
    {
        MoveMouseHumanlike(start.X, start.Y, end.X, end.Y, humanFactor, speed, overshoot);
    }

    /// <summary>
    /// Overload that moves from current position to target position
    /// </summary>
    /// <param name="endX">Target X coordinate</param>
    /// <param name="endY">Target Y coordinate</param>
    /// <param name="humanFactor">Factor controlling how human-like the movement is (0.0-1.0, default 0.5)</param>
    /// <param name="speed">Movement speed factor (1.0 = normal, higher = faster)</param>
    /// <param name="overshoot">Whether cursor might slightly overshoot the target sometimes (more human-like)</param>
    public static void MoveMouseHumanlike(Coordinate end, double humanFactor = 0.5,
                                         double speed = 1.0, bool overshoot = true)
    {
        var current = CursorPosition;
        MoveMouseHumanlike(current.X, current.Y, end.X, end.Y, humanFactor, speed, overshoot);
    }
}