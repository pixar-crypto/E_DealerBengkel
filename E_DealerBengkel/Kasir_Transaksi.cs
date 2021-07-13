using E_DealerBengkel.Transaksi.Pembelian;
using E_DealerBengkel.Transaksi.Penjualan;
using E_DealerBengkel.Transaksi.Services;
using E_DealerBengkel.Transaksi.Retur;
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

namespace E_DealerBengkel
{
    public partial class Kasir_Transaksi : Form
    {

        Timer timerJam = new Timer();
        Timer timerJual = new Timer();

        public Kasir_Transaksi()
        {
            InitializeComponent();

            timerJam.Tick += new EventHandler(timer_Tick);
            //1000 = 1 detik
            timerJam.Interval = (1000) * (1);
            timerJam.Enabled = true;
            timerJam.Start();

            timerJual.Tick += new EventHandler(timer_jual);
            timerJual.Interval = (1000) * (1);
            timerJual.Enabled = true;
            timerJual.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lbWaktu.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        void timer_jual(object sender, EventArgs e)
        {
            //fillGraph();
        }

        public void isiPendapatan()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT SUM(total_harga) AS PENDAPATAN FROM tPenjualan", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgPendapatan.DataSource = dt;
                dgPendapatan.Columns[0].DefaultCellStyle.Format = "Rp #,###";

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DgKaryawan()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tKaryawan", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvKaryawan.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DgMember()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tMember", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvMember.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void DgMobil()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tMobil", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvMobil.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DgMotor()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tMotor", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvMotor.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DgServis()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tService", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvServis.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DgSukuCadang()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tSukucadang", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvSukuCadang.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DgSup()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());

            try
            {
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT count(*) FROM tSupplier", connection);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvSup.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Graph Keseluruhan Penjualan
        private void fillGraph()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                DataSet ds = new DataSet();

                SqlCommand query = new SqlCommand("SELECT id_jenisBarang AS JenisBarang, Total FROM (SELECT TOP 3 COUNT(id_jenisBarang)" +
                    " AS Total, id_jenisBarang FROM tPenjualan GROUP BY id_jenisBarang ORDER BY Total DESC) AS A", connection);
                connection.Open();
                SqlDataReader rdr = query.ExecuteReader();
                while (rdr.Read())
                {
                    graphPenjualan.Series["Jual"].Points.AddXY(rdr.GetString(0), rdr.GetInt32(1));
                }
                rdr.Close();
                connection.Close();

            }
            catch (Exception ex)
            {

            }
        }

        //Graph Penjualan Mobil
        private void fillJualMobil()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                DataSet ds = new DataSet();

                SqlCommand query = new SqlCommand("SELECT TOP 3 COUNT(id_jenisBarang) AS Total, m.merek_mobil FROM tPenjualan P INNER JOIN tMobil M ON P.id_jenisBarang = M.id_mobil GROUP BY merek_mobil ORDER BY Total DESC", connection);
                connection.Open();
                SqlDataReader rdr = query.ExecuteReader();
                while (rdr.Read())
                {
                    graphMobil.Series["Mobil"].Points.AddXY(rdr.GetString(1), rdr.GetInt32(0));
                }
                rdr.Close();
                connection.Close();

            }
            catch (Exception ex)
            {

            }
        }

        //Graph Penjualan Motor
        private void fillJualMotor()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                DataSet ds = new DataSet();

                SqlCommand query = new SqlCommand("SELECT TOP 3 COUNT(id_jenisBarang) AS Total, m.merek_motor FROM tPenjualan P " +
                    "INNER JOIN tMotor M ON P.id_jenisBarang = M.id_motor GROUP BY merek_motor ORDER BY Total DESC", connection);
                connection.Open();
                SqlDataReader rdr = query.ExecuteReader();
                while (rdr.Read())
                {
                    graphMotor.Series["Motor"].Points.AddXY(rdr.GetString(1), rdr.GetInt32(0));
                }
                rdr.Close();
                connection.Close();
                
            }
            catch (Exception ex)
            {

            }
        }

        //Graph Penjualan SukuCadang
        private void fillJualSukuCadang()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                DataSet ds = new DataSet();

                SqlCommand query = new SqlCommand("SELECT TOP 3 COUNT(id_jenisBarang) AS Total, T.merek_sukucadang FROM tPenjualan P INNER JOIN tSukucadang T ON P.id_jenisBarang = T.id_sukucadang GROUP BY merek_sukucadang ORDER BY Total DESC", connection);
                connection.Open();
                SqlDataReader rdr = query.ExecuteReader();
                while (rdr.Read())
                {
                    graphSukuCadang.Series["SukuCadang"].Points.AddXY(rdr.GetString(1), rdr.GetInt32(0));
                }
                rdr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void LogoutIcon_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }


        private void BtnPembelian_Click(object sender, EventArgs e)
        {
            Pembelian beli = new Pembelian();
            beli.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login logn = new Login();
            logn.Show();
            this.Hide();
        }

        private void BtnPenjualan_Click(object sender, EventArgs e)
        {
            Penjualan penjualan = new Penjualan();
            penjualan.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login logn = new Login();
            logn.Show();
            this.Hide();
        }

        private void BtnRetur_Click(object sender, EventArgs e)
        {
            Retur retur = new Retur();
            retur.Show();
            this.Hide();
        }

        private void BtnServices_Click(object sender, EventArgs e)
        {
            Services services = new Services();
            services.Show();
            this.Hide();
        }

        private void Kasir_Transaksi_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
            isiPendapatan();

            fillGraph();
            fillJualMobil();
            fillJualMotor();
            fillJualSukuCadang();

            DgKaryawan();
            DgMember();
            DgMobil();
            DgMotor();
            DgServis();
            DgSukuCadang();
            DgSup();
        }
    }
}
