using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Reports
{
    public partial class Konfirmasi_Retur : Form
    {

        Timer timer = new Timer();

        public Konfirmasi_Retur()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(timer_Tick);
            //1000 = 1 detik
            timer.Interval = (1000) * (1);
            timer.Enabled = true;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lbWaktu.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void Konfirmasi_Retur_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;

            RefreshDg();
        }

        public void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tReturPembelian where status='Menunggu'", connection);
            DataTable dt = new DataTable();

            connection.Open();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvDataRetur.DataSource = dt;
            dgvDataRetur.Columns[1].HeaderText = "ID Retur";
            dgvDataRetur.Columns[2].HeaderText = "Tanggal Retur";
            dgvDataRetur.Columns[3].HeaderText = "Keterangan";
            dgvDataRetur.Columns[4].HeaderText = "ID Karyawan";
            dgvDataRetur.Columns[5].HeaderText = "ID Member";
            dgvDataRetur.Columns[6].HeaderText = "Status";
            dgvDataRetur.Columns[7].HeaderText = "No Penjualan";
            dgvDataRetur.Columns[8].HeaderText = "Jenis Barang";

            foreach (DataGridViewColumn colm in dgvDataRetur.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvDataRetur.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            connection.Close();

            dgvDataRetur.BorderStyle = BorderStyle.None;
            dgvDataRetur.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvDataRetur.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDataRetur.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvDataRetur.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDataRetur.BackgroundColor = Color.White;

            dgvDataRetur.EnableHeadersVisualStyles = false;
            dgvDataRetur.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDataRetur.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvDataRetur.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void dgvDataRetur_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idR, tanggalR, ketR, idKar, idMbr, status;
            int n = dgvDataRetur.CurrentCell.RowIndex;
            if (dgvDataRetur.Rows[n].Cells[5].Value.ToString() == "Disetujui" || dgvDataRetur.Rows[n].Cells[5].Value.ToString() == "Tidak Disetujui")
            {
                MessageBox.Show("Data telah dikonfirmasi", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dgvDataRetur.Rows[n].Cells[5].Value.ToString() == "")
            {
                MessageBox.Show("Data kosong!", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                idR = dgvDataRetur.Rows[n].Cells[1].Value.ToString();
                tanggalR = dgvDataRetur.Rows[n].Cells[2].Value.ToString();
                ketR = dgvDataRetur.Rows[n].Cells[3].Value.ToString();
                idKar = dgvDataRetur.Rows[n].Cells[4].Value.ToString();
                idMbr = dgvDataRetur.Rows[n].Cells[5].Value.ToString();
                status = dgvDataRetur.Rows[n].Cells[6].Value.ToString();

                Konfirmasi_Retur2 retur2 = new Konfirmasi_Retur2(idR, tanggalR, ketR, idKar, idMbr, status);
                retur2.Show();
                //this.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tReturPembelian where status='Menunggu'", connection);
            DataTable dt = new DataTable();

            connection.Open();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvDataRetur.DataSource = dt;
            dgvDataRetur.Columns[1].HeaderText = "ID Retur";
            dgvDataRetur.Columns[2].HeaderText = "Tanggal Retur";
            dgvDataRetur.Columns[3].HeaderText = "Keterangan";
            dgvDataRetur.Columns[4].HeaderText = "ID Karyawan";
            dgvDataRetur.Columns[5].HeaderText = "ID Member";
            dgvDataRetur.Columns[6].HeaderText = "Status";
            dgvDataRetur.Columns[7].HeaderText = "No Penjualan";
            dgvDataRetur.Columns[8].HeaderText = "Jenis Barang";

            foreach (DataGridViewColumn colm in dgvDataRetur.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //this.dgvDataRetur.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            connection.Close();

            dgvDataRetur.BorderStyle = BorderStyle.None;
            dgvDataRetur.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvDataRetur.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDataRetur.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvDataRetur.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDataRetur.BackgroundColor = Color.White;

            dgvDataRetur.EnableHeadersVisualStyles = false;
            dgvDataRetur.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDataRetur.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvDataRetur.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tReturPembelian where status='Ditolak'", connection);
            DataTable dt = new DataTable();

            connection.Open();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvDataRetur.DataSource = dt;
            dgvDataRetur.Columns[1].HeaderText = "ID Retur";
            dgvDataRetur.Columns[2].HeaderText = "Tanggal Retur";
            dgvDataRetur.Columns[3].HeaderText = "Keterangan";
            dgvDataRetur.Columns[4].HeaderText = "ID Karyawan";
            dgvDataRetur.Columns[5].HeaderText = "ID Member";
            dgvDataRetur.Columns[6].HeaderText = "Status";
            dgvDataRetur.Columns[7].HeaderText = "No Penjualan";
            dgvDataRetur.Columns[8].HeaderText = "Jenis Barang";

            foreach (DataGridViewColumn colm in dgvDataRetur.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //this.dgvDataRetur.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            connection.Close();

            dgvDataRetur.BorderStyle = BorderStyle.None;
            dgvDataRetur.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvDataRetur.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDataRetur.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvDataRetur.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDataRetur.BackgroundColor = Color.White;

            dgvDataRetur.EnableHeadersVisualStyles = false;
            dgvDataRetur.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDataRetur.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvDataRetur.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbSetuju_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tReturPembelian where status='Disetujui'", connection);
            DataTable dt = new DataTable();

            connection.Open();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvDataRetur.DataSource = dt;
            dgvDataRetur.Columns[1].HeaderText = "ID Retur";
            dgvDataRetur.Columns[2].HeaderText = "Tanggal Retur";
            dgvDataRetur.Columns[3].HeaderText = "Keterangan";
            dgvDataRetur.Columns[4].HeaderText = "ID Karyawan";
            dgvDataRetur.Columns[5].HeaderText = "ID Member";
            dgvDataRetur.Columns[6].HeaderText = "Status";
            dgvDataRetur.Columns[7].HeaderText = "No Penjualan";
            dgvDataRetur.Columns[8].HeaderText = "Jenis Barang";

            foreach (DataGridViewColumn colm in dgvDataRetur.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //this.dgvDataRetur.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            connection.Close();

            dgvDataRetur.BorderStyle = BorderStyle.None;
            dgvDataRetur.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvDataRetur.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDataRetur.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvDataRetur.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDataRetur.BackgroundColor = Color.White;

            dgvDataRetur.EnableHeadersVisualStyles = false;
            dgvDataRetur.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDataRetur.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvDataRetur.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Manager_Report manager = new Manager_Report();
            manager.Show();
            this.Hide();
        }

        private void BtnLapPembelian_Click(object sender, EventArgs e)
        {
            Laporan_Pembelian lap = new Laporan_Pembelian();
            lap.Show();
            this.Hide();
        }

        private void BtnLapPenjualan_Click(object sender, EventArgs e)
        {
            Laporan_Penjualan lap = new Laporan_Penjualan();
            lap.Show();
            this.Hide();
        }

        private void BtnLapRetur_Click(object sender, EventArgs e)
        {
            Laporan_Retur lap = new Laporan_Retur();
            lap.Show();
            this.Hide();
        }

        private void BtnLapServices_Click(object sender, EventArgs e)
        {
            Laporan_Service lap = new Laporan_Service();
            lap.Show();
            this.Hide();
        }

        private void BtnKonfirRetur_Click(object sender, EventArgs e)
        {
            Konfirmasi_Retur konf = new Konfirmasi_Retur();
            konf.Show();
            this.Hide();
        }
    }
}
