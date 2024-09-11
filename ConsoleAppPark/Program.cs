using RailwayData.Entities;
using Path = RailwayData.Entities.Path;

try
{
    Console.WriteLine("Start test console app railway!");

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
        new(3, new[]
        {
            new Segment(3, new Point(1, 4), new Point(5, 4)),
        }),
        new(4, new[]
        {
            new Segment(4, new Point(3, 3), new Point(9, 3)),
            new Segment(5, new Point(9, 3), new Point(14, 2)),
        }),
        new(5, new[]
        {
            new Segment(6, new Point(2, 5), new Point(8, 5)),
            new Segment(7, new Point(8, 5), new Point(12, 7)),
            new Segment(8, new Point(12, 7), new Point(14, 5)),
        }),
        new(6, new[]
        {
            new Segment(9, new Point(1, 10), new Point(5, 10)),
        }),
    };

    var station = new Station(1, paths);

    station.Filling(new[] { 1, 2, 3 });
    station.Filling(new[] { 4, 5 });
    station.Filling(new[] { 6 });

    foreach (var park in station.Parks)
    {
        var parkPoints = park.Paths.SelectMany(t => t.Segments);
        var strParkPoints = parkPoints.Select(s => s.ToString()).Aggregate((current, next) => $"{current}; {next}");

        Console.WriteLine(park.ToString() + $": {{{strParkPoints}}}");
    }

    Console.WriteLine("End test console app railway!");
}
catch(Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}