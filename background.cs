public class Background
{
    public static string Get(string profile)
    {
        var file = @".\joysticks\" + profile + ".png";

        if (!File.Exists(file))
        {
            throw new Exception("File not found: " + file);
        }

        return file;
    }
}