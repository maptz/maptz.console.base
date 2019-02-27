namespace Maptz
{
    /// <summary>
    /// A service for secure console input.
    /// </summary>
    public interface ISecureConsoleInputService
    {
        /// <summary>
        /// Read a secure string.
        /// </summary>
        /// <returns></returns>
        string ReadLine();
    }
}