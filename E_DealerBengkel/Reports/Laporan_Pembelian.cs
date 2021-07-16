using System;
using E_DealerBengkel.Reports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Reports
{
    public partial class Laporan_Pembelian : Form
    {

        Timer timer = new Timer();

        public Laporan_Pembelian()
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

        private void Laporan_Pembelian_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
            this.reportViewer1.RefreshReport();
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Manager_Report manager = new Manager_Report();
            manager.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void dateAwal_ValueChanged(object sender, EventArgs e)
        {
            string awal = dateAwal.Text;
            string akhir = dateAkhir.Text;

            
            this.laporan_BeliTableAdapter.Fill(this.database_Laporan.Laporan_Beli, awal, akhir);
            this.reportViewer1.RefreshReport();
        }

        private void dateAkhir_ValueChanged(object sender, EventArgs e)
        {
            string awal = dateAwal.Text;
            string akhir = dateAkhir.Text;


            this.laporan_BeliTableAdapter.Fill(this.database_Laporan.Laporan_Beli, awal, akhir);
            this.reportViewer1.RefreshReport();
        }

        private void BtnKembali_Click_1(object sender, EventArgs e)
        {
            Manager_Report manager = new Manager_Report();
            manager.Show();
            this.Hide();
        }

        private void BtnLapPenjualan_Click(object sender, EventArgs e)
        {
            Laporan_Penjualan lap = new Laporan_Penjualan();
            lap.Show();
            this.Hide();
        }

        private void BtnLapPembelian_Click(object sender, EventArgs e)
        {
            Laporan_Pembelian lap = new Laporan_Pembelian();
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
            Konfirmasi_Retur lap = new Konfirmasi_Retur();
            lap.Show();
            this.Hide();
        }
    }
}
