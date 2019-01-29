namespace NudgeApp.Data.OracleDb
{
    using System;
    using Oracle.ManagedDataAccess.Client;

    public class NudgeOracleConnection
    {
        public static string ConnectionString = "User Id=system; password=cosmin123; Data Source=localhost:1521/orcl;";

        public string Command(string cmd)
        {
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                using (OracleCommand command = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        command.BindByName = true;

                        command.CommandText = cmd;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        con.Clone();
                    }
                }
            }

            return null;
        }
    }
}
