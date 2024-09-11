using RailwayData.Entities;
using Path = RailwayData.Entities.Path;

namespace RailwayUTest;

public class StationTest
{
    /// <summary>
    /// Тест схемы станции.
    /// </summary>
    [Fact]
    public void TestStationPoints() {
        var pointA = new Point(0, 0);
        var pointB = new Point(0, 10);
        var segment1 = new Segment(1, pointA, pointB);

        var pathA = new Path(1, new List<Segment> { segment1 });
        
        var park = new Park(2, new List<Path> { pathA });

        var points = park.Paths.SelectMany(p => p.Segments.SelectMany(s => new[] { s.StartPoint, s.EndPoint }));
        
        Assert.Contains(pointA, points);
        Assert.Contains(pointB, points);
        Assert.Equal(2, points.Count());
    }

    /// <summary>
    /// Тест заливки парка.
    /// </summary>
    [Fact]
    public void TestStationFilling() {

        var pointA = new Point(0, 0);
        var pointB = new Point(0, 10);
        var segment1 = new Segment(1, pointA, pointB);

        var pathA = new Path(1, new List<Segment> { segment1 });

        var station = new Station(1, new[] { pathA });

        station.Filling(new[] { 1 });

        var park = new Park(1, new[] { pathA });

        Assert.Equal(park.Id, station.Parks.First().Id);
        Assert.Equal(1, station.Parks.Count);
    }

    /// <summary>
    /// Тест поиска кратчайшего пути.
    /// </summary>
    [Fact]
    public void TestShortestPathExists() {
        var paths = new List<Path>
        {
            new(1, new[]
            {
                new Segment(1, new Point(2, 5), new Point(8, 5)),
                new Segment(2, new Point(8, 5), new Point(12, 7)),
                new Segment(3, new Point(12, 7), new Point(14, 5)),
            }),
            new(2, new[]
            {
                new Segment(4, new Point(8, 5), new Point(14, 5)),
            }),
        };

        var segments = paths.SelectMany(p => p.Segments);
        var station = new Station(1, paths);

        var shortestPath = station.FindShortestPath(segments.First(s => s.Id == 1), segments.First(s => s.Id == 4));
        Assert.NotNull(shortestPath);
        Assert.DoesNotContain(segments.First(s => s.Id == 2), shortestPath.Segments);

        shortestPath = station.FindShortestPath(segments.First(s => s.Id == 4), segments.First(s => s.Id == 1));
        Assert.NotNull(shortestPath);
        Assert.DoesNotContain(segments.First(s => s.Id == 3), shortestPath.Segments);

        shortestPath = station.FindShortestPath(segments.First(s => s.Id == 1), segments.First(s => s.Id == 3));
        Assert.NotNull(shortestPath);
        Assert.Contains(segments.First(s => s.Id == 3), shortestPath.Segments);

        shortestPath = station.FindShortestPath(segments.First(s => s.Id == 3), segments.First(s => s.Id == 1));
        Assert.NotNull(shortestPath);
        Assert.DoesNotContain(segments.First(s => s.Id == 4), shortestPath.Segments);

        shortestPath = station.FindShortestPath(segments.First(s => s.Id == 1), segments.First(s => s.Id == 1));
        Assert.NotNull(shortestPath);
        Assert.Contains(segments.First(s => s.Id == 1), shortestPath.Segments);
    }

    /// <summary>
    /// Тест поиска кратчайшего пути с проверкой несуществующего пути.
    /// </summary>
    [Fact]
    public void TestNoShortestPathExists() {
        var paths = new List<Path>
        {
            new(1, new[]
            {
                new Segment(1, new Point(0, 0), new Point(3, 0)),
            }),
            new(2, new[]
            {
                new Segment(2, new Point(0, 2), new Point(5, 2)),
            }),
        };

        var segments = paths.SelectMany(p => p.Segments);
        var station = new Station(1, paths);

        var shortestPath = station.FindShortestPath(segments.First(s => s.Id == 1), segments.First(s => s.Id == 2));
        Assert.Null(shortestPath);
    }
}