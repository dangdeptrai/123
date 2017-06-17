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
    public partial class fTable : Form
    {
        static SqlConnection connection = new SqlConnection();
        DataSet ds = new DataSet();
        public fTable(string tk)
        {
            InitializeComponent();
            if (tk != "admin")
            {
                button2.Visible = false;
                button3.Visible = false;
                button1.Visible = false;
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
        public void addArea()
        {
            List<string> khu = new List<string>();
            DataTable dtban = new DataTable();
            dtban = getBan();
            foreach (DataRow row in dtban.Rows)
            {
                int i = 0;
                foreach (string kv in khu)
                {
                    if (row[2].ToString() == kv) i++;
                }
                if (i == 0) khu.Add(row[2].ToString());
            }
            foreach (string kv in khu)
                comboBox1.Items.Add(kv);
        }

        public DataTable gettable(string sql)
        {
            SqlDataAdapter adt = new SqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            return dt;
        }
        public DataTable getBan()
        {
            string sql = "select * from BAN";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public DataTable getBanKhuVuc(string kv)
        {
            string sql = "select * from BAN where TinhTrang!='O' and KhuVuc like N'" + kv + "'";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public void loadban()
        {
            List<string> khu = new List<string>();
            DataTable dtban = new DataTable();
            dtban = getBan();
            foreach (DataRow row in dtban.Rows)
            {
                int i = 1;
                foreach (string kv in khu)
                {
                    if (row[2].ToString() == kv) i = 0;

                }
                if (i == 1) khu.Add(row[2].ToString());
            }
            foreach (string kv in khu)
            {
                ListViewGroup group = new ListViewGroup(kv);
                dtban = getBanKhuVuc(kv);
                listView2.Groups.Add(group);
                foreach (DataRow row in dtban.Rows)
                {
                    ListViewItem item = new ListViewItem(row[1].ToString(), group);
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, row[0].ToString());
                    ListViewItem status = new ListViewItem(row[3].ToString());
                    if (status.Text == "Y")
                    {
                        item.SubItems.Add(sub);
                        item.ImageIndex = 0;
                    }
                    else
                    {
                        if (status.Text == "N")
                        {
                            item.SubItems.Add(sub);
                            item.ImageIndex = 1;
                        }
                    }
                    listView2.Items.Add(item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MaBan = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from BAN", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "BAN");
            dt = ds.Tables["BAN"];
            if (dt.Rows.Count <= 0)
                MaBan = "TB0000001";
            else
            {
                int k;
                MaBan = "TB";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 7));
                k = k + 1;
                for (int i = 0; i < (7 - k.ToString().Length); i++)
                    MaBan = MaBan + "0";
                MaBan = MaBan + k.ToString();
            }
            textBox2.Text = MaBan;
            comboBox1.Text = "";
            textBox1.Text = "";
        }

        private void fTable_Load(object sender, EventArgs e)
        {
            //ConnectData.Connect();
            connection = ConnectData.connection;
            loadban();
            addArea();
        }

        private void listView2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = gettable("select * from BAN where MaBan='" + listView2.FocusedItem.SubItems[1].Text + "'");
            textBox2.Text = listView2.FocusedItem.SubItems[1].Text;
            textBox1.Text = dt.Rows[0][1].ToString();
            comboBox1.Text = dt.Rows[0][2].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text == "") || (textBox2.Text == "") || (textBox1.Text == ""))
                MessageBox.Show("Điền đủ chi tiết bàn");
            else
            {
                SqlCommand cmd = new SqlCommand("select * from BAN", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "BAN");
                int i = 0;
                DataTable dt = ds.Tables["BAN"];
                foreach (DataRow row in dt.Rows)
                    if (textBox2.Text == row[0].ToString())
                        i = 1;
                if (i == 0)
                {
                    DataRow rw = default(DataRow);
                    rw = dt.NewRow();
                    rw[0] = textBox2.Text;
                    rw[1] = textBox1.Text;
                    rw[2] = comboBox1.Text;
                    rw[3] = "Y";
                    dt.Rows.Add(rw);
                    SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                    adt.Update(dt);
                }
                else
                {
                    string sql = "update BAN " +
                                "set TenBan='" + textBox1.Text + "',KhuVuc='" + comboBox1.Text +"'"+
                                " where MaMon='" + textBox2.Text + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                comboBox1.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";        
                listView2.Items.Clear();
                loadban();
                MessageBox.Show("Lưu thành công");
                addArea();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from BAN", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "BAN");
            int i = 0;
            DataTable dt = ds.Tables["BAN"];
            foreach (DataRow row in dt.Rows)
            {
                if (textBox2.Text == row[0].ToString())
                {
                    if (row[3].ToString() == "N") i = 2;
                    else i = 1;

                }
            }
            if (i == 1)
            {
                
                string sql = "update BAN " +
                                        "set TinhTrang='O'" +
                                        "where MaBan='" + textBox2.Text + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                listView2.Items.Clear();
                loadban();
                MessageBox.Show("Đã xóa bàn");

            }
            if (i == 2)
                MessageBox.Show("Bàn đang được đặt");
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox1.Text = "";
        }

        private void fTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectData.Disconnect();
        }
    }
}
