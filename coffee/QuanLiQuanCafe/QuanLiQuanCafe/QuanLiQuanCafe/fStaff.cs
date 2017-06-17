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
    public partial class fStaff : Form
    {
        static SqlConnection connection = new SqlConnection();
        DataSet ds = new DataSet();
        public fStaff(string tk)
        {
            InitializeComponent();
            if (tk != "admin")
            {
                button2.Visible = false;
                button3.Visible = false;
                button5.Visible = false;
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
            }
            //connection.Close();
        }
        public void loadnvcombo()
        {
            DataTable dt = new DataTable();
            dt = gettable("select * from NHANVIEN");
            foreach (DataRow row in dt.Rows)
            {
                comboBox3.Items.Add(row[1].ToString());
            }
            textBox10.Text = System.DateTime.Today.Year.ToString();
            comboBox5.Text = System.DateTime.Today.Month.ToString();
        }
        private void fStaff_Load(object sender, EventArgs e)
        {
            //ConnectData.Connect();
            connection = ConnectData.connection;
            loadnv();
            loadcv();
            load();
            loadnvcombo();
        }
        public void loadcv()
        {
            comboBox1.Items.Clear();
            DataTable dt = new DataTable();
            dt = gettable("select * from CHUCVU");
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row[1].ToString());
            }
        }
        public DataTable gettable(string sql)
        {
            SqlDataAdapter adt = new SqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            return dt;
        }
        //public static void OpenSqlConnection()
        //{
        //    string connectionString = "Data Source=DESKTOP-UVQ597Q;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    //string connectionString = "Data Source=DESKTOP-IG7IPCN\\SQLEXPRESS;Initial Catalog=CoffeeManagement;" + "Integrated Security=true;";
        //    connection.ConnectionString = connectionString;
        //    connection.Open();
        //}
        public void loadnv()
        {
            SqlDataAdapter adt = new SqlDataAdapter("select * from NHANVIEN where TinhTrang=1", connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            adt.Dispose();
            dataGridView2.DataSource = dt;
        }
        public void load()
        {
            SqlDataAdapter adt = new SqlDataAdapter("select * from CHUCVU", connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            adt.Dispose();
            dataGridView1.DataSource = dt;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string MaNV = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from NHANVIEN", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "NHANVIEN");
            dt = ds.Tables["NHANVIEN"];
            if (dt.Rows.Count <= 0)
                MaNV = "SF0000001";
            else
            {
                int k;
                MaNV = "SF";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 7));
                k = k + 1;
                for (int i = 0; i < (7 - k.ToString().Length); i++)
                    MaNV = MaNV + "0";
                MaNV = MaNV + k.ToString();
            }
            textBox1.Text = MaNV;
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox7.Text = "";
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            dateTimePicker1.Value = System.DateTime.Today;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") ||(textBox2.Text=="")||(textBox4.Text=="")||(textBox5.Text=="")||(textBox6.Text==""))
                MessageBox.Show("Điền Tên Nhân Viên");
            else
            {
                SqlCommand cmd = new SqlCommand("select * from NHANVIEN", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "NHANVIEN");
                int i = 0;
                DataTable dt = ds.Tables["NHANVIEN"];
                foreach (DataRow row in dt.Rows)
                    if (textBox1.Text == row[0].ToString())
                        i = 1;
                if (i == 0)
                {
                    DataRow rw = default(DataRow);
                    rw = dt.NewRow();
                    rw[0] = textBox1.Text;
                    rw[1] = textBox2.Text;
                    if (radioButton1.Checked == true) rw[2] = radioButton1.Text;
                    else rw[2] = radioButton2.Text;
                    rw[3] = textBox5.Text;
                    rw[4] = textBox4.Text;
                    rw[5] = textBox6.Text;
                    rw[6] = dateTimePicker1.Text.Trim();
                    DataTable dt1 = new DataTable();
                    dt1 = gettable("select * from CHUCVU");
                    foreach (DataRow rw1 in dt1.Rows)
                        if (comboBox1.Text == rw1[1].ToString())
                            rw[7] = rw1[0].ToString();
                    rw[8] = true;
                    dt.Rows.Add(rw);
                    SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                    adt.Update(dt);
                }
                else
                {
                    //string gioitinh;
                    //if (radioButton1.Checked == true) gioitinh = radioButton1.Text;
                    //else gioitinh = radioButton2.Text;
                    //string sql = "update NHANVIEN " +
                    //            "set TenNV=N'" + textBox2.Text + "',GioiTinh=N'" + gioitinh +"',SDT='"+textBox5.Text+"',CMND='"+textBox4.Text+"'"+
                    //            ",DiaChi=N'"+textBox6.Text+"',NgaySinh='"+dateTimePicker1.Text+"'"+
                    //            " where MaNV='" + textBox1.Text + "'";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();

                    foreach (DataRow rw in dt.Rows)
                    {
                        if (rw[0].ToString() == textBox1.Text)
                        {
                            rw.BeginEdit();
                            rw[1] = textBox2.Text;
                            if (radioButton1.Checked == true) rw[2] = radioButton1.Text;
                            else rw[2] = radioButton2.Text;
                            rw[3] = textBox5.Text;
                            rw[4] = textBox4.Text;
                            rw[5] = textBox6.Text;
                            rw[6] = dateTimePicker1.Text;
                            DataTable dt1 = new DataTable();
                            dt1 = gettable("select * from CHUCVU");
                            foreach (DataRow rw1 in dt1.Rows)
                                if (comboBox1.Text == rw1[1].ToString())
                                    rw[7] = rw1[0].ToString();
                            rw[8] = true;
                            rw.EndEdit();
                            SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                            adt.Update(dt);
                            searchstaff();
                        }
                    }
                    
                }
                
                if (comboBox1.Text=="Bán Hàng")
                {
                    MessageBox.Show("Tạo tài khoản cho nhân viên bán hàng");
                    fAccount facc = new fAccount(textBox1.Text);
                    facc.ShowDialog();
                }
                textBox1.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
                textBox7.Text = "";
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                dateTimePicker1.Value = System.DateTime.Today;
                loadnv();
                MessageBox.Show("Lưu thành công");

                
            }
        }

        public void searchstaff()
        {
            DataTable dtnv = new DataTable();
            if (comboBox2.Text == "Họ tên")
                dtnv = gettable("select * from NHANVIEN where TinhTrang!=0 and TenNV like '%" + textBox3.Text + "%'");
            if (comboBox2.Text == "Giới Tính")
                dtnv = gettable("select * from NHANVIEN where GioiTinh like '%" + textBox3.Text + "%'");
            int stt = 0;
            listView1.Items.Clear();
            foreach (DataRow row in dtnv.Rows)
            {
                stt++;
                ListViewItem item = new ListViewItem(stt.ToString());
                ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, row[0].ToString());
                item.SubItems.Add(row[1].ToString());
                item.SubItems.Add(row[5].ToString());
                item.SubItems.Add(row[3].ToString());
                item.SubItems.Add(row[2].ToString());
                item.SubItems.Add(row[6].ToString());
                item.SubItems.Add(row[4].ToString());
                item.SubItems.Add(sub);
                listView1.Items.Add(item);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            searchstaff();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            DataTable dtnv = new DataTable();
            dtnv = gettable("select * from NHANVIEN where MaNV='" + listView1.FocusedItem.SubItems[7].Text + "'");
            textBox1.Text=dtnv.Rows[0][0].ToString();
            textBox2.Text = dtnv.Rows[0][1].ToString();
            if (radioButton2.Text== dtnv.Rows[0][2].ToString())
                radioButton2.Checked=true;
            else
                radioButton1.Checked = true;
            textBox5.Text = dtnv.Rows[0][3].ToString();
            textBox4.Text = dtnv.Rows[0][4].ToString();
            textBox6.Text = dtnv.Rows[0][5].ToString();
            dateTimePicker1.Text = dtnv.Rows[0][6].ToString();
            DataTable dt1 = new DataTable();
            dt1 = gettable("select * from CHUCVU");
            foreach (DataRow row in dt1.Rows)
                if (row[0].ToString()==dtnv.Rows[0][7].ToString())
                {
                    comboBox1.Text = row[1].ToString();
                    textBox7.Text = row[2].ToString();
                }
                    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from NHANVIEN", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "NHANVIEN");
            int i = 0;
            DataTable dt = ds.Tables["NHANVIEN"];
            foreach (DataRow row in dt.Rows)
                if (textBox1.Text == row[0].ToString())
                    i = 1;
            if (i == 1)
            {
                string sql = "update NHANVIEN " +
                                        "set TinhTrang=0" +
                                        "where MaNV='" + textBox1.Text + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                listView1.Items.Clear();
                loadnv();
                searchstaff();
                MessageBox.Show("Đã xóa nhân viên");
            }
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox2.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            dateTimePicker1.Value = System.DateTime.Today;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = gettable("select * from CHUCVU");
            foreach (DataRow row in dt.Rows)
                if (comboBox1.Text == row[1].ToString())
                    textBox7.Text = row[2].ToString();
        }

        private void fStaff_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectData.Disconnect();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string MaCV = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from CHUCVU", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "CHUCVU");
            dt = ds.Tables["CHUCVU"];
            if (dt.Rows.Count <= 0)
                MaCV = "CV001";
            else
            {
                int k;
                MaCV = "CV";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3));
                k = k + 1;
                for (int i = 0; i < (3 - k.ToString().Length); i++)
                    MaCV = MaCV + "0";
                MaCV = MaCV + k.ToString();
            }
            textBox13.Text = MaCV;
            textBox12.Text = "";
            textBox9.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox13.Text == "") || (textBox12.Text == "") || (textBox9.Text == ""))
                MessageBox.Show("Điền đủ chi tiết");
            else
            {
                SqlCommand cmd = new SqlCommand("select * from CHUCVU", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "CHUCVU");
                int i = 0;
                DataTable dt = ds.Tables["CHUCVU"];
                foreach (DataRow row in dt.Rows)
                    if (textBox13.Text == row[0].ToString())
                        i = 1;
                if (i == 0)
                {
                    DataRow rw = default(DataRow);
                    rw = dt.NewRow();
                    rw[0] = textBox13.Text;
                    rw[1] = textBox12.Text;
                    rw[2] = textBox9.Text;
                    dt.Rows.Add(rw);
                    SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                    adt.Update(dt);
                }
                else
                {
                    string sql = "update CHUCVU " +
                                "set TenCV='" + textBox12.Text + "',LuongCa=" + textBox9.Text + "" +
                                " where MaCV='" + textBox13.Text + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                textBox13.Text = "";
                textBox12.Text = "";
                textBox9.Text = "";
                load();
                MessageBox.Show("Lưu thành công");

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = new DataTable();
            dt = gettable("select * from CHUCVU where MaCV='" + dataGridView1.Rows[rowIndex].Cells[0].Value.ToString() + "'");
            textBox13.Text = dt.Rows[0][0].ToString();
            textBox12.Text = dt.Rows[0][1].ToString();
            textBox9.Text = dt.Rows[0][2].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox10.Text == "") || (comboBox3.Text == ""))
                MessageBox.Show("Nhập đủ thông tin");
            else
            {
                string manv = "";
                DataTable dt = new DataTable();
                dt = gettable("select * from NHANVIEN");
                foreach (DataRow row in dt.Rows)
                {
                    if (comboBox3.Text == row[1].ToString())
                        manv = row[0].ToString();
                }
                SqlCommand cmd = new SqlCommand("select * from LAMVIEC", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "LAMVIEC");
           
                DataTable dtlv = ds.Tables["LAMVIEC"];

                DataRow rw = default(DataRow);
                rw = dtlv.NewRow();
                rw[0] = comboBox5.Text;
                rw[1] = textBox10.Text;
                rw[2] = manv;
                rw[3] = textBox8.Text;
                dtlv.Rows.Add(rw);
                SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                adt.Update(dtlv);
                loadlv();
            }
        }
        public void loadlv()
        {
            SqlDataAdapter adt = new SqlDataAdapter("select * from LAMVIEC", connection);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            adt.Dispose();
            dataGridView3.DataSource = dt;
        }
    }
}
