using System;
using System.Threading;

namespace Maptz
{

    public class AsyncKeyReader
    {
        #region Private Static Fields
        private static AutoResetEvent getInput, gotInput;
        private static ConsoleKeyInfo input;
        private static Thread inputThread;
        #endregion Private Static Fields

        #region Private Static Constructors
        static AsyncKeyReader()
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
                input = System.Console.ReadKey();
                gotInput.Set();
            }
        }
        #endregion Private Static Methods

        #region Public Static Methods
        public static ConsoleKeyInfo ReadKey(int? timeOutMillisecs)
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
