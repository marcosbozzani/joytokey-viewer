public class Positions
{
    private string file;
    private Dictionary<string, Point> pairs;

    public Positions(string profile)
    {
        var file = Paths.GetFull(@"joysticks\" + profile + ".ini");
        this.file = file;
        this.pairs = new Dictionary<string, Point>();
    }

    public void Load() 
    {
        pairs = new Dictionary<string, Point>();

        if (!File.Exists(file))
        {
            return;
        }

        Log.Info("Reading file: {0}", file);

        using (var reader = new StreamReader(file))
        {
            string line;
            while((line = reader.ReadLine()) != null)  
            {
                Log.Debug("Line: {0}", line);

                var data = Split(line, "=");
                if (data.Length != 2)
                {
                    continue;
                }

                Log.Debug("Data[0]: {0}", data[0]);
                Log.Debug("Data[1]: {0}", data[1]);

                var coords = Split(data[1], ",");
                if (coords.Length != 2)
                {
                    continue;
                }

                pairs[data[0]] = new Point
                (
                    int.Parse(coords[0]),
                    int.Parse(coords[1])
                );
            }
        }
    }

    public void Save()
    {
        using (var writer = new StreamWriter(file))
        {
            foreach (var pair in pairs)
            {
                writer.WriteLine("{0}={1},{2}", pair.Key, pair.Value.X, pair.Value.Y);
            }
        }
    }
    
    public Point Get(string key)
    {
        var value = Point.Empty;
        if (pairs.TryGetValue(key, out value))
        {
            return value;
        }
        else
        {
            return Point.Empty;
        }
    }

    public void Set(string key, Point value)
    {
        pairs[key] = value;
    }

    private static string[] Split(string input, string separator)
    {
        return input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}