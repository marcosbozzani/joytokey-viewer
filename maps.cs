
public class Maps
{
    public static Dictionary<string, Setting> Load(string profile)
    {
        var file = @".\joysticks\" + profile + ".map";

        if (!File.Exists(file))
        {
            throw new Exception("File not found: " + file);
        }

        var pairs = new Dictionary<string, Setting>();

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

                pairs[data[0]] = new Setting(data[1]);
            }
        }

        return pairs;
    }

    private static string[] Split(string input, string separator)
    {
        return input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}

