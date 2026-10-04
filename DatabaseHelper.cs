using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OES
{
    public static class DatabaseHelper
    {
        private const string DefaultConnectionString = "Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True";

        public static string GetConnectionString()
        {
            try
            {
                var csSetting = ConfigurationManager.ConnectionStrings["OnlineExamDB"];
                if (csSetting != null && !string.IsNullOrWhiteSpace(csSetting.ConnectionString))
                {
                    return csSetting.ConnectionString;
                }
            }
            catch
            {
                // Fallback if ConfigurationManager fails
            }
            return DefaultConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
