using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_DealerBengkel
{
    public partial class Login : Form
    {


        //---SERVER UMUM---

        string connectionstring =
                "integrated security=true;data source=localhost;initial catalog=VroomDG";

        public Login()
        {
            InitializeComponent();
        }


        private DataSet GetRoles(string username)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());

                SqlDataAdapter adapter = new SqlDataAdapter
                    ("SELECT id_posisi FROM tKaryawan WHERE username = '" + username + "'",
                    connection);
                adapter.Fill(ds);
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
            return ds;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void lbMataBuka_Click_1(object sender, EventArgs e)
        {
            lbMataBuka.Visible = false;
            TxtPassword.UseSystemPasswordChar = true;
        }

        private void lbMataTutup_Click(object sender, EventArgs e)
        {
            lbMataBuka.Visible = true;
            TxtPassword.UseSystemPasswordChar = false;
        }

        private void BtnLupaPassword_Click(object sender, EventArgs e)
        {
            LupaPassword lupapass = new LupaPassword();
            lupapass.Show();
            this.Hide();
        }

        private void Clear()
        {
            TxtUsername.Text = "";
            TxtPassword.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = true;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            String role = "";

            if (TxtUsername.Text == "" || TxtPassword.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong!", "Pemberitahuan!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (TxtUsername.Text == "admin" && TxtPassword.Text == "admin")
            {
                Admin_Master adm = new Admin_Master();
                adm.Show();
                this.Hide();
            }
            else
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                connection.Open();
                DataSet ds = new DataSet();
                string username = TxtUsername.Text.Trim();
                string password = TxtPassword.Text.Trim();

                SqlDataAdapter adapter = new SqlDataAdapter("select * from tKaryawan where username = '" + username + "' and password = '" + password + "'", connection);
                adapter.Fill(ds);
                int hitung = ds.Tables[0].Rows.Count;
                if (hitung == 0)
                {
                    MessageBox.Show("Username/password salah!", "Error!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //---- AMBIL ROLE ----
                    connection.Close();
                    SqlCommand query = new SqlCommand("SELECT id_posisi FROM tKaryawan WHERE username='" + TxtUsername.Text + "'", connection);
                    connection.Open();

                    SqlDataReader rdr = query.ExecuteReader();
                    rdr.Read();
                    role = rdr.GetString(0);
                    //-----------------------

                    //---- AMBIL USERNAME ----
                    GenericIdentity myIdentity = new GenericIdentity(TxtUsername.Text);
                    ds = GetRoles(TxtUsername.Text);
                    string[] myRole = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        myRole[i] = ds.Tables[0].Rows[i][0].ToString();
                    }
                    GenericPrincipal myPrincipal = new GenericPrincipal(myIdentity, myRole);
                    Thread.CurrentPrincipal = myPrincipal;
                    //-----------------------

                    if (role == "ROLE-03")
                    {
                        MessageBox.Show("Login berhasil!", "Pemberitahuan!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Admin_Master adm = new Admin_Master();
                        adm.Show();
                        this.Hide();
                    }
                    else if (role == "ROLE-02")
                    {
                        MessageBox.Show("Login berhasil!", "Pemberitahuan!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Kasir_Transaksi kt = new Kasir_Transaksi();
                        kt.Show();
                        this.Hide();
                    }
                    else if (role == "ROLE-01")
                    {
                        MessageBox.Show("Login berhasil!", "Pemberitahuan!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Manager_Report ml = new Manager_Report();
                        ml.Show();
                        this.Hide();
                    }
                }
                connection.Close();
            }
            Clear();
        }

        private void TxtUsername_Click(object sender, EventArgs e)
        {
            TxtUsername.Clear();
        }

        private void TxtPassword_Click(object sender, EventArgs e)
        {
            TxtPassword.Clear();
        }
    }
}
