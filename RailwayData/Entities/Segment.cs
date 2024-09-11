namespace RailwayData.Entities;

/// <summary>
/// Участок.
/// </summary>
public class Segment : BaseEntity
{
    /// <summary>
    /// Создает новый экземпляр участка.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="startPoint">Координаты начала участка.</param>
    /// <param name="endPoint">Координаты конца участка.</param>
    public Segment(uint id, Point startPoint, Point endPoint)
    {
        Id = id;
        StartPoint = startPoint;
        EndPoint = endPoint;

        Length = MathF.Sqrt(MathF.Pow(endPoint.X - startPoint.X, 2) + MathF.Pow(endPoint.Y - startPoint.Y, 2));
    }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public string Name => $"{nameof(Segment)} {Id}";

    /// <summary>
    /// Возвращает начальную точку.
    /// </summary>
    public Point StartPoint { get; }

    /// <summary>
    /// Возвращает конечную точку.
    /// </summary>
    public Point EndPoint { get; }

    /// <summary>
    /// Возвращает длину.
    /// </summary>
    public float Length { get; }

    public override string ToString()
    {
        return $"[{StartPoint} -> {EndPoint}]";
    }
}
