namespace NudgeApp.Data.OracleDb
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using MySql.Data.MySqlClient;
    using Oracle.ManagedDataAccess.Client;

    public class NudgeOracleConnection : INudgeOracleConnection
    {
        public static string ConnectionString = "User Id=nudgeAd; password=cosmin123; Data Source=localhost:1521/orcl;";

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

        public void Test()
        {
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                con.Open();
                using (OracleCommand command = con.CreateCommand())
                {
                    try
                    {
                        var cmd = "SELECT * FROM \"SYS\".\"NUDGES\"";
                        DataTable result = new DataTable();
                        command.BindByName = true;
                        command.CommandText = cmd;
                        OracleDataReader reader = command.ExecuteReader();
                        Console.WriteLine("nudge rows: " + reader.HasRows);
                        while (reader.Read())
                        {
                            for(var i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(reader[i]);
                            }
                        }                       

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        con.Close();
                    }
                }
            }
        }
    }
}
