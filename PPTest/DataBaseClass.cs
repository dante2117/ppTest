using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PPTest
{
    public class DataBaseClass
    {
        public static string Users_ID = "null", Password = "null", App_Name = "Администратор";
        public static string ConnectionStrig = "Data Source = DESKTOP-DNTIAC2\\CAT; Initial Catalog = Test; Persist Security Info = true; User ID = {0}; Password = '{1}';";
        public SqlConnection connection = new SqlConnection(ConnectionStrig);
        private SqlCommand command = new SqlCommand();
        public DataTable resultTable = new DataTable();
        public SqlDependency dependency = new SqlDependency();
        public enum act { select, manipulation };
        public void sqlExecute(string quety, act act)
        {
            command.Connection = connection;
            command.CommandText = quety;
            command.Notification = null;
            switch (act)
            {
                case act.select:
                    dependency.AddCommandDependency(command);
                    SqlDependency.Start(connection.ConnectionString);
                    connection.Open();
                    resultTable.Load(command.ExecuteReader());
                    connection.Close();
                    break;
                case act.manipulation:
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    break;
            }
        }
    }
}
