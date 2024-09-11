namespace RailwayData.Entities;

/// <summary>
/// Путь.
/// </summary>
public class Path : BaseEntity
{
    /// <summary>
    /// Создает новый экземпляр пути.
    /// </summary>
    /// <param name="segments">Список участков.</param>
    public Path(IEnumerable<Segment> segments)
    {
        Id = 0;
        Segments = segments;

        Length = segments.Select(s => s.Length).Sum();
    }

    /// <summary>
    /// Создает новый экземпляр пути.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="segments">Список участков.</param>
    public Path(uint id, IEnumerable<Segment> segments) 
    {
        Id = id;
        Segments = segments;

        Length = segments.Select(s => s.Length).Sum();
    }

    /// <summary>
    /// Возвращает список участков.
    /// </summary>
    public IEnumerable<Segment> Segments { get; }

    /// <summary>
    /// Возвращает длину.
    /// </summary>
    public float Length { get; }

    public override string ToString()
    {
        return $"{nameof(Path)} {Id}";
    }
}
