using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLiQuanCafe
{
    
    class ConnectDTB
    {

        //string connectionString = "Data Source=MHUY-PC\\MHUY;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        public static class Connect
        {
            public static void connect()
            {
                SqlConnection connection = new SqlConnection();
                string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
                connection.ConnectionString = connectionString;
            }
        }
    }
}
