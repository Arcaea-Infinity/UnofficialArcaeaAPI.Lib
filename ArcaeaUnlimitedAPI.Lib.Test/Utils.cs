using System.Reflection;

namespace ArcaeaUnlimitedAPI.Lib.Test;

public static class Utils
{
    public static void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void LogIfPassed(bool passed)
    {
        if (passed) LogSuccess("Passed\n");
        else LogError("Failed\n");
    }

    public static void LogProperty(PropertyInfo property, object obj, ref bool passed)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"  {property.Name} - ");
        var val = property.GetValue(obj);
        if (val is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Null");
            passed = false;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(val.GetType().Name);
        }
    }

    public static void Test(object obj, ref bool passed)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("- Testing " + obj.GetType().Name);
        foreach (var property in obj.GetType().GetProperties())
            LogProperty(property, obj, ref passed);
    }
}