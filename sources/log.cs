public class Log
{
    public static void Debug(string format, params object[] args)
    {
        #if debug
        Console.WriteLine(format, args);
        #endif
    }

    public static void Info(string format, params object[] args)
    {
        #if info
        Console.WriteLine(format, args);
        #endif
    }
}