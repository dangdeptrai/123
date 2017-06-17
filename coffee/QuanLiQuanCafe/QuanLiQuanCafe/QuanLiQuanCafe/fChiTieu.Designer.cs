namespace QuanLiQuanCafe
{
    partial class fChiTieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.grbPhiêuChi = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbSo_tien_chi = new System.Windows.Forms.Label();
            this.dtpNgay_chi_tieu = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTong_tien_chi = new System.Windows.Forms.TextBox();
            this.tbMa_chi_tieu = new System.Windows.Forms.TextBox();
            this.tbLi_do_chi = new System.Windows.Forms.TextBox();
            this.btNhap_phieu_chi = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NgayChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMucChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grbPhiêuChi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã Chi Tiêu";
            // 
            // grbPhiêuChi
            // 
            this.grbPhiêuChi.Controls.Add(this.label5);
            this.grbPhiêuChi.Controls.Add(this.button1);
            this.grbPhiêuChi.Controls.Add(this.tbSo_tien_chi);
            this.grbPhiêuChi.Controls.Add(this.dtpNgay_chi_tieu);
            this.grbPhiêuChi.Controls.Add(this.label3);
            this.grbPhiêuChi.Controls.Add(this.label2);
            this.grbPhiêuChi.Controls.Add(this.tbTong_tien_chi);
            this.grbPhiêuChi.Controls.Add(this.label1);
            this.grbPhiêuChi.Controls.Add(this.tbMa_chi_tieu);
            this.grbPhiêuChi.Controls.Add(this.tbLi_do_chi);
            this.grbPhiêuChi.Controls.Add(this.btNhap_phieu_chi);
            this.grbPhiêuChi.Location = new System.Drawing.Point(12, 12);
            this.grbPhiêuChi.Name = "grbPhiêuChi";
            this.grbPhiêuChi.Size = new System.Drawing.Size(375, 420);
            this.grbPhiêuChi.TabIndex = 3;
            this.grbPhiêuChi.TabStop = false;
            this.grbPhiêuChi.Text = "Nhập Thông Tin Phiếu Chi";
            this.grbPhiêuChi.Enter += new System.EventHandler(this.grbPhiêuChi_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(103, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "CHI TIẾT PHIẾU CHI";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 57);
            this.button1.TabIndex = 15;
            this.button1.Text = "Lưu phiếu chi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbSo_tien_chi
            // 
            this.tbSo_tien_chi.AutoSize = true;
            this.tbSo_tien_chi.Location = new System.Drawing.Point(38, 261);
            this.tbSo_tien_chi.Name = "tbSo_tien_chi";
            this.tbSo_tien_chi.Size = new System.Drawing.Size(62, 13);
            this.tbSo_tien_chi.TabIndex = 11;
            this.tbSo_tien_chi.Text = "Số Tiền Chi";
            // 
            // dtpNgay_chi_tieu
            // 
            this.dtpNgay_chi_tieu.Location = new System.Drawing.Point(169, 129);
            this.dtpNgay_chi_tieu.Name = "dtpNgay_chi_tieu";
            this.dtpNgay_chi_tieu.Size = new System.Drawing.Size(165, 20);
            this.dtpNgay_chi_tieu.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Lí Do Chi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ngày Chi Tiêu";
            // 
            // tbTong_tien_chi
            // 
            this.tbTong_tien_chi.Location = new System.Drawing.Point(169, 261);
            this.tbTong_tien_chi.Name = "tbTong_tien_chi";
            this.tbTong_tien_chi.Size = new System.Drawing.Size(165, 20);
            this.tbTong_tien_chi.TabIndex = 7;
            // 
            // tbMa_chi_tieu
            // 
            this.tbMa_chi_tieu.Enabled = false;
            this.tbMa_chi_tieu.Location = new System.Drawing.Point(169, 69);
            this.tbMa_chi_tieu.Name = "tbMa_chi_tieu";
            this.tbMa_chi_tieu.Size = new System.Drawing.Size(165, 20);
            this.tbMa_chi_tieu.TabIndex = 6;
            // 
            // tbLi_do_chi
            // 
            this.tbLi_do_chi.Location = new System.Drawing.Point(169, 197);
            this.tbLi_do_chi.Name = "tbLi_do_chi";
            this.tbLi_do_chi.Size = new System.Drawing.Size(165, 20);
            this.tbLi_do_chi.TabIndex = 5;
            // 
            // btNhap_phieu_chi
            // 
            this.btNhap_phieu_chi.Location = new System.Drawing.Point(71, 328);
            this.btNhap_phieu_chi.Name = "btNhap_phieu_chi";
            this.btNhap_phieu_chi.Size = new System.Drawing.Size(107, 57);
            this.btNhap_phieu_chi.TabIndex = 3;
            this.btNhap_phieu_chi.Text = "Tạo phiếu chi";
            this.btNhap_phieu_chi.UseVisualStyleBackColor = true;
            this.btNhap_phieu_chi.Click += new System.EventHandler(this.btNhap_phieu_chi_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(155, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "PHIẾU CHI";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NgayChi,
            this.TenMucChi,
            this.SoTien});
            this.dataGridView1.Location = new System.Drawing.Point(0, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(424, 357);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // NgayChi
            // 
            this.NgayChi.DataPropertyName = "NgayChi";
            this.NgayChi.FillWeight = 150F;
            this.NgayChi.HeaderText = "Ngày Chi Tiêu";
            this.NgayChi.MinimumWidth = 10;
            this.NgayChi.Name = "NgayChi";
            this.NgayChi.Width = 150;
            // 
            // TenMucChi
            // 
            this.TenMucChi.DataPropertyName = "TenMucChi";
            this.TenMucChi.HeaderText = "Lí Do Chi";
            this.TenMucChi.Name = "TenMucChi";
            this.TenMucChi.Width = 120;
            // 
            // SoTien
            // 
            this.SoTien.DataPropertyName = "SoTien";
            this.SoTien.HeaderText = "Số Tiền Chi";
            this.SoTien.Name = "SoTien";
            this.SoTien.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(393, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 420);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu chi";
            // 
            // fChiTieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 448);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbPhiêuChi);
            this.Name = "fChiTieu";
            this.Text = "fChiTieu";
            this.Load += new System.EventHandler(this.fChiTieu_Load);
            this.grbPhiêuChi.ResumeLayout(false);
            this.grbPhiêuChi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbPhiêuChi;
        private System.Windows.Forms.TextBox tbTong_tien_chi;
        private System.Windows.Forms.TextBox tbMa_chi_tieu;
        private System.Windows.Forms.TextBox tbLi_do_chi;
        private System.Windows.Forms.Button btNhap_phieu_chi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpNgay_chi_tieu;
        private System.Windows.Forms.Label tbSo_tien_chi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMucChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTien;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}