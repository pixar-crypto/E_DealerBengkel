using E_DealerBengkel.Master.Employee;
using E_DealerBengkel.Master.Member;
using E_DealerBengkel.Master.Mobil;
using E_DealerBengkel.Master.Motor;
using E_DealerBengkel.Master.Posisi;
using E_DealerBengkel.Master.Services;
using E_DealerBengkel.Master.SukuCadang;
using E_DealerBengkel.Master.Supplier;
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
    public partial class Admin_Master : Form
    {
        Timer timer = new Timer();

        public Admin_Master()
        {
            InitializeComponent();
            tampilKaryawan();
            tampilPosisi();
            tampilMember();
            tampilMobil();
            tampilMotor();
            tampilSukuCadang();
            tampilSupplier();
            tampilService();

            timer.Tick += new EventHandler(timer_Tick);
            //1000 = 1 detik
            timer.Interval = (1000) * (1);
            timer.Enabled = true;
            timer.Start();
        }

        private void LogoutIcon_Click(object sender, EventArgs e)
        {
            Login logn = new Login();
            logn.Show();
            this.Hide();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lbWaktu.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        public void tampilMember()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tMember", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblMember.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilPosisi()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tPosisi", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblPosisi.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilKaryawan()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tKaryawan", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblKaryawan.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilMobil()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tMobil", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblMobil.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilMotor()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tMotor", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblMotor.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilSukuCadang()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tSukucadang", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblSukuCadang.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilSupplier()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tSupplier", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblSupplier.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        public void tampilService()
        {
            int jml = 0;
            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();

                SqlCommand command = new SqlCommand
                    ("SELECT * FROM tService", connection);

                // eksekuai command
                SqlDataReader dr;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    jml++;
                }
                lblServices.Text = jml.ToString();

                connection.Close();
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
        }

        private void Admin_Master_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
        }

        private void BtnKaryawan_Click_1(object sender, EventArgs e)
        {
            CRUD_Employee M_Emp = new CRUD_Employee();
            M_Emp.Show();
            this.Hide();
        }

        private void BtnPosisi_Click_1(object sender, EventArgs e)
        {
            CRUD_Posisi M_Posisi = new CRUD_Posisi();
            M_Posisi.Show();
            this.Hide();
        }

        private void BtnMember_Click_1(object sender, EventArgs e)
        {
            CRUD_Member M_Member = new CRUD_Member();
            M_Member.Show();
            this.Hide();
        }

        private void BtnMobil_Click_1(object sender, EventArgs e)
        {
            CRUD_Mobil M_Mobil = new CRUD_Mobil();
            M_Mobil.Show();
            this.Hide();
        }

        private void BtnMotor_Click_1(object sender, EventArgs e)
        {
            CRUD_Motor M_Motor = new CRUD_Motor();
            M_Motor.Show();
            this.Hide();
        }

        private void BtnServices_Click_1(object sender, EventArgs e)
        {
            CRUD_Service M_Services = new CRUD_Service();
            M_Services.Show();
            this.Hide();
        }

        private void BtnSukuCadang_Click_1(object sender, EventArgs e)
        {
            CRUD_SukuCadang M_SukuCadang = new CRUD_SukuCadang();
            M_SukuCadang.Show();
            this.Hide();
        }

        private void BtnSupplier_Click_1(object sender, EventArgs e)
        {
            CRUD_Supplier M_Supplier = new CRUD_Supplier();
            M_Supplier.Show();
            this.Hide();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            TentangAplikasi aplikasi = new TentangAplikasi();
            aplikasi.Show();
            this.Hide();
        }

        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            Login logn = new Login();
            logn.Show();
            this.Hide();
        }
    }
}
