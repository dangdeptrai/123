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
    public partial class fChangePW : Form
    {
        DataSet ds = new DataSet();
        static SqlConnection connection = new SqlConnection();
        public fChangePW(string tk)
        {
            InitializeComponent();
            if (tk != "admin")
                tabControl1.TabPages.Remove(tabPage2);
            textBox1.Text = tk;
            
        }
        string TK;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TK = listView1.FocusedItem.SubItems[0].Text;
        }
        public void changepw()
        {
            if ((textBox2.Text == "") || (textBox3.Text == ""))
            {
                MessageBox.Show("Nhập mặt khẩu mới");
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Nhập  lại mặt khẩu không đúng");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from TAIKHOAN", connection);
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    adt.Fill(ds, "TAIKHOAN");
                    string sql = "update TAIKHOAN " +
                                 "set PW='" + textBox2.Text + "' " +
                                 "where ID='" + textBox1.Text + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đổi mặt khẩu thành công");
                    this.Close();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changepw();       
        }

        private void fChangePW_Load(object sender, EventArgs e)
        {
            connection = ConnectData.connection;
            loadtk();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                changepw();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                changepw();
            }
        }
        public void loadtk()
        {
            string sql = "select ID,PW,TenNV from TAIKHOAN,NHANVIEN where TAIKHOAN.MaNV=NHANVIEN.MaNV and TinhTrang=1";
            SqlDataAdapter adt = new SqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                item.SubItems.Add(row[1].ToString());
                item.SubItems.Add(row[2].ToString());
                listView1.Items.Add(item);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bán muốn reset mật khẩu của tài khoản: "+TK, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                SqlCommand cmd = new SqlCommand("select * from TAIKHOAN", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "TAIKHOAN");
                string sql = "update TAIKHOAN " +
                             "set PW='1' " +
                             "where ID='" + TK + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reset mặt khẩu thành công");
            }
        }
    }
}
