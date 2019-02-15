namespace NudgeApp.Data.OracleDb
{
    using System.Collections.Generic;

    public interface INudgeOracleConnection
    {
        string InsertCommand(string command);
        IList<string> SelectCommand(string cmd);
    }
}