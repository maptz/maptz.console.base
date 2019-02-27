using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maptz
{

    /// <summary>
    /// A progress bar for console applications.
    /// </summary>
    public class ProgressBar : IDisposable, IProgressMonitor
    {
        /* #region Private Sub-types */
        private class ConsoleStateSaver : IDisposable
        {
            ConsoleColor _bgColor;
            int _cursorTop, _cursorLeft;
            bool _cursorVisible;

            public ConsoleStateSaver()
            {
                _bgColor = global::System.Console.BackgroundColor;
                _cursorTop = global::System.Console.CursorTop;
                _cursorLeft = global::System.Console.CursorLeft;
                _cursorVisible = global::System.Console.CursorVisible;
            }

            public void Dispose()
            {
                RestoreState();
            }

            public void RestoreState()
            {
                 global::System.Console.BackgroundColor = _bgColor;
                 global::System.Console.CursorTop = _cursorTop;
                 global::System.Console.CursorLeft = _cursorLeft;
                global::System.Console.CursorVisible = _cursorVisible;
            }
        }
        /* #endregion Private Sub-types */
        /* #region Private Fields */
        private int _cursorRow;
        private int _lastFilledSlots;
        private int _progress;
        private int _total;
        private int _width;
        /* #endregion Private Fields */
        /* #region Private Methods */
        private void DrawBar()
        {
            using (new ConsoleStateSaver())
            {
                 global::System.Console.CursorVisible = false;
                 global::System.Console.CursorTop = _cursorRow;

                // Draw the outline of the progress bar
                 global::System.Console.CursorLeft = _width + 1;
                global::System.Console.Write("]");

                 global::System.Console.CursorLeft = 0;
                global::System.Console.Write("[");
                // Draw progressed part
                 global::System.Console.BackgroundColor = ConsoleColor.Green;
                global::System.Console.Write(new String(' ', _lastFilledSlots));

                // Draw remaining part
                global::System. Console.BackgroundColor = ConsoleColor.Black;
                global::System.Console.Write(new String(' ', _width - _lastFilledSlots));
            }
        }
        private void DrawText()
        {
            using (new ConsoleStateSaver())
            {
                // Write progress text
                 global::System.Console.CursorVisible = false;
                 global::System.Console.CursorTop = _cursorRow;
                 global::System.Console.CursorLeft = _width + 4;
                 global::System.Console.Write("{0} of {1}", _progress.ToString().PadLeft(_total.ToString().Length), _total);
                global::System.Console.Write(new String(' ', global::System.Console.WindowWidth - global::System.Console.CursorLeft));
            }
        }
        /* #endregion Private Methods */
        /* #region Public Constructors */
        public ProgressBar(int total, int width = -1, bool drawImmediately = true)
        {
            _progress = 0;
            _total = total;

            if (width < 0)
                _width = global::System.Console.WindowWidth - string.Format("[]   {0} of {0}", _total).Length;
            else
                _width = width;

            _lastFilledSlots = -1;
            _cursorRow = -1;

            if (drawImmediately)
                Update(0);
        }
        /* #endregion Public Constructors */
        /* #region Public Methods */
        
        /// <summary>
        /// Force the progress bar to draw.
        /// </summary>
        public void ForceDraw()
        {
            DrawBar();
            DrawText();

            if (global::System.Console.CursorTop == _cursorRow)
                global::System.Console.CursorLeft = global::System.Console.WindowWidth - 1;
        }
        /// <summary>
        /// Increment the progress bar.
        /// </summary>
        public void Increment()
        {
            Update(_progress + 1);
        }
        /* #endregion Public Methods */
        /* #region Interface: 'Maptz.IProgressMonitor' Methods */
        public void Update(int progress)
        {
            _progress = Math.Max(Math.Min(progress, _total), 0);

            if (_cursorRow < 0)
            {
                _cursorRow = global::System.Console.CursorTop;
                 global::System.Console.CursorTop++;
                global::System.Console.CursorLeft = 0;
            }

            int filledSlots = (int)Math.Floor(_width * ((double)progress) / _total);
            if (filledSlots != _lastFilledSlots)
            {
                _lastFilledSlots = filledSlots;
                DrawBar();
            }

            DrawText();

            if (global::System.Console.CursorTop == _cursorRow)
                global::System.Console.CursorLeft = global::System.Console.WindowWidth - 1;
        }
        /* #endregion Interface: 'Maptz.IProgressMonitor' Methods */
        /* #region Interface: 'System.IDisposable' Methods */
        public void Dispose()
        {
            Update(_total);

            if ( global::System.Console.CursorTop == _cursorRow)
                global::System.Console.WriteLine("");
        }
        /* #endregion Interface: 'System.IDisposable' Methods */
    }
}