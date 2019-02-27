using System;
using System.Threading;
namespace Maptz
{
    public class AsyncLineReader
    {
        #region Private Static Fields
        private static AutoResetEvent getInput, gotInput;
        private static string input;
        private static Thread inputThread;
        #endregion Private Static Fields

        #region Private Static Constructors
        static AsyncLineReader()
        {
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
        }
        #endregion Private Static Constructors

        #region Private Static Methods
        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = System.Console.ReadLine();
                gotInput.Set();
            }
        }
        #endregion Private Static Methods

        #region Public Static Methods
        public static string ReadLine(int? timeOutMillisecs)
        {
            getInput.Set();
            bool success;
            if (timeOutMillisecs.HasValue)
            {
                success = gotInput.WaitOne(timeOutMillisecs.Value);
            }
            else
            {
                success = gotInput.WaitOne();
            }
            if (success)
                return input;
            else
                throw new TimeoutException("User did not provide input within the timelimit.");
        }
        #endregion Public Static Methods

    }
}