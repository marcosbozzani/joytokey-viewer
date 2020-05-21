public class Printer
{
    public static void Print(Action<string> callback, Dictionary<string, Setting> map)
    {
        callback(string.Format("{0,-10} {1,-14} {2,-10} {3,-10} {4}",
            "Key", "Name", "Type", "Buttons", "Comment"));

        callback(new String('=', 58));

        foreach (var pair in map) 
        {
            var key = pair.Key;
            var value = pair.Value;
            callback(string.Format("{0,-10} {1,-14} {2,-10} {3,-10} {4}",
                key, value.Name, value.Type, string.Join(" ", value.Buttons), value.Comment));
        }
    }
}