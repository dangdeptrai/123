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
    public partial class fMon : Form
    {
        static SqlConnection connection = new SqlConnection();
        DataSet ds = new DataSet();
        public fMon(string tk)
        {
            InitializeComponent();
            if (tk != "admin")
            {
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
            //connection.Close();
        }
        
        //public static void OpenSqlConnection()
        //{
        //    string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    //string connectionString = "Data Source=MHUY-PC\\MHUY;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    connection.ConnectionString = connectionString;
        //    connection.Open();
        //}
        public DataTable gettable(string sql)
        {
            SqlDataAdapter adt = new SqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            return dt;
        }
        public DataTable getMon()
        {
            string sql = "select * from MON";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public DataTable getMonTheoLoai(string lm)
        {
            string sql = "select * from MON where TinhTrang=1 and LoaiMon like N'" + lm + "'";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        public void loadmon()
        {
            
            List<string> loaimon = new List<string>();
            DataTable dt = new DataTable();
            dt = getMon();
            foreach (DataRow row in dt.Rows)
            {
                int i = 1;
                foreach (string lm in loaimon)
                    if (row[2].ToString() == lm) i = 0;
                if (i == 1) loaimon.Add(row[2].ToString());
            }
            int stt;
            foreach (string lm in loaimon)
            {
                stt = 1;
                ListViewGroup group = new ListViewGroup(lm);
                dt = getMonTheoLoai(lm);
                listView1.Groups.Add(group);

                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(stt.ToString(), group);
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, row[0].ToString());
                    item.SubItems.Add(sub);
                    item.SubItems.Add(row[1].ToString());
                    listView1.Items.Add(item);
                    stt++;
                }
            }
            
        }
        public void addLoai()
        {
            List<string> loai = new List<string>();
            DataTable dtmon = new DataTable();
            dtmon = getMon();
            foreach (DataRow row in dtmon.Rows)
            {
                int i = 1;
                foreach (string l in loai)
                {
                    if (row[2].ToString() == l) i = 0;

                }
                if (i == 1) loai.Add(row[2].ToString());
            }
            foreach (string l in loai)
                comboBox1.Items.Add(l);
        }
        public void addDonvi()
        {
            List<string> donvi = new List<string>();
            DataTable dtmon = new DataTable();
            dtmon = getMon();
            foreach (DataRow row in dtmon.Rows)
            {
                int i = 1;
                foreach (string dv in donvi)
                {
                    if (row[3].ToString() == dv) i = 0;

                }
                if (i == 1) donvi.Add(row[3].ToString());
            }
            foreach (string dv in donvi)
                comboBox1.Items.Add(dv);
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
            //ConnectData.Connect();
            connection = ConnectData.connection;
            loadmon();
            addDonvi();
            addLoai();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = gettable("select * from MON where MaMon='" + listView1.FocusedItem.SubItems[1].Text+"'");
            textBox3.Text = dt.Rows[0][0].ToString();
            txbUserName.Text = dt.Rows[0][1].ToString();
            comboBox1.Text = dt.Rows[0][2].ToString();
            comboBox2.Text = dt.Rows[0][3].ToString();
            textBox1.Text = dt.Rows[0][4].ToString();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MaMon="";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from MON", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "MON");
            dt = ds.Tables["MON"];
            if (dt.Rows.Count <= 0)
                MaMon = "MM0000001";
            else
            {
                int k;
                MaMon = "MM";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 7));
                k = k + 1;
                for (int i = 0; i < (7 - k.ToString().Length); i++)
                    MaMon = MaMon + "0";
                MaMon = MaMon + k.ToString();
            }
            textBox3.Text = MaMon;
            txbUserName.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox3.Text == "")||(comboBox1.Text == "") ||(comboBox2.Text == "") || (textBox1.Text == "")||(txbUserName.Text==""))
                MessageBox.Show("Điền đủ chi tiết món");
            else
            {
                SqlCommand cmd = new SqlCommand("select * from MON", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "MON");
                int i = 0;
                DataTable dt = ds.Tables["MON"];
                foreach (DataRow row in dt.Rows)
                    if (textBox3.Text == row[0].ToString())
                        i = 1;
                if (i == 0)
                {
                    DataRow rw = default(DataRow);
                    rw = dt.NewRow();
                    rw[0] = textBox3.Text;
                    rw[1] = txbUserName.Text;
                    rw[2] = comboBox1.Text;
                    rw[3] = comboBox2.Text;
                    rw[4] = textBox1.Text;
                    rw[5] = true;
                    dt.Rows.Add(rw);
                    SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                    adt.Update(dt);
                }
                else
                {
                    string sql = "update MON " +
                                "set TenMon='" + txbUserName.Text + "',LoaiMon=N'" + comboBox1.Text + "',DonVi='" + comboBox2.Text + "',Gia=" + textBox1.Text +
                                " where MaMon='" + textBox3.Text + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                textBox3.Text="";
                txbUserName.Text="";
                comboBox1.Text="";
                comboBox2.Text="";
                textBox1.Text="";               
                listView1.Items.Clear();
                loadmon();
                MessageBox.Show("Lưu thành công");
                addDonvi();
                addLoai();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from MON", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "MON");
            int i = 0;
            DataTable dt = ds.Tables["MON"];
            foreach (DataRow row in dt.Rows)
                if (textBox3.Text == row[0].ToString())
                    i = 1;
            if (i == 1)
            {
                string sql = "update MON " +
                                        "set TinhTrang=0" +
                                        "where MaMon='" + textBox3.Text + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();             
                listView1.Items.Clear();
                loadmon();
                MessageBox.Show("Đã xóa món");

            }
            
            textBox3.Text = "";
            txbUserName.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox1.Text = "";
            
        }

        private void fMon_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectData.Disconnect();
        }

  
    }
}
