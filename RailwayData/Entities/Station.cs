namespace RailwayData.Entities;

/// <summary>
/// Cтанция.
/// </summary>
public class Station : BaseEntity
{
    /// <summary>
    /// Создает новый экземпляр схемы станции.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="paths">Список путей.</param>
    public Station(uint id, IEnumerable<Path> paths)
    {
        Id = id;
        this.paths = paths.ToList();
        parks = new List<Park>();
    }

    private readonly List<Path> paths;
    private readonly List<Park> parks;

    /// <summary>
    /// Возвращает список путей.
    /// </summary>
    public IReadOnlyCollection<Path> Paths => paths.AsReadOnly();

    /// <summary>
    /// Возвращает списко парков.
    /// </summary>
    public IReadOnlyCollection<Park> Parks => parks.AsReadOnly();

    /// <summary>
    /// Выполняет заливку парка.
    /// </summary>
    /// <param name="pathNumbers">Номера путей.</param>
    public void Filling(int[] pathNumbers)
    {
        var paths = this.paths.Where(p => pathNumbers.Any(n => n == p.Id));

        uint currentId = 0;

        if (parks.Any()) {
            currentId = parks.Select(p => p.Id).Max();
        }

        parks.Add(new Park(currentId + 1, paths));
    }

    /// <summary>
    /// Выполняет поиск кратчайшего пути.
    /// </summary>
    /// <param name="startSegment">Начальный участок пути.</param>
    /// <param name="endSegment">Конечный участок пути.</param>
    /// <returns>Возвращает путь или null если пути не существует.</returns>
    public Path? FindShortestPath(Segment startSegment, Segment endSegment)
    {
        if (startSegment == endSegment) {
            return new Path(new[] { endSegment });
        }

        var graph = BuildGraph(startSegment, endSegment);

        if (graph == null) {
            return null;
        }

        var segments = new List<Segment> { startSegment };
        segments.AddRange(graph.Select(g => g.Key));

        var segmentDistances = new Dictionary<Segment, float>();
        var visitedSegments = new Dictionary<Segment, Segment?>();
        var priorityQueue = new SortedSet<(float distance, Segment segment)>(Comparer<(float distance, Segment)>
            .Create((x, y) => x.distance.CompareTo(y.distance)));

        foreach (var segment in segments)
        {
            segmentDistances[segment] = float.PositiveInfinity;
            visitedSegments[segment] = null;
        }

        segmentDistances[startSegment] = startSegment.Length;
        priorityQueue.Add((startSegment.Length, startSegment));

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Min;
            priorityQueue.Remove(current);

            if (current.segment.Id == endSegment.Id)
            {
                var currentSegment = current.segment;

                var pathSegments = new List<Segment>();

                while (currentSegment != null && visitedSegments.ContainsKey(currentSegment))
                {
                    pathSegments.Add(currentSegment);
                    currentSegment = visitedSegments[currentSegment];
                }

                pathSegments.Reverse();

                return new Path(pathSegments);
            }

            foreach (var neighbor in graph[current.segment])
            {
                float newDistance = current.distance + neighbor.Length;
                if (newDistance < segmentDistances[neighbor])
                {
                    priorityQueue.Remove((segmentDistances[neighbor], neighbor));
                    segmentDistances[neighbor] = newDistance;
                    visitedSegments[neighbor] = current.segment;
                    priorityQueue.Add((newDistance, neighbor));
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Выполняет построение ориентированного графа между выбранынми участками, которые имеют пересечения.
    /// </summary>
    /// <param name="startSegment">Начальный участок.</param>
    /// <param name="endSegment">Конеченый участок.</param>
    /// <returns>Возвращает словарь графов.</returns>
    private Dictionary<Segment, List<Segment>>? BuildGraph(Segment startSegment, Segment endSegment)
    {
        var graph = new Dictionary<Segment, List<Segment>>();
        var stationsSegments = paths.SelectMany(p => p.Segments).Where(s => s != startSegment);
        var segments = new List<Segment>() { startSegment };

        var isEndSegment = false;

        for (var i = 0; i < segments.Count; i++)
        {
            var segment = segments[i];

            var intersectionSegments = stationsSegments
                .Where(s => !graph.Keys.Contains(s)
                            && (segment.EndPoint == s.StartPoint || segment.StartPoint == s.EndPoint
                            || segment.StartPoint == s.StartPoint || segment.EndPoint == s.EndPoint));

            if (intersectionSegments.Any())
            {
                graph[segment] = new List<Segment>(intersectionSegments);
                segments.AddRange(intersectionSegments);
            }

            if (segment.Id == endSegment.Id) {
                isEndSegment = true;
            }
        }

        if (isEndSegment) {
            return graph;
        }

        return null;
    }
}
