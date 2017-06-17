using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class fTableManager : Form
    {
        string MaNV,tk;
        public fTableManager(string TK,string manv)
        {
            
            InitializeComponent();
            if (TK != "admin")
            {
                btChiTieu.Visible = false;
                btnReport.Visible = false;
            }
            tk = TK;
            MaNV = manv;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            fSell fsell = new fSell(MaNV);
            fsell.Show();
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            fMon fmon = new fMon(tk);
            fmon.ShowDialog();
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            fTable ftable = new fTable(tk);
            ftable.ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            fStaff fstaff = new fStaff(tk);
            fstaff.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            fReport freport = new fReport();
            freport.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btChiTieu_Click(object sender, EventArgs e)
        {
            fChiTieu fchitieu = new fChiTieu();
            fchitieu.ShowDialog();
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button1, new Point(0, button1.Height));
        }

        private void mậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fChangePW f = new fChangePW(tk);
            f.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }





    }
}
