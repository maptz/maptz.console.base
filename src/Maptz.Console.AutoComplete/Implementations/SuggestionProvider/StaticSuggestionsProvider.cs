using System;
using System.Collections.Generic;
using System.Linq;
namespace Maptz
{

    /// <summary>
    /// A suggestions provider for static suggestions.
    /// </summary>
    public class StaticSuggestionsProvider : ISuggestionProvider
    {
        /* #region Public Properties */
        public IEnumerable<string> Suggestions { get; private set; }
        /* #endregion Public Properties */
        /* #region Public Constructors */
        public StaticSuggestionsProvider(IEnumerable<string> suggestions)
        {
            this.Suggestions = suggestions;
        }
        /* #endregion Public Constructors */
        /* #region Interface: 'Maptz.ISuggestionProvider' Methods */
        public IEnumerable<string> GetSuggestions(string buffer)
        {
            return this.Suggestions.Where(p => p.StartsWith(buffer, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
        /* #endregion Interface: 'Maptz.ISuggestionProvider' Methods */
    }
}