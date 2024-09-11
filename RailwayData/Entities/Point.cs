namespace RailwayData.Entities;

/// <summary>
/// Вершина.
/// </summary>
public struct Point
{
    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float X { get; }

    public float Y { get; }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        var other = (Point)obj;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator >(Point a, Point b)
    {
        return a.X > b.X && a.Y > b.Y;
    }

    public static bool operator >=(Point a, Point b)
    {
        return a.X >= b.X && a.Y >= b.Y;
    }

    public static bool operator <(Point a, Point b)
    {
        return a.X < b.X && a.Y < b.Y;
    }

    public static bool operator <=(Point a, Point b)
    {
        return a.X <= b.X && a.Y <= b.Y;
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return a.X != b.X || a.Y != b.Y;
    }
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
}
