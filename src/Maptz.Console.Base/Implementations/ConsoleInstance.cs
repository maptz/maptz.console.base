using System;
namespace Maptz
{

    public class ConsoleInstance : IConsoleInstance
    {
        /* #region Interface: 'Maptz.CliTools.MGetVersion.IConsoleInstance' Methods */
        public void Write(string str)
        {
            Console.Write(str);
        }
        public void WriteError(string str)
        {
            var store = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.Write(str);
            Console.ForegroundColor = store;
        }
        public void WriteErrorLine(string str)
        {
            var store = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(str);
            Console.ForegroundColor = store;
        }
        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
        public void WriteWarning(string str)
        {
            var store = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(str);
            Console.ForegroundColor = store;
        }
        public void WriteWarningLine(string str)
        {
            var store = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(str);
            Console.ForegroundColor = store;
        }
        /* #endregion Interface: 'Maptz.CliTools.MGetVersion.IConsoleInstance' Methods */
    }
}