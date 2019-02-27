using System;
using System.Collections.Generic;
using System.Linq;
namespace Maptz
{public class DynamicSuggestionsProvider : ISuggestionProvider
    {
        public DynamicSuggestionsProvider(Func<string,IEnumerable<string>> suggestionsFunc)
        {
            this.SuggestionsFunc = suggestionsFunc;
        }

        public Func<string, IEnumerable<string>> SuggestionsFunc { get; private set; }

        public IEnumerable<string> GetSuggestions(string buffer)
        {
            return this.SuggestionsFunc(buffer);
        }
    }
}