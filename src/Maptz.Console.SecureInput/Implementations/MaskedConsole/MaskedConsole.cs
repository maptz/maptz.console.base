using System.Collections.Generic;
using System.Linq;

namespace Maptz
{
    /// <summary>
    /// A masked console. 
    /// </summary>
    public static class MaskedConsole
    {
        /* #region Public Static Fields */
        public static int[] FilteredChars = { 0, 27, 9, 10 };
        /* #endregion Public Static Fields */
        /* #region Public Static Methods */

        /// <summary>
        /// Like System.Console.ReadLine(), only with a mask.
        /// </summary>
        /// <param name="mask">a <c>char</c> representing your choice of console mask</param>
        /// <returns>the string the user typed in </returns>
        public static string ReadPassword(char mask)
        {

            var retvalStack = new Stack<char>();
            char currentChar = (char)0;
            //Keep reading characters
            while ((currentChar = System.Console.ReadKey(true).KeyChar) != EnterChar)
            {

                /* #region Backspace */
                if (currentChar == BackspaceChar)
                {
                    if (retvalStack.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        retvalStack.Pop();
                    }
                }
                /* #endregion*/
                /* #region Ctrl+Backspace */
                else if (currentChar == CtrlBackspaceChar)
                {
                    while (retvalStack.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        retvalStack.Pop();
                    }
                }
                /* #endregion*/
                /* #region Filtered chars */
                else if (FilteredChars.Count(x => currentChar == x) > 0) { }
                /* #endregion*/
                /* #region Others */
                else
                {
                    retvalStack.Push((char)currentChar);
                    System.Console.Write(mask);
                }
                /* #endregion*/

            }

            System.Console.WriteLine();

            return new string(retvalStack.Reverse().ToArray());

        }

        /// <summary>
        /// Like System.Console.ReadLine(), only with a mask.
        /// </summary>
        /// <returns>the string the user typed in </returns>
        public static string ReadPassword()
        {
            return MaskedConsole.ReadPassword('*');
        }
        /* #endregion Public Static Methods */
        /* #region Public Fields */
        public const int BackspaceChar = 8;
        public const int CtrlBackspaceChar = 127;
        public const int EnterChar = 13;
        /* #endregion Public Fields */
    }

}
