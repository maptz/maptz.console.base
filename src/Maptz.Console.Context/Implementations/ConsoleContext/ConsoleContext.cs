using System;
using System.Text.RegularExpressions;

namespace Maptz
{

    /// <summary>
    /// A context for the console, allowing you to keep track of the current console text as the user types.
    /// </summary>
    public class ConsoleContext
    {
        /* #region Private Methods */
        private void Clear()
        {
            //Clear the screen.
            //Console.Clear();
            var positionX = global::System.Console.CursorLeft;
            var positionY = global::System.Console.CursorTop;

            global::System.Console.CursorLeft = 0;

            //Console.CursorLeft = 0;
            //Console.CursorTop = 0;

            //Console.Write(this.Header);

            //Console.CursorTop = 1;
            //Console.CursorLeft = 0;
        }
        /* #endregion Private Methods */
        /* #region Protected Methods */
        protected virtual bool OnKey(ConsoleKeyInfo k)
        {
            var c = k.KeyChar;
            if (k.Key == ConsoleKey.Tab)
            {

            }
            else if (k.Key == ConsoleKey.Escape)
            {
                this.Buffer = string.Empty;
                this.CursorIndex = 0;
                return false;
            }
            else if (k.Key == ConsoleKey.Enter)
            {
                return false;
            }
            else if (k.Key == ConsoleKey.LeftArrow)
            {
                this.CursorIndex = this.CursorIndex > 0 ? this.CursorIndex - 1 : this.CursorIndex;
            }
            else if (k.Key == ConsoleKey.RightArrow)
            {
                this.CursorIndex = this.CursorIndex < this.Buffer.Length ? this.CursorIndex + 1 : this.CursorIndex;
            }
            else if (k.Key == ConsoleKey.UpArrow)
            {

            }
            else if (k.Key == ConsoleKey.DownArrow)
            {

            }
            else if (k.Key == ConsoleKey.Backspace)
            {
                if (global::System.Console.CursorLeft > 0 && this.Buffer.Length > 0)
                {
                    var prefix = this.Buffer.Substring(0, this.CursorIndex - 1);
                    var suffix = this.CursorIndex < this.Buffer.Length ? this.Buffer.Substring(this.CursorIndex) : string.Empty;

                    this.Buffer = prefix + suffix;
                    this.CursorIndex--;
                }
            }
            else if (k.Key == ConsoleKey.Delete)
            {
                if (global::System.Console.CursorLeft < this.Buffer.Length && this.Buffer.Length > 0)
                {
                    var prefix = this.Buffer.Substring(0, this.CursorIndex);
                    var suffix = this.CursorIndex < this.Buffer.Length ? this.Buffer.Substring(this.CursorIndex + 1) : string.Empty;
                    this.Buffer = prefix + suffix;
                }
            }
            else if (Regex.IsMatch(c.ToString(), ".", RegexOptions.IgnoreCase))
            {
                this.Buffer += c;
                this.CursorIndex++;
            }

            return true;
        }
        protected virtual void Redraw(bool continu)
        {
            global::System.Console.CursorLeft = 0;
            global::System.Console.Write(string.Join(" ", new string[global::System.Console.BufferWidth]));
            global::System.Console.CursorLeft = 0;
            global::System.Console.Write(this.Buffer);
            global::System.Console.CursorLeft = this.CursorIndex;
        }
        /* #endregion Protected Methods */
        /* #region Public Properties */
        public string Buffer { get; protected set; }
        public int CursorIndex { get; protected set; }
        public string Header { get; set; }
        /* #endregion Public Properties */
        /* #region Public Constructors */
        public ConsoleContext()
        {

        }
        /* #endregion Public Constructors */
        /* #region Public Methods */
        public string Read()
        {
            this.Clear();


            var continu = true;
            this.Buffer = string.Empty;
            this.CursorIndex = global::System.Console.CursorLeft;
            this.Redraw(continu);
            while (continu)
            {
                var k = global::System.Console.ReadKey(true);
                continu = this.OnKey(k);
                this.Redraw(continu);
            }

            global::System.Console.CursorTop++;
            global::System.Console.CursorLeft = 0;
            return this.Buffer;
        }
        /* #endregion Public Methods */
    }

}