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
    public partial class fChiTieu : Form
    {
        static SqlConnection connection = new SqlConnection();
        DataSet ds = new DataSet();
        //public static void OpenSqlConnection()
        //{
        //    string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    //string connectionString = "Data Source=DESKTOP-IG7IPCN\\SQLEXPRESS;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    connection.ConnectionString = connectionString;
        //    connection.Open();
        //}
        public fChiTieu()
        {
            InitializeComponent();
        }
        public void loadct()
        {
            SqlDataAdapter adt = new SqlDataAdapter("select * from CHITIEU", connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            adt.Dispose();
            dataGridView1.DataSource = dt;
        }
        
        private void btNhap_phieu_chi_Click(object sender, EventArgs e)
        {
            string MaCT = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from CHITIEU", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "CHITIEU");
            dt = ds.Tables["CHITIEU"];
            if (dt.Rows.Count <= 0)
                MaCT = "CT0000001";
            else
            {
                int k;
                MaCT = "CT";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 7));
                k = k + 1;
                for (int i = 0; i < (7 - k.ToString().Length); i++)
                    MaCT = MaCT + "0";
                MaCT = MaCT + k.ToString();
            }

            tbMa_chi_tieu.Text = MaCT;
            tbLi_do_chi.Text = "";
            tbTong_tien_chi.Text = "";
                    
            dtpNgay_chi_tieu.Value = System.DateTime.Today;
        }

        private void grbPhiêuChi_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((tbLi_do_chi.Text == "") || (tbTong_tien_chi.Text == ""))
                MessageBox.Show("Điền đủ chi tiết chi");
            else
            {
                SqlCommand cmd = new SqlCommand("select * from CHITIEU", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "CHITIEU");
     
                DataTable dt = ds.Tables["CHITIEU"];
                DataRow rw = default(DataRow);
                rw = dt.NewRow();
                rw[0] = tbMa_chi_tieu.Text;
                rw[1] = tbLi_do_chi.Text;
                rw[2] = dtpNgay_chi_tieu.Value;
                rw[3] = tbTong_tien_chi.Text;
                dt.Rows.Add(rw);
                SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                adt.Update(dt);
                MessageBox.Show("Lưu thành công");
                loadct();
                tbLi_do_chi.Text = "";
                tbTong_tien_chi.Text = "";
                tbMa_chi_tieu.Text = "";
       


            }
        }

        private void fChiTieu_Load(object sender, EventArgs e)
        {
            //ConnectData.Connect();
            connection = ConnectData.connection;
            loadct();
        }

        private void fChiTieu_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectData.Disconnect();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
