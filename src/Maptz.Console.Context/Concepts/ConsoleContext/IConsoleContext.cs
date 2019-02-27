namespace Maptz
{
    public interface IConsoleContext
    {
        /// <summary>
        /// The current context text.
        /// </summary>
        string Buffer { get;  }
        /// <summary>
        /// The current cursor index
        /// </summary>
        int CursorIndex { get;  }
        /// <summary>
        /// Initialize the context. 
        /// </summary>
        /// <returns></returns>
        string Read();
    }
}