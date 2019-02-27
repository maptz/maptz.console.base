using System.Collections.Generic;
namespace Maptz
{
    /// <summary>
    /// A suggestions provider.
    /// </summary>
    public interface ISuggestionProvider
    {
        /// <summary>
        /// Get suggestions based on the current text buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        IEnumerable<string> GetSuggestions(string buffer);
    }
}