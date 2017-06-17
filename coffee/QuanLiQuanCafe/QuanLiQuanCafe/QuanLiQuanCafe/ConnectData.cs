using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLiQuanCafe
{
    class ConnectData
    {
        static public SqlConnection connection = new SqlConnection();
        static public void Connect()
        {
            string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
           // string connectionString = "Data Source=MHUY-PC\\MHUY;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
            connection.ConnectionString = connectionString;
            connection.Open();
        }
        static public void Disconnect()
        {
            connection.Close();
        }
    }
}
