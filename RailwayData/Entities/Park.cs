namespace RailwayData.Entities;

/// <summary>
/// Парк.
/// </summary>
public class Park : BaseEntity
{
    /// <summary>
    /// Создает новый экземпляр парка станции.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="paths">Список путей.</param>
    public Park(uint id, IEnumerable<Path> paths) 
    {
        Id = id;
        Paths = paths;
    }

    /// <summary>
    /// Возвращает список путей.
    /// </summary>
    public IEnumerable<Path> Paths { get; }

    public override string ToString()
    {
        return $"{nameof(Park)} {Id}";
    }
}
