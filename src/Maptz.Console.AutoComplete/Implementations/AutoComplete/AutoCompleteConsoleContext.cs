using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace Maptz
{

    /// <summary>
    /// A console context used for an autocomplete console.
    /// </summary>
    public class AutoCompleteConsoleContext : ConsoleContext, IAutoCompleteConsoleContext
    {
        /* #region Private Methods */
        private void InvalidateCurrentMatches()
        {
            this.CurrentMatches = this.SuggestionsProvider.GetSuggestions(this.Buffer);
            //System.Diagnostics.Debug.WriteLine("Current matches invalidated");
            //this.CurrentMatches.ToList().ForEach(p => System.Diagnostics.Debug.WriteLine($"\t{p.ToString()}"));

            var oldMatch = this.CurrentMatch;
            this.CurrentMatch = this.CurrentMatches.Contains(oldMatch) ? oldMatch : this.CurrentMatches.FirstOrDefault();
        }
        private void MoveCurrentMatch(int count)
        {
            if (this.CurrentMatch != null && this.CurrentMatches.Count() > 0)
            {
                var currentIndex = Array.IndexOf(this.CurrentMatches as string[], this.CurrentMatch);
                var newIndex = currentIndex + count;
                var shortIndex = newIndex % this.CurrentMatches.Count();
                var actualIndex = shortIndex < 0 ? this.CurrentMatches.Count() + shortIndex : shortIndex;

                System.Diagnostics.Debug.WriteLine($"Setting new Current Match Index {actualIndex}");

                this.CurrentMatch = this.CurrentMatches.ElementAt(actualIndex);
            }
            else
            {
                if (count > 0)
                {
                    this.CurrentMatch = this.CurrentMatches.FirstOrDefault();
                }
                else
                {
                    this.CurrentMatch = this.CurrentMatches.LastOrDefault();
                }
            }
        }
        /* #endregion Private Methods */
        /* #region Protected Methods */
        protected override bool OnKey(ConsoleKeyInfo k)
        {
            var c = k.KeyChar;

            var match = this.CurrentMatch;
            if (match != null && (match != this.Buffer) && (k.Key == ConsoleKey.Tab || k.Key == ConsoleKey.Enter))
            {
                if (match != null)
                {
                    this.Buffer = match;
                    this.CursorIndex = this.Buffer.Length;
                    return true;
                }
            }
            else if (k.Key == ConsoleKey.UpArrow)
            {
                this.MoveCurrentMatch(-1);

            }
            else if (k.Key == ConsoleKey.DownArrow)
            {
                this.MoveCurrentMatch(1);
            }
            var retval = base.OnKey(k);

            return retval;
        }
        protected override void Redraw(bool continu)
        {
            base.Redraw(continu);
            this.InvalidateCurrentMatches();

            if (continu)
            {
                global::System.Console.CursorLeft = this.Buffer.Length;
                if (this.Buffer.Length >= this.Settings.MinHintLength)
                {
                    var match = this.CurrentMatch;
                    if (match != null)
                    {
                        global::System.Console.BackgroundColor = ConsoleColor.White;
                        global::System.Console.ForegroundColor = ConsoleColor.Black;
                        var remainingLength = Math.Min(match.Length - this.Buffer.Length, global::System.Console.BufferWidth - this.Buffer.Length);
                        if (remainingLength > 0)
                        {
                            var bufferRemainder = match.Substring(this.Buffer.Length, remainingLength);
                            global::System.Console.Write(bufferRemainder);
                        }
                        global::System.Console.BackgroundColor = ConsoleColor.Black;
                        global::System.Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                global::System.Console.CursorLeft = this.CursorIndex;
            }
        }
        /* #endregion Protected Methods */
        /* #region Public Properties */
        public string CurrentMatch { get; private set; }
        public IEnumerable<string> CurrentMatches { get; private set; }
        public AutoCompleteSettings Settings { get; private set; }
        public ISuggestionProvider SuggestionsProvider { get; private set; }
        /* #endregion Public Properties */
        /* #region Public Constructors */
        public AutoCompleteConsoleContext(ISuggestionProvider suggestionsProvider, IOptions<AutoCompleteSettings> settings)
        {
            this.SuggestionsProvider = suggestionsProvider;
            this.Settings = settings.Value;
        }
        /* #endregion Public Constructors */
    }

}