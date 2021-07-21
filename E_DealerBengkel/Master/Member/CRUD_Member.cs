using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Master.Member
{
    public partial class CRUD_Member : Form
    {
        String id;

        Timer timer = new Timer();

        public CRUD_Member()
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void Clear()
        {
            TxtNama.Text = "";
            TxtNoKTP.Text = "";
            TxtAlamat.Text = "";
            TxtEmail.Text = "";
            TxtNoTelp.Text = "";
            CbStatus.Text = " - PILIH STATUS -";

            if (lbJudul.Text == "TAMBAH MEMBER")
            {

            }
            else
            {
                TxtNoKTP.Enabled = false;
                TxtAlamat.Enabled = false;
                TxtEmail.Enabled = false;
                TxtNoTelp.Enabled = false;
                CbStatus.Enabled = false;
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            Clear();
            TxtNoKTP.Enabled = true;
            TxtAlamat.Enabled = true;
            TxtEmail.Enabled = true;
            TxtNoTelp.Enabled = true;
            CbStatus.Enabled = false;
            lbledit.Visible = false;

            BtnSimpan.Text = "SIMPAN";
            lbJudul.Text = "TAMBAH MEMBER";
            BtnHapus.Visible = false;
            lbStatus.Visible = false;
            CbStatus.Visible = false;
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            Clear();
            TxtNoKTP.Enabled = false;
            TxtAlamat.Enabled = false;
            TxtEmail.Enabled = false;
            TxtNoTelp.Enabled = false;
            CbStatus.Enabled = false;
            lbledit.Visible = true;

            BtnSimpan.Text = "UBAH";
            lbJudul.Text = "UBAH MEMBER";
            BtnHapus.Visible = true;
            lbStatus.Visible = true;
            CbStatus.Visible = true;
            CbStatus.Text = " - PILIH STATUS -";
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Admin_Master Adm_M = new Admin_Master();
            Adm_M.Show();
            this.Hide();
        }

      

        public bool ValidateEmail()
        {
            string regexPattern = @"[\w-]+@([\w-]+\.)+[\w-]+";

            Regex regex = new Regex(regexPattern);

            if (regex.IsMatch(TxtEmail.Text))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        private void CRUD_Member_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;

            RefreshDg();
            CbStatus.Text = " - PILIH STATUS -";
            BtnHapus.Visible = false;
            lbledit.Visible = false;
        }

        public void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tMember", connection);
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

            dgvMember.DataSource = dt;
            dgvMember.Columns[1].HeaderText = "ID";
            dgvMember.Columns[2].HeaderText = "Nama Member";
            dgvMember.Columns[3].HeaderText = "No KTP";
            dgvMember.Columns[4].HeaderText = "Alamat";
            dgvMember.Columns[5].HeaderText = "Email";
            dgvMember.Columns[6].HeaderText = "No Telepon";
            dgvMember.Columns[7].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMember.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMember.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMember.Columns["no_KTP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMember.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            connection.Close();

            dgvMember.BorderStyle = BorderStyle.None;
            dgvMember.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMember.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMember.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMember.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMember.BackgroundColor = Color.White;

            dgvMember.EnableHeadersVisualStyles = false;
            dgvMember.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMember.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMember.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void TxtNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNoKTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNoTelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (TxtNama.Text == "" || TxtNoKTP.Text == "" ||
                TxtAlamat.Text == "" || TxtEmail.Text == "" || TxtNoTelp.Text == "")
            {
                MessageBox.Show("Data ada yang kosong!!", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool validateEmail = ValidateEmail();

                if (validateEmail == true)
                {
                    var hasil = MessageBox.Show("Yakin ingin menghapus data? ", "Information",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);
                    if (hasil == DialogResult.Yes)
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlParameter param = new SqlParameter();

                        SqlCommand delete = new SqlCommand("[sp_DeleteMember]", connection);
                        delete.CommandType = CommandType.StoredProcedure;

                        delete.Parameters.AddWithValue("id_member", id);

                        try
                        {
                            delete.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil dihapus", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshDg();
                            Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to update: " + ex.Message);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Email tidak valid!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MEMBER")
            {
                if (TxtNama.Text == "" || TxtNoKTP.Text == "" || TxtAlamat.Text == "" || TxtEmail.Text == "" || TxtNoTelp.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TxtNoTelp.Text.Length > 13 || TxtNoTelp.Text.Length < 12)
                {
                    MessageBox.Show("No. Telepon maksimal 13 digit!!", "Pemberitahuan",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TxtNoKTP.Text.Length != 16)
                {
                    MessageBox.Show("No. KTP harus 16 digit!!", "Pemberitahuan",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string query = "select top 1 id_member from tMember order by id_member desc";
                    id = Program.autogenerateID("MBR-", query);

                    bool validateEmail = ValidateEmail();

                    if (validateEmail == true)
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlParameter param = new SqlParameter();

                        SqlCommand insert = new SqlCommand("[sp_InputMember]", connection);
                        insert.CommandType = CommandType.StoredProcedure;

                        insert.Parameters.AddWithValue("id_member", id);
                        insert.Parameters.AddWithValue("nama_member", TxtNama.Text);
                        insert.Parameters.AddWithValue("no_KTP", TxtNoKTP.Text);
                        insert.Parameters.AddWithValue("alamat", TxtAlamat.Text);
                        insert.Parameters.AddWithValue("email", TxtEmail.Text);
                        insert.Parameters.AddWithValue("no_telepon", TxtNoTelp.Text);
                        insert.Parameters.AddWithValue("status", "Aktif");

                        try
                        {
                            //transaction.Commit();
                            insert.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil disimpan", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshDg();
                            Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to update: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email tidak valid!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (TxtNama.Text == "" || TxtNoKTP.Text == "" || TxtAlamat.Text == "" || TxtEmail.Text == "" || TxtNoTelp.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TxtNoTelp.Text.Length > 13 || TxtNoTelp.Text.Length < 12)
                {
                    MessageBox.Show("No. Telepon maksimal 13 digit!!", "Pemberitahuan",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TxtNoKTP.Text.Length != 16)
                {
                    MessageBox.Show("No. KTP harus 16 digit!!", "Pemberitahuan",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bool validateEmail = ValidateEmail();

                    if (validateEmail == true)
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlParameter param = new SqlParameter();

                        SqlCommand insert = new SqlCommand("[sp_UpdateMember]", connection);
                        insert.CommandType = CommandType.StoredProcedure;

                        insert.Parameters.AddWithValue("id_member", id);
                        insert.Parameters.AddWithValue("nama_member", TxtNama.Text);
                        insert.Parameters.AddWithValue("no_KTP", TxtNoKTP.Text);
                        insert.Parameters.AddWithValue("alamat", TxtAlamat.Text);
                        insert.Parameters.AddWithValue("email", TxtEmail.Text);
                        insert.Parameters.AddWithValue("no_telepon", TxtNoTelp.Text);
                        insert.Parameters.AddWithValue("status", CbStatus.Text);

                        try
                        {
                            //transaction.Commit();
                            insert.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil disimpan", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshDg();
                            Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to update: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email tidak valid!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void TxtNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MEMBER")
            {

            }
            else
            {
                try
                {
                    TxtNoKTP.Enabled = true;
                    TxtAlamat.Enabled = true;
                    TxtEmail.Enabled = true;
                    TxtNoTelp.Enabled = true;
                    CbStatus.Enabled = true;

                    DataGridViewRow row = this.dgvMember.Rows[e.RowIndex];
                    id = row.Cells[1].Value.ToString();
                    TxtNama.Text = row.Cells[2].Value.ToString();
                    TxtNoKTP.Text = row.Cells[3].Value.ToString();
                    TxtAlamat.Text = row.Cells[4].Value.ToString();
                    TxtEmail.Text = row.Cells[5].Value.ToString();
                    TxtNoTelp.Text = row.Cells[6].Value.ToString();
                    CbStatus.Text = row.Cells[7].Value.ToString();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void rbAktif_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tMember WHERE status='Aktif'", connection);
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

            dgvMember.DataSource = dt;
            dgvMember.Columns[1].HeaderText = "ID";
            dgvMember.Columns[2].HeaderText = "Nama Member";
            dgvMember.Columns[3].HeaderText = "No KTP";
            dgvMember.Columns[4].HeaderText = "Alamat";
            dgvMember.Columns[5].HeaderText = "Email";
            dgvMember.Columns[6].HeaderText = "No Telepon";
            dgvMember.Columns[7].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMember.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMember.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMember.Columns["no_KTP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMember.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            connection.Close();

            dgvMember.BorderStyle = BorderStyle.None;
            dgvMember.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMember.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMember.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMember.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMember.BackgroundColor = Color.White;

            dgvMember.EnableHeadersVisualStyles = false;
            dgvMember.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMember.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMember.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbTidakAktif_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tMember WHERE status='Tidak aktif'", connection);
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

            dgvMember.DataSource = dt;
            dgvMember.Columns[1].HeaderText = "ID";
            dgvMember.Columns[2].HeaderText = "Nama Member";
            dgvMember.Columns[3].HeaderText = "No KTP";
            dgvMember.Columns[4].HeaderText = "Alamat";
            dgvMember.Columns[5].HeaderText = "Email";
            dgvMember.Columns[6].HeaderText = "No Telepon";
            dgvMember.Columns[7].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMember.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMember.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMember.Columns["no_KTP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMember.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            connection.Close();

            dgvMember.BorderStyle = BorderStyle.None;
            dgvMember.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMember.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMember.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMember.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMember.BackgroundColor = Color.White;

            dgvMember.EnableHeadersVisualStyles = false;
            dgvMember.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMember.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMember.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDg();
        }
    }
}
