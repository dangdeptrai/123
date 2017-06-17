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
    public partial class fLogin : Form
    {
        private void login()
        {
            ds = new DataSet();
            SqlCommand cmd = new SqlCommand("select * from TAIKHOAN", ConnectData.connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "TAIKHOAN");
            dt = ds.Tables["TAIKHOAN"];
            adt.Dispose();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i = 0;
                if ((row[0].ToString() == txbUserName.Text) && (row[1].ToString() == txbPassWord.Text))
                {

                    fTableManager f = new fTableManager(row[0].ToString().ToString(), row[2].ToString().ToString());
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    txbPassWord.Clear();
                    txbUserName.Clear();
                    i = 1;
                    break;
                }
            }
            if(i!=1)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK);
                txbPassWord.Clear();
                txbUserName.Clear();
            }

        }
        DataSet ds;
        DataTable dt;
        public fLogin()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectData.Connect();
            
            
        }

        //private static void OpenSqlConnection()
        //{
        //    string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    //string connectionString = "Data Source=MHUY-PC\\MHUY;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    connection.ConnectionString = connectionString;
        //    connection.Open();
        //}
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không ?","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                ConnectData.Disconnect();
                e.Cancel = true;
            }
        }

        private void txbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                login();
            }
        }

        private void txbPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                login();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
