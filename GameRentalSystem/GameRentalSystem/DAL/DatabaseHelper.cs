using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GameRentalSystem.DAL
{
    public static class DatabaseHelper
    {
        private static string GetConnectionString()
        {
            // Reads connection string from App.config
            return ConfigurationManager.ConnectionStrings["GameRentalDB"].ConnectionString;
        }

        // Executes a non-query command (INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string commandText, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Executes a command that returns a single scalar value
        public static object ExecuteScalar(string commandText, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        // Executes a command that returns a data reader (SELECT)
        // It's the caller's responsibility to close the reader and connection
        public static SqlDataReader ExecuteReader(string commandText, SqlParameter[] parameters = null)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                connection.Open();
                // CommandBehavior.CloseConnection closes the connection when the reader is closed
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }

        // Helper to get data into a DataTable (useful for populating DataGrids)
        public static System.Data.DataTable ExecuteDataTable(string commandText, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        connection.Open();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}