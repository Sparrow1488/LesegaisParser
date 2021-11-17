using System;
using System.Collections.Generic;
using System.Text;

namespace LesegaisParser
{
    public static class SimpleLogger
    {
        public static void LogSucc(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[success] ");
            Console.ResetColor();
            Console.WriteLine(text);
        }

        public static void LogInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[info] ");
            Console.ResetColor();
            Console.WriteLine(text);
        }

        public static void LogError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[error] ");
            Console.ResetColor();
            Console.WriteLine(text);
        }
    }
}
