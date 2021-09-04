using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp16
{
    class DB
    {
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-NVP6N3J4;Initial Catalog=MYDB;Integrated Security=True");
        public void openconnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeconnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public SqlConnection getconnection()
        {
            return connection;
        }
    }
}
