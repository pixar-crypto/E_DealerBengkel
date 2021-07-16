using E_DealerBengkel.Reports;
using System;
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

namespace E_DealerBengkel
{
    public partial class Manager_Report : Form
    {

        //---SERVER UMUM---
        string connectionString =
          "integrated security=true; data source=localhost;initial catalog=VroomDG";

        Timer timer = new Timer();

        public Manager_Report()
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

        private void LogoutIcon_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
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

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void Manager_Report_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
        }

        private void BtnLapPembelian_Click(object sender, EventArgs e)
        {
            Laporan_Pembelian lap = new Laporan_Pembelian();
            lap.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login logn = new Login();
            logn.Show();
            this.Hide();
        }

        private void BtnLapPenjualan_Click(object sender, EventArgs e)
        {
            Laporan_Penjualan lap = new Laporan_Penjualan();
            lap.Show();
            this.Hide();
        }

        private void BtnLapServices_Click(object sender, EventArgs e)
        {
            Laporan_Service lap = new Laporan_Service();
            lap.Show();
            this.Hide();
        }

        private void BtnLapRetur_Click(object sender, EventArgs e)
        {
            Laporan_Retur lap = new Laporan_Retur();
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
