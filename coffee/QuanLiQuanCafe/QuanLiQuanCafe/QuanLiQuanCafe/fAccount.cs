using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLiQuanCafe
{
    public partial class fAccount : Form
    {
        static SqlConnection connection = new SqlConnection();
        DataSet ds = new DataSet();
        string Manv;
        public fAccount(string MaNV)
        {
            InitializeComponent();
            Manv = MaNV;
            //connection.Close();
        }
        //public static void OpenSqlConnection()
        //{
        //    string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    //string connectionString = "Data Source=MHUY-PC\\MHUY;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    connection.ConnectionString = connectionString;
        //    connection.Open();
        //}
        public void finish()
        {
            if ((textBox2.Text != "") && (textBox3.Text != ""))
            {
                if (textBox2.Text != textBox3.Text)
                    MessageBox.Show("Nhập lại mật khẩu không đúng");
                else
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("select * from TAIKHOAN", connection);
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    adt.Fill(ds, "TAIKHOAN");
                    dt = ds.Tables["TAIKHOAN"];
                    DataRow rw = default(DataRow);
                    rw = dt.NewRow();
                    rw[0] = textBox1.Text;
                    rw[1] = textBox2.Text;
                    rw[2] = Manv;
                    dt.Rows.Add(rw);
                    SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                    adt.Update(dt);
                    this.Close();
                }
            }
            else
                MessageBox.Show("Nhập mật khẩu");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            finish();
        }
        public void loadtk()
        {
            string ID = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from TAIKHOAN", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "TAIKHOAN");
            dt = ds.Tables["TAIKHOAN"];
            if (dt.Rows.Count <= 1)
                ID = "NV001";
            else
            {
                int k;
                ID = "NV";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 7));
                k = k + 1;
                for (int i = 0; i < (3 - k.ToString().Length); i++)
                    ID = ID + "0";
                ID = ID + k.ToString();
            }
            textBox1.Text = ID;
        }
        private void fAccount_Load(object sender, EventArgs e)
        {
            //ConnectData.Connect();
            connection = ConnectData.connection;
            loadtk();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                finish();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                finish();
            }
        }

        private void fAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectData.Disconnect();
        }
    }
}
