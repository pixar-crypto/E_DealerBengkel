using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Master.Supplier
{
    public partial class CRUD_Supplier : Form
    {
        String id;

        Timer timer = new Timer();

        public CRUD_Supplier()
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
            TxtCompName.Text = "";
            TxtAlamat.Text = "";
            TxtEmail.Text = "";
            TxtNoTelp.Text = "";

            if(lbJudul.Text == "TAMBAH SUPPLIER")
            {

            }
            else
            {
                TxtAlamat.Enabled = false;
                TxtEmail.Enabled = false;
                TxtNoTelp.Enabled = false;
            }
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

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            Clear();
            TxtAlamat.Enabled = true;
            TxtEmail.Enabled = true;
            TxtNoTelp.Enabled = true;
            CbStatus.Enabled = false;
            lbledit.Visible = false;

            BtnSimpan.Text = "SIMPAN";
            lbJudul.Text = "TAMBAH SUPPLIER";
            BtnHapus.Visible = false;
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            Clear();
            TxtAlamat.Enabled = false;
            TxtEmail.Enabled = false;
            TxtNoTelp.Enabled = false;
            CbStatus.Enabled = false;
            lbledit.Visible = true;

            BtnSimpan.Text = "UBAH";
            lbJudul.Text = "UBAH SUPPLIER";
            BtnHapus.Visible = true;
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Admin_Master Adm_M = new Admin_Master();
            Adm_M.Show();
            this.Hide();
        }

        public void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tSupplier", connection);
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

            dgvSupplier.DataSource = dt;
            dgvSupplier.Columns[1].HeaderText = "ID";
            dgvSupplier.Columns[2].HeaderText = "Nama Supplier";
            dgvSupplier.Columns[3].HeaderText = "Alamat";
            dgvSupplier.Columns[4].HeaderText = "Email";
            dgvSupplier.Columns[5].HeaderText = "No Telepon";
            dgvSupplier.Columns[6].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvSupplier.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvSupplier.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvSupplier.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            connection.Close();

            dgvSupplier.BorderStyle = BorderStyle.None;
            dgvSupplier.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvSupplier.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSupplier.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvSupplier.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvSupplier.BackgroundColor = Color.White;

            dgvSupplier.EnableHeadersVisualStyles = false;
            dgvSupplier.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSupplier.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvSupplier.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void CRUD_Supplier_Load(object sender, EventArgs e)
        {
            RefreshDg();
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
            BtnHapus.Visible = false;
            lbledit.Visible = false;
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
            if (TxtCompName.Text == "" || TxtAlamat.Text == "" || TxtNoTelp.Text == "" || TxtEmail.Text == "")
            {
                MessageBox.Show("Data ada yang kosong!!", "Information!",
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

                        SqlCommand delete = new SqlCommand("[sp_DeleteSupplier]", connection);
                        delete.CommandType = CommandType.StoredProcedure;

                        delete.Parameters.AddWithValue("id_supplier", id);

                        try
                        {
                            delete.ExecuteNonQuery();
                            MessageBox.Show("Delete data succesfully", "Information",
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
                    MessageBox.Show("Format email salah !", "Pemberitahuan!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        class IdOtomatis
        {
            string result;
            public void setID(string firstText, string sp)
            {
                SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlCommand sqlCmd;
                int num = 0;
                try
                {
                    sqlCmd = new SqlCommand(sp, sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCon.Open();
                    SqlDataReader dr = sqlCmd.ExecuteReader();
                    dr.Read();
                    if (dr["idReturn"].ToString() == "")
                    {
                        num = 1;
                    }
                    else
                    {
                        num = Int32.Parse(dr["idReturn"].ToString());
                    }
                    if (num < 10)
                    {
                        result = firstText + "000" + num;
                    }
                    else if (num < 100)
                    {
                        result = firstText + "00" + num;
                    }
                    else if (num < 1000)
                    {
                        result = firstText + "0" + num;
                    }
                    else
                    {
                        result = firstText + num;
                    }
                    dr.Close();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught: {0}", ex);
                }
            }

            public string getID()
            {
                return result;
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH SUPPLIER")
            {
                if (TxtCompName.Text == "" || TxtEmail.Text == "" || TxtAlamat.Text == "" || TxtNoTelp.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TxtNoTelp.Text.Length > 13 || TxtNoTelp.Text.Length < 12)
                {
                    MessageBox.Show("No. Telepon maksimal 13 digit!!", "Pemberitahuan",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //id = Program.autogenerateID("SUP-", "sp_IdSupplier");
                    IdOtomatis a = new IdOtomatis();
                    string sp = "sp_IdSupplier";
                    a.setID("SUP-", sp);
                    string id = a.getID();

                    bool validateEmail = ValidateEmail();

                    if (validateEmail == true)
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlParameter param = new SqlParameter();

                        SqlCommand insert = new SqlCommand("sp_InputSupplier", connection);
                        insert.CommandType = CommandType.StoredProcedure;

                        insert.Parameters.AddWithValue("id_supplier", id);
                        insert.Parameters.AddWithValue("company_name", TxtCompName.Text);
                        insert.Parameters.AddWithValue("address", TxtAlamat.Text);
                        insert.Parameters.AddWithValue("email", TxtEmail.Text);
                        insert.Parameters.AddWithValue("no_telp", TxtNoTelp.Text);
                        insert.Parameters.AddWithValue("status", "Aktif");

                        try
                        {
                            insert.ExecuteNonQuery();
                            MessageBox.Show("Data saved succesfully", "Information",
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
                        MessageBox.Show("Format email salah !", "Pemberitahuan!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (TxtCompName.Text == "" || TxtAlamat.Text == "" || TxtNoTelp.Text == "" || TxtEmail.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Information!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TxtNoTelp.Text.Length > 13 || TxtNoTelp.Text.Length < 12)
                {
                    MessageBox.Show("No. Telepon maksimal 13 digit!!", "Pemberitahuan",
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

                        SqlCommand insert = new SqlCommand("[sp_UpdateSupplier]", connection);
                        insert.CommandType = CommandType.StoredProcedure;

                        insert.Parameters.AddWithValue("id_supplier", id);
                        insert.Parameters.AddWithValue("company_name", TxtCompName.Text);
                        insert.Parameters.AddWithValue("address", TxtAlamat.Text);
                        insert.Parameters.AddWithValue("email", TxtEmail.Text);
                        insert.Parameters.AddWithValue("no_telp", TxtNoTelp.Text);
                        insert.Parameters.AddWithValue("status", CbStatus.SelectedItem);

                        try
                        {
                            insert.ExecuteNonQuery();
                            MessageBox.Show("Data saved succesfully", "Information",
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
                        MessageBox.Show("Format email salah !", "Pemberitahuan!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void TxtCompName_TextChanged(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH SUPPLIER")
            {

            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlDataAdapter adapt = new SqlDataAdapter("select * from tSupplier where nama_supplier like '" + TxtCompName.Text + "%'", connection);
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

                dgvSupplier.DataSource = dt;
                dgvSupplier.Columns[1].HeaderText = "ID";
                dgvSupplier.Columns[2].HeaderText = "Nama Supplier";
                dgvSupplier.Columns[3].HeaderText = "Alamat";
                dgvSupplier.Columns[4].HeaderText = "Email";
                dgvSupplier.Columns[5].HeaderText = "No Telepon";
                dgvSupplier.Columns[6].HeaderText = "Status";

                foreach (DataGridViewColumn colm in dgvSupplier.Columns)
                {
                    colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                this.dgvSupplier.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvSupplier.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                connection.Close();

                dgvSupplier.BorderStyle = BorderStyle.None;
                dgvSupplier.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvSupplier.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvSupplier.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvSupplier.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvSupplier.BackgroundColor = Color.White;

                dgvSupplier.EnableHeadersVisualStyles = false;
                dgvSupplier.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvSupplier.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvSupplier.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lbJudul.Text == "TAMBAH SUPPLIER")
            {

            }
            else
            {
                try
                {
                    CultureInfo culture = new CultureInfo("id-ID");

                    TxtAlamat.Enabled = true;
                    TxtEmail.Enabled = true;
                    TxtNoTelp.Enabled = true;
                    CbStatus.Enabled = true;

                    DataGridViewRow row = this.dgvSupplier.Rows[e.RowIndex];
                    id = row.Cells[1].Value.ToString();
                    TxtCompName.Text = row.Cells[2].Value.ToString();
                    TxtAlamat.Text = row.Cells[3].Value.ToString();
                    TxtEmail.Text = row.Cells[4].Value.ToString();
                    TxtNoTelp.Text = row.Cells[5].Value.ToString();
                    CbStatus.Text = row.Cells[6].Value.ToString();
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
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tSupplier WHERE status='Aktif'", connection);
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

            dgvSupplier.DataSource = dt;
            dgvSupplier.Columns[1].HeaderText = "ID";
            dgvSupplier.Columns[2].HeaderText = "Nama Supplier";
            dgvSupplier.Columns[3].HeaderText = "Alamat";
            dgvSupplier.Columns[4].HeaderText = "Email";
            dgvSupplier.Columns[5].HeaderText = "No Telepon";
            dgvSupplier.Columns[6].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvSupplier.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvSupplier.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvSupplier.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            connection.Close();

            dgvSupplier.BorderStyle = BorderStyle.None;
            dgvSupplier.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvSupplier.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSupplier.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvSupplier.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvSupplier.BackgroundColor = Color.White;

            dgvSupplier.EnableHeadersVisualStyles = false;
            dgvSupplier.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSupplier.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvSupplier.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbTidakAktif_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tSupplier WHERE status='Tidak Aktif'", connection);
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

            dgvSupplier.DataSource = dt;
            dgvSupplier.Columns[1].HeaderText = "ID";
            dgvSupplier.Columns[2].HeaderText = "Nama Supplier";
            dgvSupplier.Columns[3].HeaderText = "Alamat";
            dgvSupplier.Columns[4].HeaderText = "Email";
            dgvSupplier.Columns[5].HeaderText = "No Telepon";
            dgvSupplier.Columns[6].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvSupplier.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvSupplier.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvSupplier.Columns["no_telepon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            connection.Close();

            dgvSupplier.BorderStyle = BorderStyle.None;
            dgvSupplier.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvSupplier.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSupplier.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvSupplier.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvSupplier.BackgroundColor = Color.White;

            dgvSupplier.EnableHeadersVisualStyles = false;
            dgvSupplier.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSupplier.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvSupplier.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDg();
        }
    }
}
