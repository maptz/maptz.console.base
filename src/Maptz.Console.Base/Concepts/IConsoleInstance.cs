namespace Maptz
{

    public interface IConsoleInstance
    {
        /* #region Public Methods */
        void Write(string str);
        void WriteError(string str);
        void WriteErrorLine(string str);
        void WriteLine(string str);
        void WriteWarning(string str);
        void WriteWarningLine(string str);
        /* #endregion Public Methods */
    }
}