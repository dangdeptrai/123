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
    public partial class fSell : Form
    {
        static SqlConnection connection = new SqlConnection();
        DataSet ds= new DataSet();
        string MaHDChuaThanhToan="";
        int tao = 0;
        string MaNV="";
        int hoanthanh = 0;
        public fSell(string manv)
        {
            InitializeComponent();
            comboBox1.Enabled = false;
            MaNV = manv;
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
            SqlDataAdapter adt = new SqlDataAdapter(sql,connection);
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
            string sql = "select * from BAN where TinhTrang!='O' and KhuVuc like N'"+kv+"'";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public DataTable getMon()
        {
            string sql = "select * from MON where TinhTrang!=0";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public void loadHD()
        {
            SqlDataAdapter adt = new SqlDataAdapter("select * from HOADON where TongTien != 0 order by MaHD desc", connection);
  
            DataTable dt = new DataTable();
            adt.Fill(dt);
            adt.Dispose();
            dataGridView1.DataSource = dt;      
                  
        }
        public DataTable getHDChuaThanhToan()
        {
            string sql = "select MaHD,BAN.MaBan,TenBan,NgayHD,TongTien from HOADON,BAN where BAN.MaBan=HOADON.MaBan and TinhTrang!='O' and DaThanhToan=0 and TongTien!=0 order by MaHD desc";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public void addTable()
        {
            comboBox1.Items.Clear();
            DataTable dtban = new DataTable();
            dtban = getBan();
            foreach (DataRow row in dtban.Rows)
            {
                if (row[3].ToString()== "Y")
                    comboBox1.Items.Add(row[1].ToString());
            }
        }


        private void fSell_Load(object sender, EventArgs e)
        {
            //ConnectData.Connect();
            connection = ConnectData.connection;
            loadban();
            loadmon();
            addTable();
            loadHD();
            loadHDChuaThanhToan();
        }
        public DataTable getMonTheoLoai(string lm)
        {
            string sql = "select * from MON where TinhTrang!=0 and LoaiMon like N'"+lm+"'";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public void loadmon()
        {
            List<string> loaimon = new List<string>();
            DataTable dtmon = new DataTable();
            dtmon = getMon();
            foreach (DataRow row in dtmon.Rows)
            {
                int i = 1;
                foreach (string lm in loaimon)
                    if (row[2].ToString() == lm) i = 0;
                if (i == 1) loaimon.Add(row[2].ToString());
            }
            foreach (string lm in loaimon)
            {
                ListViewGroup group = new ListViewGroup(lm);
                dtmon = getMonTheoLoai(lm);
                listView1.Groups.Add(group);
                foreach (DataRow row in dtmon.Rows)
                {
                    comboBox2.Items.Add(row[1].ToString());
                    ListViewItem item = new ListViewItem(row[1].ToString(),group);
                    ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, row[0].ToString());
                    item.SubItems.Add(row[4].ToString());
                    item.SubItems.Add(row[3].ToString());
                    item.SubItems.Add(subitem);
                    listView1.Items.Add(item);
                }
            }
            
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
        public void loadHDChuaThanhToan()
        {
            DataTable dtHDChuaThanhToan = new DataTable();
            dtHDChuaThanhToan = getHDChuaThanhToan();
            int stt = 1;
            foreach (DataRow row in dtHDChuaThanhToan.Rows)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(row[3].ToString());
                item.SubItems.Add(row[2].ToString());
                item.SubItems.Add(row[4].ToString());
                item.SubItems.Add(row[0].ToString());
                listView4.Items.Add(item);
                stt++;
            }
            
            listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = listView1.FocusedItem.SubItems[0].Text;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Enabled == true)
            {
                if (listView2.FocusedItem.ImageIndex == 0)
                {
                    label9.Text = "";
                    if (tao - hoanthanh == 1)
                    {
                        comboBox1.Text = listView2.FocusedItem.SubItems[0].Text;
                        comboBox2.Text = "";
                        listView3.Items.Clear();
                        label3.Text = "";
                    }
                    else
                    {
                        comboBox1.Enabled = false;
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        listView3.Items.Clear();
                        label3.Text = "";
                    }
                    }
                else
                {
                    hoanthanh = tao;
                    label9.Text = listView2.FocusedItem.SubItems[0].Text;
                    comboBox1.Text = "";
                    listView3.Items.Clear();
                    SqlDataAdapter adt = new SqlDataAdapter("select TongTien,MaHD from HOADON where DaThanhToan=0 and MaBan='" + listView2.FocusedItem.SubItems[1].Text + "'", connection);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);
                    adt.Dispose();
                    label3.Text = float.Parse(dt.Rows[0][0].ToString()).ToString();
                    loadCTHD(dt.Rows[0][0].ToString());
                    MaHDChuaThanhToan = dt.Rows[0][1].ToString();
                    loadCTHD(MaHDChuaThanhToan);
                }
            }
            else
            {
                if (listView2.FocusedItem.ImageIndex == 1)
                {
                    label9.Text = listView2.FocusedItem.SubItems[0].Text;
                    comboBox1.Text = "";
                    listView3.Items.Clear();
                    SqlDataAdapter adt = new SqlDataAdapter("select TongTien,MaHD from HOADON where DaThanhToan=0 and MaBan='" + listView2.FocusedItem.SubItems[1].Text + "'", connection);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);
                    adt.Dispose();
                    label3.Text = float.Parse(dt.Rows[0][0].ToString()).ToString();
                    loadCTHD(dt.Rows[0][0].ToString());
                    MaHDChuaThanhToan = dt.Rows[0][1].ToString();
                    loadCTHD(MaHDChuaThanhToan);
                    comboBox1.Enabled = true;
                }
                else
                {
                    
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    listView3.Items.Clear();
                    label3.Text = "";
                    label9.Text = "";
                }
            }
        }
        public string taoHD()
        {
            string MaHD;
            SqlCommand cmd = new SqlCommand("select * from HOADON", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "HOADON");
            DataTable dt = ds.Tables["HOADON"];

            if (dt.Rows.Count <= 0)
            {
                MaHD = "HD0000001";
            }
            else
            {
                int k;
                MaHD = "HD";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 7));
                k = k + 1;
                for (int i = 0; i < (7 - k.ToString().Length); i++)
                    MaHD = MaHD + "0";
                MaHD = MaHD + k.ToString();
            }
            DataRow rw = default(DataRow);
            rw = dt.NewRow();
            rw[0] = MaHD;
            if (MaNV == "") rw[1] = null;
            else rw[1] = MaNV;
            foreach (ListViewItem lvitem in listView2.SelectedItems)
            {
                if (comboBox1.Text == lvitem.SubItems[0].Text)
                    rw[2] = lvitem.SubItems[1].Text;
            }
            rw[3] = System.DateTime.Now;
            rw[4] = "0";
            rw[5] = false;
            dt.Rows.Add(rw);
            SqlCommandBuilder cb = new SqlCommandBuilder(adt);
            adt.Update(dt);
            dt.Rows.Clear();
            loadHD();
            return MaHD;
        }

        public DataTable getCTHD(string MaHD)
        {
            string sql = "select MaHD,CTHD.MaMon,TenMon,SoLuong,ThanhTien from CTHD,MON where CTHD.MaMon=MON.MaMON and MaHD='"+MaHD+"'";
            DataTable dt = new DataTable();
            dt = gettable(sql);
            return dt;
        }
        public void loadCTHD(string MaHD)
        {
            DataTable dtCTHD = new DataTable();
            dtCTHD = getCTHD(MaHD);
            int stt = 1;
            foreach (DataRow row in dtCTHD.Rows)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, row[0].ToString());
                item.SubItems.Add(row[2].ToString());
                item.SubItems.Add(row[3].ToString());
                item.SubItems.Add(row[4].ToString());
                item.SubItems.Add(sub);
                listView3.Items.Add(item);
                stt++;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            string MaHD;
            MaHD = "";
            if ((comboBox1.Text == "")&&(label9.Text=="")) MessageBox.Show("Chọn bàn Trống");
            else
            {
                if (comboBox2.Text == "")
                    MessageBox.Show("Chọn món");
                else
                {
                    if (listView3.Items.Count == 0)
                        if (label9.Text == "") MaHD = taoHD();
                        else MaHD = MaHDChuaThanhToan;
                    else MaHD = listView3.Items[0].SubItems[4].Text;
                    SqlCommand cmd = new SqlCommand("select * from CTHD where MaHD='"+MaHD+"'", connection);
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    adt.Fill(ds, "CTHD");
                    DataTable dt = new DataTable();
                    dt = ds.Tables["CTHD"];
                    DataTable dtmon = getMon();
                    DataRow rw = default(DataRow);
                    string test = "";
                    foreach (DataRow rowmon in dtmon.Rows)
                        if (rowmon[1].ToString() == comboBox2.Text)
                            test = rowmon[0].ToString();
                    int i = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((row[1].ToString() == test)&&(row[0].ToString()==MaHD))
                            i = 0;
                    }

                    if (listView3.Items.Count == 0) i = 1;
                        
                    if (i == 0)
                    {
                        string thanhtien="";
                        foreach (DataRow row in dtmon.Rows)
                            if (row[0].ToString() == test)
                                thanhtien = (Convert.ToInt16(numericUpDown1.Value) * float.Parse(row[4].ToString())).ToString();
                        string sql="update CTHD "+
                                    "set SoLuong=SoLuong+"+numericUpDown1.Value.ToString()+",ThanhTien=ThanhTien+   "+thanhtien+
                                    " where MaHD='"+MaHD+"' and MaMon='"+test+"'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        rw = dt.NewRow();
                        rw[0] = MaHD;
                        rw[1] = test;
                        rw[2] = numericUpDown1.Value;
                        foreach (DataRow row in dtmon.Rows)
                            if (row[0].ToString() == test)
                                 rw[3]= (Convert.ToInt32(numericUpDown1.Value)*float.Parse(row[4].ToString())).ToString();
                        dt.Rows.Add(rw);
                        SqlCommandBuilder cb = new SqlCommandBuilder(adt);
                        adt.Update(dt);
                    }
                    listView3.Items.Clear();
                    comboBox2.Text = "";
                    numericUpDown1.Value = 1;
                    loadCTHD(MaHD);
                    float sum ;
                    sum = 0;
                    SqlDataAdapter adt1 = new SqlDataAdapter("select * from CTHD where MaHD='"+MaHD+"'", connection);

                    DataTable dt1 = new DataTable();
                    adt1.Fill(dt1);
                    adt.Dispose();
                    foreach (DataRow row in dt1.Rows)
                        sum = sum + float.Parse(row[3].ToString());
                    label3.Text = sum.ToString() ;
                    if (label9.Text != "")
                    {
                        SqlCommand cmd2 = new SqlCommand("select * from HOADON", connection);
                        SqlDataAdapter adt2 = new SqlDataAdapter(cmd2);
                        adt2.Fill(ds, "HOADON");
                        listView4.Items.Clear();
                        string sql = "update HOADON" +
                        " set TONGTIEN=" + sum.ToString() +
                        "where MaHD='" + MaHD + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        loadHDChuaThanhToan();
                        loadHD();
                    }
                    
                }
            }
        }

        public void CreateHD()
        {
            string sql;
            sql = "select HOADON.MaHD,ThanhTien,TongTien,MaBan from HOADON,CTHD where HOADON.MaHD=CTHD.MaHD and TongTien=0";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from HOADON",connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "HOADON");
            SqlDataAdapter adt1 = new SqlDataAdapter(sql, connection);

            DataTable dt1 = new DataTable();
            adt1.Fill(dt1);
            adt1.Dispose();
       
            float sum = 0;
            foreach (DataRow row in dt1.Rows)
                sum = sum + float.Parse(row[1].ToString());
            sql = "update HOADON" +
                    " set TONGTIEN=" + sum.ToString()+
                    "where MaHD='"+dt1.Rows[0][0].ToString()+"'";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            
        }
        public void updatetinhtrangban()
        {
            string sql;
            sql = "select BAN.MaBan from BAN,HOADON where HOADON.MaBan=BAN.MaBan and TongTien=0";
            if (label9.Text!="") sql= "select BAN.MaBan from BAN,HOADON where HOADON.MaBan=BAN.MaBan and MaHD ='"+MaHDChuaThanhToan+"'";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from BAN", connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(ds, "BAN");
            dt = gettable(sql);
            sql = "select * from BAN";
            SqlDataAdapter adt1 = new SqlDataAdapter(sql, connection);

            DataTable dt1 = new DataTable();
            adt1.Fill(dt1);
            adt1.Dispose();
            string tt = "";
            foreach (DataRow row in dt1.Rows)
                if (row[3].ToString()!="O")
                if (row[0].ToString() == dt.Rows[0][0].ToString())
                    if (row[3].ToString() == "Y")
                        tt = "N";
                    else
                        tt = "Y";

            sql = "update BAN" +
                    " set TinhTrang='"+tt+"'" +
                    " where MaBan='" + dt.Rows[0][0].ToString() + "'";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (tao - hoanthanh == 1) hoanthanh++;
            if (comboBox1.Text == "")
            {
                if (listView3.Items.Count == 0)
                    MessageBox.Show("Chọn Bàn Trống Và Thêm Món");
                else
                    MessageBox.Show("Hóa đơn đã được tạo");
            }
            else
            {
                if (listView3.Items.Count == 0) MessageBox.Show("Hóa đơn chưa có món");
                else
                {
                    listView4.Items.Clear();                    
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    listView3.Items.Clear();                  
                    label9.Text = "";
                    label3.Text = "";           
                    updatetinhtrangban();
                    CreateHD();
                    loadHD();
                    loadHDChuaThanhToan();
                    listView2.Items.Clear();
                    listView2.Groups.Clear();
                    loadban();
                    addTable();
                    comboBox1.Enabled = false;
                    MessageBox.Show("Tạo thành công");

                }
                
            }
            
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tao == hoanthanh) || (listView3.Items.Count == 0))
            {
                hoanthanh = tao;

                comboBox1.Enabled = true;
                comboBox1.Text = "";
                comboBox2.Text = "";
                label9.Text = listView4.FocusedItem.SubItems[2].Text;
                listView3.Items.Clear();
                loadCTHD(listView4.FocusedItem.SubItems[4].Text);
                SqlDataAdapter adt = new SqlDataAdapter("select TongTien from HOADON where MaHD='" + listView4.FocusedItem.SubItems[4].Text + "'", connection);

                DataTable dt = new DataTable();
                adt.Fill(dt);
                adt.Dispose();
                label3.Text = float.Parse(dt.Rows[0][0].ToString()).ToString();
                MaHDChuaThanhToan = listView4.FocusedItem.SubItems[4].Text;

            }
            else MessageBox.Show("Đang tạo hóa đơn");
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Enabled == true) && (label9.Text == ""))
            {
                MessageBox.Show("Đang tạo hóa đơn mới");
            }
            else
            {
                if (tao == hoanthanh) tao++;
                comboBox1.Enabled = true;
                comboBox1.Text = "";
                comboBox2.Text = "";
                listView3.Items.Clear();
                label3.Text = "";
                label9.Text = "";
                MessageBox.Show("Chọn bàn Trống và chọn món");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label9.Text != "")
            {
                comboBox1.Enabled = false;
                SqlCommand cmd = new SqlCommand("select TongTien from HOADON ", connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(ds, "BAN");
                //adt.Dispose();
                string sql = "update HOADON" +
                             " set DaThanhToan=1" +
                            " where MaHD='" + MaHDChuaThanhToan + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                updatetinhtrangban();
                listView4.Items.Clear();
                loadHDChuaThanhToan();
                loadHD();
                string t = label9.Text;
                label3.Text = "";
                label9.Text = "";
                listView3.Items.Clear();
                listView2.Items.Clear();
                listView2.Groups.Clear();
                loadban();
                MessageBox.Show("Đã Thanh Toán " + t);
            }
        }

        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView3.FocusedItem==null)
                MessageBox.Show("Chọn món trong chi tiết cần xóa!");
            else
            {
                SqlDataAdapter adt = new SqlDataAdapter("select MaMon from MON where TinhTrang=1 and TenMon= N'" + listView3.FocusedItem.SubItems[1].Text + "'", connection);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                SqlCommand cmd = new SqlCommand("select * from CTHD ", connection);
                SqlDataAdapter adt1 = new SqlDataAdapter(cmd);
                adt.Fill(ds, "CTHD");
                string sql = "delete from CTHD" +
                            " where MaHD='" + listView3.FocusedItem.SubItems[4].Text + "' and MaMon='"+dt.Rows[0][0].ToString()+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                string ct = listView3.FocusedItem.SubItems[4].Text;
                
                float sum;
                sum = 0;
                SqlDataAdapter adt2 = new SqlDataAdapter("select * from CTHD where MaHD='" + listView3.FocusedItem.SubItems[4].Text + "'", connection);
                DataTable dt2 = new DataTable();
                adt2.Fill(dt2);
                adt2.Dispose();
                foreach (DataRow row in dt2.Rows)
                    sum = sum + float.Parse(row[3].ToString());
                label3.Text = sum.ToString();
                if (label9.Text != "")
                {
                    SqlCommand cmd2 = new SqlCommand("select * from HOADON", connection);
                    adt2 = new SqlDataAdapter(cmd2);
                    adt2.Fill(ds, "HOADON");
                    listView4.Items.Clear();
                    sql = "update HOADON" +
                    " set TONGTIEN=" + sum.ToString() +
                    "where MaHD='" + listView3.FocusedItem.SubItems[4].Text + "'";
                    cmd2.CommandText = sql;
                    cmd2.ExecuteNonQuery();
                    loadHDChuaThanhToan();
                    loadHD();
                                       
                }
                listView3.Items.Clear();
                loadCTHD(ct);
            }
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label9.Text != "")
            {
                if (comboBox1.Text == "") MessageBox.Show("Chọn bàn trống");
                else
                {
                    updatetinhtrangban();
                    SqlDataAdapter adt = new SqlDataAdapter("select MaBan from BAN where TinhTrang !='O' and TenBan like N'" + comboBox1.Text+"'",connection);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);
                    adt.Dispose();
                    label9.Text = comboBox1.Text;
                    string maban = dt.Rows[0][0].ToString();
                    SqlCommand cmd = new SqlCommand("select * from HOADON ", connection);
                    SqlDataAdapter adt1 = new SqlDataAdapter(cmd);
                    adt1.Fill(ds, "CTHD");
                    string sql = "update HOADON" +
                                 " set MaBan='" + maban + "'" +
                                " where MaHD='" + MaHDChuaThanhToan + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    updatetinhtrangban();
                    listView2.Items.Clear();
                    listView2.Groups.Clear();
                    loadban();
                    loadHD();
                    comboBox1.Text = "";
                    listView4.Items.Clear();
                    loadHDChuaThanhToan();
                    addTable();
                }
            }
        }

        private void fSell_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectData.Disconnect();
        }
    }
}
