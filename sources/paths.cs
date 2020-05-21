public class Paths
{
    private static string PsScriptRoot = 
        System.Environment.GetEnvironmentVariable("PsScriptRoot");

    public static string GetFull(string path)
    {
        return Path.Combine(PsScriptRoot, path);
    }
}