using E_DealerBengkel.Master.Employee;
using E_DealerBengkel.Master.Member;
using E_DealerBengkel.Master.Posisi;
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
            tampilMember();

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

        private void BtnKaryawan_Click(object sender, EventArgs e)
        {
            CRUD_Employee M_Emp = new CRUD_Employee();
            M_Emp.Show();
            this.Hide();
        }

        private void BtnPosisi_Click(object sender, EventArgs e)
        {
            CRUD_Posisi M_Posisi = new CRUD_Posisi();
            M_Posisi.Show();
            this.Hide();
        }

        private void BtnMember_Click(object sender, EventArgs e)
        {
            CRUD_Member M_Member = new CRUD_Member();
            M_Member.Show();
            this.Hide();
        }

        private void BtnSupplier_Click(object sender, EventArgs e)
        {
            CRUD_Supplier M_Supplier = new CRUD_Supplier();
            M_Supplier.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login logn = new Login();
            logn.Show();
            this.Hide();
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
                txtMember.Text = jml.ToString();

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
                txtKaryawan.Text = jml.ToString();

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
    }
}
