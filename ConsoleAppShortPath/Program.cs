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
        new(7, new[]
        {
            new Segment(10, new Point(8, 5), new Point(14, 5)),
        }),
        new(8, new[]
        {
            new Segment(11, new Point(0, 0), new Point(2, 1)),
            new Segment(12, new Point(2, 1), new Point(2, 0)),
            new Segment(13, new Point(2, 0), new Point(0, 0)),
        }),
    };

    var station = new Station(1, paths);

    Console.WriteLine("Station segments:");

    var segments = station.Paths.SelectMany(p => p.Segments).ToArray();

    for (var i = 0; i < segments.Length; i++)
    {
        var segment = segments[i];

        Console.WriteLine($"[{i + 1}].[{segment.Name}]");
    }

    while (true)
    {
        Console.Write("Enter the number of the start segment or enter to exit: ");

        var readLine = Console.ReadLine();

        if (string.IsNullOrEmpty(readLine)) {
            break;
        }

        uint startSegmentNumber = uint.Parse(readLine);

        Console.Write("Enter the number of the end segment or enter to exit: ");

        readLine = Console.ReadLine();

        if (string.IsNullOrEmpty(readLine)) {
            break;
        }

        uint endSegmentNumber = uint.Parse(readLine);

        var shortestPath = station.FindShortestPath(segments[startSegmentNumber - 1], segments[endSegmentNumber - 1]);

        if (shortestPath != null)
        {
            Console.WriteLine("Shortest path:");
            foreach (var segment in shortestPath.Segments) {
                Console.WriteLine(segment.ToString());
            }
        }
        else
        {
            Console.WriteLine("The path does not exist!");
        }
    }

    Console.WriteLine("End test console app railway!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

