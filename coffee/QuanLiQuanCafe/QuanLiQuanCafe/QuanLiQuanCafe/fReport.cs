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
    public partial class fReport : Form
    {
        public fReport()
        {
            InitializeComponent();
        }

        private void fReport_Load(object sender, EventArgs e)
        {
            comboBox1.Text = System.DateTime.Today.Month.ToString();
            comboBox2.Text = System.DateTime.Today.Year.ToString();
            this.reportViewer2.Clear();
            this.reportViewer3.Clear();
            
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            this.mONTableAdapter.Fill(this.bCDOANHTHU.MON, tu_ngay.Value.Date, den_ngay.Value.Date);
            this.cHITIEUTableAdapter.Fill(this.bCDOANHTHU.CHITIEU, tu_ngay.Value.Date, den_ngay.Value.Date);
            this.reportViewer2.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DataTable1TableAdapter.Fill(this.BCLUONG.DataTable1, Int32.Parse(comboBox1.Text), Int32.Parse(comboBox2.Text));
            this.reportViewer3.RefreshReport();
        }
    }
}
