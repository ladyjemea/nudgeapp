namespace NudgeApp.Data.OracleDb
{
    public interface INudgeOracleConnection
    {
        string Command(string command);
        void Test();
    }
}