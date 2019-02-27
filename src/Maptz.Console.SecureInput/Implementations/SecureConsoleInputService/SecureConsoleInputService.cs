using System;

namespace Maptz
{
    /// <summary>
    /// A service for reading secure console input.
    /// </summary>
    public class SecureConsoleInputService : ISecureConsoleInputService
    {
        /// <summary>
        /// Read the console input. 
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            var password = string.Empty;
            password = MaskedConsole.ReadPassword();
            return password;
        }
    }
}