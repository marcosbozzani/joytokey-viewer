public class Background
{
    public static string Get(string profile)
    {
        var file = Paths.GetFull(@"joysticks\" + profile + ".png");

        if (!File.Exists(file))
        {
            throw new Exception("File not found: " + file);
        }

        return file;
    }
}