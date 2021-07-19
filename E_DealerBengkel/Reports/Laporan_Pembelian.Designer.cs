namespace E_DealerBengkel.Reports
{
    partial class Laporan_Pembelian
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Laporan_Pembelian));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.laporanBeliBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.database_Laporan = new E_DealerBengkel.Database_Laporan();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateAwal = new System.Windows.Forms.DateTimePicker();
            this.lbJudul = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dateAkhir = new System.Windows.Forms.DateTimePicker();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label10 = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.lbWaktu = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.laporan_BeliTableAdapter = new E_DealerBengkel.Database_LaporanTableAdapters.Laporan_BeliTableAdapter();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BtnKembali = new System.Windows.Forms.Button();
            this.BtnKonfirRetur = new System.Windows.Forms.Button();
            this.BtnLapServices = new System.Windows.Forms.Button();
            this.BtnLapRetur = new System.Windows.Forms.Button();
            this.BtnLapPenjualan = new System.Windows.Forms.Button();
            this.BtnLapPembelian = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.laporanBeliBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database_Laporan)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // laporanBeliBindingSource
            // 
            this.laporanBeliBindingSource.DataMember = "Laporan_Beli";
            this.laporanBeliBindingSource.DataSource = this.database_Laporan;
            // 
            // database_Laporan
            // 
            this.database_Laporan.DataSetName = "Database_Laporan";
            this.database_Laporan.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(-84, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1467, 38);
            this.panel1.TabIndex = 131;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Honeydew;
            this.label3.Location = new System.Drawing.Point(295, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(344, 31);
            this.label3.TabIndex = 130;
            this.label3.Text = "Halaman Manager - Laporan Pembelian";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1412, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 34);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(1378, 2);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 34);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label9.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(301, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(215, 25);
            this.label9.TabIndex = 135;
            this.label9.Text = "CAKUNG - JAKARTA TIMUR";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(301, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 35);
            this.label1.TabIndex = 134;
            this.label1.Text = "VROOM! D&&G";
            // 
            // dateAwal
            // 
            this.dateAwal.CustomFormat = "yyyy-MM-dd";
            this.dateAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateAwal.Location = new System.Drawing.Point(233, 39);
            this.dateAwal.Name = "dateAwal";
            this.dateAwal.Size = new System.Drawing.Size(200, 20);
            this.dateAwal.TabIndex = 150;
            this.dateAwal.ValueChanged += new System.EventHandler(this.dateAwal_ValueChanged);
            // 
            // lbJudul
            // 
            this.lbJudul.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJudul.ForeColor = System.Drawing.Color.Black;
            this.lbJudul.Location = new System.Drawing.Point(652, 169);
            this.lbJudul.Name = "lbJudul";
            this.lbJudul.Size = new System.Drawing.Size(324, 23);
            this.lbJudul.TabIndex = 151;
            this.lbJudul.Text = "LAPORAN PEMBELIAN";
            this.lbJudul.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.dateAkhir);
            this.panel3.Controls.Add(this.reportViewer1);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dateAwal);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(234, 215);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1122, 525);
            this.panel3.TabIndex = 152;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(646, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 154;
            this.label4.Text = "TANGGAL AWAL";
            // 
            // dateAkhir
            // 
            this.dateAkhir.CustomFormat = "yyyy-MM-dd";
            this.dateAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateAkhir.Location = new System.Drawing.Point(767, 39);
            this.dateAkhir.Name = "dateAkhir";
            this.dateAkhir.Size = new System.Drawing.Size(200, 20);
            this.dateAkhir.TabIndex = 153;
            this.dateAkhir.ValueChanged += new System.EventHandler(this.dateAkhir_ValueChanged);
            // 
            // reportViewer1
            // 
            reportDataSource4.Name = "dsPembelian";
            reportDataSource4.Value = this.laporanBeliBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "E_DealerBengkel.Reports.Rdlc.Laporan_Pembelian.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(47, 85);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1024, 409);
            this.reportViewer1.TabIndex = 152;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(112, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 16);
            this.label10.TabIndex = 151;
            this.label10.Text = "TANGGAL AWAL";
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUser.Location = new System.Drawing.Point(85, 14);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(135, 19);
            this.lbUser.TabIndex = 18;
            this.lbUser.Text = "Hallo, manager ";
            // 
            // lbWaktu
            // 
            this.lbWaktu.AutoSize = true;
            this.lbWaktu.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaktu.Location = new System.Drawing.Point(80, 59);
            this.lbWaktu.Name = "lbWaktu";
            this.lbWaktu.Size = new System.Drawing.Size(0, 16);
            this.lbWaktu.TabIndex = 115;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.lbWaktu);
            this.panel4.Controls.Add(this.lbUser);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Location = new System.Drawing.Point(1072, 45);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(281, 93);
            this.panel4.TabIndex = 136;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(14, 14);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(60, 58);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 104;
            this.pictureBox3.TabStop = false;
            // 
            // laporan_BeliTableAdapter
            // 
            this.laporan_BeliTableAdapter.ClearBeforeFill = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(215, 55);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(80, 80);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 133;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pictureBox12.Location = new System.Drawing.Point(197, 34);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(1183, 117);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox12.TabIndex = 132;
            this.pictureBox12.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.BtnKembali);
            this.panel2.Controls.Add(this.BtnKonfirRetur);
            this.panel2.Controls.Add(this.BtnLapServices);
            this.panel2.Controls.Add(this.BtnLapRetur);
            this.panel2.Controls.Add(this.BtnLapPenjualan);
            this.panel2.Controls.Add(this.BtnLapPembelian);
            this.panel2.Controls.Add(this.panel12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(211, 788);
            this.panel2.TabIndex = 153;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel5.Location = new System.Drawing.Point(17, 202);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(176, 3);
            this.panel5.TabIndex = 154;
            // 
            // BtnKembali
            // 
            this.BtnKembali.FlatAppearance.BorderSize = 0;
            this.BtnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKembali.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKembali.ForeColor = System.Drawing.Color.Honeydew;
            this.BtnKembali.Image = ((System.Drawing.Image)(resources.GetObject("BtnKembali.Image")));
            this.BtnKembali.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnKembali.Location = new System.Drawing.Point(8, 695);
            this.BtnKembali.Name = "BtnKembali";
            this.BtnKembali.Size = new System.Drawing.Size(200, 42);
            this.BtnKembali.TabIndex = 131;
            this.BtnKembali.Text = "   KEMBALI";
            this.BtnKembali.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnKembali.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnKembali.UseVisualStyleBackColor = true;
            this.BtnKembali.Click += new System.EventHandler(this.BtnKembali_Click_1);
            // 
            // BtnKonfirRetur
            // 
            this.BtnKonfirRetur.FlatAppearance.BorderSize = 0;
            this.BtnKonfirRetur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKonfirRetur.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKonfirRetur.ForeColor = System.Drawing.Color.Honeydew;
            this.BtnKonfirRetur.Image = ((System.Drawing.Image)(resources.GetObject("BtnKonfirRetur.Image")));
            this.BtnKonfirRetur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnKonfirRetur.Location = new System.Drawing.Point(0, 319);
            this.BtnKonfirRetur.Name = "BtnKonfirRetur";
            this.BtnKonfirRetur.Size = new System.Drawing.Size(200, 37);
            this.BtnKonfirRetur.TabIndex = 130;
            this.BtnKonfirRetur.Text = "KONFIRMASI RETUR";
            this.BtnKonfirRetur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnKonfirRetur.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnKonfirRetur.UseVisualStyleBackColor = true;
            this.BtnKonfirRetur.Click += new System.EventHandler(this.BtnKonfirRetur_Click);
            // 
            // BtnLapServices
            // 
            this.BtnLapServices.FlatAppearance.BorderSize = 0;
            this.BtnLapServices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLapServices.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLapServices.ForeColor = System.Drawing.Color.Honeydew;
            this.BtnLapServices.Image = global::E_DealerBengkel.Properties.Resources.documents_30px;
            this.BtnLapServices.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapServices.Location = new System.Drawing.Point(0, 281);
            this.BtnLapServices.Name = "BtnLapServices";
            this.BtnLapServices.Size = new System.Drawing.Size(200, 32);
            this.BtnLapServices.TabIndex = 61;
            this.BtnLapServices.Text = "LAPORAN SERVICES";
            this.BtnLapServices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapServices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLapServices.UseVisualStyleBackColor = true;
            this.BtnLapServices.Click += new System.EventHandler(this.BtnLapServices_Click);
            // 
            // BtnLapRetur
            // 
            this.BtnLapRetur.FlatAppearance.BorderSize = 0;
            this.BtnLapRetur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLapRetur.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLapRetur.ForeColor = System.Drawing.Color.Honeydew;
            this.BtnLapRetur.Image = global::E_DealerBengkel.Properties.Resources.documents_30px;
            this.BtnLapRetur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapRetur.Location = new System.Drawing.Point(0, 243);
            this.BtnLapRetur.Name = "BtnLapRetur";
            this.BtnLapRetur.Size = new System.Drawing.Size(200, 32);
            this.BtnLapRetur.TabIndex = 58;
            this.BtnLapRetur.Text = "LAPORAN RETUR";
            this.BtnLapRetur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapRetur.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLapRetur.UseVisualStyleBackColor = true;
            this.BtnLapRetur.Click += new System.EventHandler(this.BtnLapRetur_Click);
            // 
            // BtnLapPenjualan
            // 
            this.BtnLapPenjualan.FlatAppearance.BorderSize = 0;
            this.BtnLapPenjualan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLapPenjualan.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLapPenjualan.ForeColor = System.Drawing.Color.Honeydew;
            this.BtnLapPenjualan.Image = global::E_DealerBengkel.Properties.Resources.documents_30px;
            this.BtnLapPenjualan.Location = new System.Drawing.Point(0, 205);
            this.BtnLapPenjualan.Name = "BtnLapPenjualan";
            this.BtnLapPenjualan.Size = new System.Drawing.Size(217, 36);
            this.BtnLapPenjualan.TabIndex = 57;
            this.BtnLapPenjualan.Text = "LAPORAN PENJUALAN";
            this.BtnLapPenjualan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapPenjualan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLapPenjualan.UseVisualStyleBackColor = true;
            this.BtnLapPenjualan.Click += new System.EventHandler(this.BtnLapPenjualan_Click);
            // 
            // BtnLapPembelian
            // 
            this.BtnLapPembelian.FlatAppearance.BorderSize = 0;
            this.BtnLapPembelian.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLapPembelian.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLapPembelian.ForeColor = System.Drawing.Color.Honeydew;
            this.BtnLapPembelian.Image = ((System.Drawing.Image)(resources.GetObject("BtnLapPembelian.Image")));
            this.BtnLapPembelian.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapPembelian.Location = new System.Drawing.Point(0, 164);
            this.BtnLapPembelian.Name = "BtnLapPembelian";
            this.BtnLapPembelian.Size = new System.Drawing.Size(210, 36);
            this.BtnLapPembelian.TabIndex = 56;
            this.BtnLapPembelian.Text = "LAPORAN PEMBELIAN";
            this.BtnLapPembelian.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLapPembelian.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLapPembelian.UseVisualStyleBackColor = true;
            this.BtnLapPembelian.Click += new System.EventHandler(this.BtnLapPembelian_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.SlateGray;
            this.panel12.Controls.Add(this.pictureBox4);
            this.panel12.Controls.Add(this.pictureBox1);
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(211, 151);
            this.panel12.TabIndex = 128;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.SlateGray;
            this.pictureBox1.Image = global::E_DealerBengkel.Properties.Resources.icons8_document_90px;
            this.pictureBox1.Location = new System.Drawing.Point(39, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.SlateGray;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(87, 116);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(36, 32);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 58;
            this.pictureBox4.TabStop = false;
            // 
            // Laporan_Pembelian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1379, 788);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lbJudul);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Laporan_Pembelian";
            this.Text = "Laporan_Pembelian";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Laporan_Pembelian_Load);
            ((System.ComponentModel.ISupportInitialize)(this.laporanBeliBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database_Laporan)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DateTimePicker dateAwal;
        private System.Windows.Forms.Label lbJudul;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label10;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label lbWaktu;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateAkhir;
        private System.Windows.Forms.BindingSource laporanBeliBindingSource;
        private Database_Laporan database_Laporan;
        private Database_LaporanTableAdapters.Laporan_BeliTableAdapter laporan_BeliTableAdapter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnKembali;
        private System.Windows.Forms.Button BtnKonfirRetur;
        private System.Windows.Forms.Button BtnLapServices;
        private System.Windows.Forms.Button BtnLapRetur;
        private System.Windows.Forms.Button BtnLapPenjualan;
        private System.Windows.Forms.Button BtnLapPembelian;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}