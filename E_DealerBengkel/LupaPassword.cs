using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace E_DealerBengkel
{
    public partial class LupaPassword : Form
    {

        //---SERVER UMUM---

        string connectionstring =
                "integrated security=true;data source=localhost;initial catalog=VroomDG";
        String pass;

        public LupaPassword()
        {
            InitializeComponent();
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (TxtUsername.Text == "" || TxtNoTelp.Text == "")
            {
                MessageBox.Show("Data ada yang kosong!", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                SqlCommand search = new SqlCommand("[sp_CariPassword]", connection);

                if (!string.IsNullOrEmpty(TxtUsername.Text.Trim()) && !string.IsNullOrEmpty(TxtNoTelp.Text.Trim()))
                {
                    connection.Open();
                    search.CommandType = System.Data.CommandType.StoredProcedure;
                    search.Parameters.AddWithValue("@username", TxtUsername.Text.Trim());
                    search.Parameters.AddWithValue("@no_telp", TxtNoTelp.Text.Trim());
                    SqlDataReader reader = search.ExecuteReader();
                    if (reader.HasRows)
                    {
                        lbPilihan.Visible = true;
                        cbPilihan.Visible = true;
                        TxtUsername.Enabled = false;
                        TxtNoTelp.Enabled = false;

                        reader.Read();
                        TxtPass1.Text = Convert.ToString(reader["password"]);
                        pass = Convert.ToString(reader["password"]);
                        MessageBox.Show("Data ketemu", "Pemberitahuan",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void cbPilihan_TextChanged(object sender, EventArgs e)
        {
            if (cbPilihan.Text == "Lihat Password")
            {
                
                lbPass1.Visible = true;
                TxtPass1.Visible = true;
                lbPass2.Visible = false;
                TxtPass2.Visible = false;
                btnSimpan.Visible = false;
                lbMataTutup.Visible = true;
                lbMataBuka.Visible = true;
                TxtPass1.UseSystemPasswordChar = true;

                TxtPass1.Enabled = false;
                TxtPass2.Text = pass;
            }
            else
            {
                
                lbPass1.Visible = true;
                TxtPass1.Visible = true;
                lbPass2.Visible = true;
                TxtPass2.Visible = true;
                btnSimpan.Visible = true;
                lbMataTutup.Visible = true;

                TxtPass1.Enabled = true;
                TxtPass1.Text = "";
            }
        }

       

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (TxtPass1.Text == "" || TxtPass2.Text == "")
            {
                MessageBox.Show("Data ada yang kosong!!");
            }
            else
            {
                if (TxtPass1.Text == TxtPass2.Text)
                {
                    String query = "UPDATE tKaryawan SET password='" + TxtPass1.Text + "' WHERE username='"
                        + TxtUsername.Text + "' AND no_telepon='" + TxtNoTelp.Text + "'";
                    SqlConnection myConnection = new SqlConnection(Program.koneksi());

                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myConnection;

                    myCommand.CommandText = query;

                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Password tidak cocok!", "Pemberitahuan",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void lbMataTutup_Click_1(object sender, EventArgs e)
        {
            lbMataBuka.Visible = true;
            TxtPass1.UseSystemPasswordChar = false;
            TxtPass2.UseSystemPasswordChar = false;
        }

        private void lbMataBuka_Click_1(object sender, EventArgs e)
        {
            lbMataBuka.Visible = false;
            TxtPass1.UseSystemPasswordChar = true;
            TxtPass2.UseSystemPasswordChar = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
