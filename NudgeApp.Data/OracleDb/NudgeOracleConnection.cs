namespace NudgeApp.Data.OracleDb
{
    using System;
    using System.Collections.Generic;
    using Oracle.ManagedDataAccess.Client;

    public class NudgeOracleConnection : INudgeOracleConnection
    {
        public static string ConnectionString
        {
            get
            {
#if DEBUG
                return "User Id=nudgeAd; password=cosmin123; Data Source=localhost:1521/orcl;";
#else
                return OnlineConnectionString;
#endif

            }
        }

        public static string OnlineConnectionString = "User Id=NudgeUser; password=SwnRv27H6WpNCAXG; Data Source=nudgeanonymousdatabase.cz12dzdjwlt5.eu-central-1.rds.amazonaws.com:1521/orcl;"; 

        public string InsertCommand(string cmd)
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
                        command.ExecuteNonQuery();
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

        public IList<string> SelectCommand(string cmd)
        {
            var result = new List<string>();
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                con.Open();
                using (OracleCommand command = con.CreateCommand())
                {
                    try
                    {
                        var line = "";
                        command.BindByName = true;
                        command.CommandText = cmd;
                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                line += reader[i];
                                line += " ";
                            }
                            result.Add(line);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                con.Close();
            }

            return result;
        }
    }
}
