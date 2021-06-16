using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Master.Services
{
    public partial class CRUD_Services : Form
    {
        //---SERVER UMUM---

        string connectionstring =
                "integrated security=true;data source=localhost;initial catalog=VroomDG";
        String id;

        Timer timer = new Timer();

        public CRUD_Services()
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
            TxtJenisServices.Text = "";
            TxtHarga.Text = "";
            cbStatus.Text = " - PILIH STATUS -";
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            Clear();
            TxtHarga.Enabled = true;
            cbStatus.Enabled = false;

            lbJudul.Text = "TAMBAH SERVICES";
            BtnHapus.Visible = false;
            lbStatus.Visible = false;
            cbStatus.Visible = false;
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            Clear();
            TxtHarga.Enabled = false;
            cbStatus.Enabled = false;

            lbJudul.Text = "UBAH SERVICES";
            BtnHapus.Visible = true;
            lbStatus.Visible = true;
            cbStatus.Visible = true;
            cbStatus.Text = " - PILIH STATUS -";
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Admin_Master Adm_M = new Admin_Master();
            Adm_M.Show();
            this.Hide();
        }

        public string autogenerateID(string firstText, string query)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon;
            string result = "";
            int num = 0;
            try
            {
                sqlCon = new SqlConnection(Program.koneksi());
                sqlCon.Open();
                sqlCmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    string last = reader[0].ToString();
                    num = Convert.ToInt32(last.Remove(0, firstText.Length)) + 1;
                }
                else
                {
                    num = 1;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            result = firstText + num.ToString().PadLeft(2, '0');
            return result;
        }

        private void CRUD_Services_Load(object sender, EventArgs e)
        {
            RefreshDg();
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
        }

        public void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tService", connection);
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

            dgvJenisService.DataSource = dt;
            dgvJenisService.Columns[1].HeaderText = "ID";
            dgvJenisService.Columns[2].HeaderText = "Jenis Service";
            dgvJenisService.Columns[3].HeaderText = "Harga";
            dgvJenisService.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvJenisService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvJenisService.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvJenisService.Columns["harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvJenisService.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvJenisService.BorderStyle = BorderStyle.None;
            dgvJenisService.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvJenisService.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvJenisService.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvJenisService.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvJenisService.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvJenisService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvJenisService.EnableHeadersVisualStyles = false;
            dgvJenisService.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvJenisService.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvJenisService.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (TxtJenisServices.Text == "" || TxtHarga.Text == "" || cbStatus.Text == "")
            {
                MessageBox.Show("Data ada yang kosong!!");
            }
            else
            {
                var hasil = MessageBox.Show("Yakin ingin menghapus data? ", "Information",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
                if (hasil == DialogResult.Yes)
                {
                    SqlConnection connection = new SqlConnection(Program.koneksi());

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand update = new SqlCommand("[sp_UpdateService]", connection);
                    update.CommandType = CommandType.StoredProcedure;

                    update.Parameters.AddWithValue("id_service", id);
                    update.Parameters.AddWithValue("jenis_service", TxtJenisServices.Text);
                    string harga = Program.toAngka(TxtHarga.Text).ToString();
                    update.Parameters.AddWithValue("harga", harga);
                    update.Parameters.AddWithValue("status", "Tidak tersedia");

                    try
                    {
                        update.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dihapus", "Information",
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
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH SERVICES")
            {
                if (TxtJenisServices.Text == "" || TxtHarga.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!!");
                }
                else
                {
                    string query = "select top 1 id_service from tService order by id_service desc";
                    id = autogenerateID("SRV-", query);

                    SqlConnection connection = new SqlConnection(Program.koneksi());

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand insert = new SqlCommand("[sp_InputService]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_service", id);
                    insert.Parameters.AddWithValue("jenis_service", TxtJenisServices.Text);
                    string harga = Program.toAngka(TxtHarga.Text).ToString();
                    insert.Parameters.AddWithValue("harga", harga);
                    insert.Parameters.AddWithValue("status", "Tersedia");

                    try
                    {
                        //transaction.Commit();
                        insert.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Disimpan", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshDg();
                        Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update: " + ex.Message);
                    }
                }
            }
            else
            {
                if (TxtJenisServices.Text == "" || TxtHarga.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!!");
                }
                else
                {
                    SqlConnection connection = new SqlConnection(Program.koneksi());

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand update = new SqlCommand("[sp_UpdateService]", connection);
                    update.CommandType = CommandType.StoredProcedure;

                    update.Parameters.AddWithValue("id_service", id);
                    update.Parameters.AddWithValue("jenis_service", TxtJenisServices.Text);
                    string harga = Program.toAngka(TxtHarga.Text).ToString();
                    update.Parameters.AddWithValue("harga", harga);
                    update.Parameters.AddWithValue("status", cbStatus.SelectedItem);

                    try
                    {
                        //transaction.Commit();
                        update.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Disimpan", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshDg();
                        Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update: " + ex.Message);
                    }
                }
            }
        }

        private void TxtJenisServices_TextChanged(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH SERVICES")
            {

            }
            else
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                SqlDataAdapter adapt = new SqlDataAdapter("select * from tService where jenis_service like '" + TxtJenisServices.Text + "%'", connection);
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

                dgvJenisService.DataSource = dt;
                dgvJenisService.Columns[1].HeaderText = "ID";
                dgvJenisService.Columns[2].HeaderText = "Jenis Service";
                dgvJenisService.Columns[3].HeaderText = "Harga";
                dgvJenisService.Columns[4].HeaderText = "Status";

                foreach (DataGridViewColumn colm in dgvJenisService.Columns)
                {
                    colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                this.dgvJenisService.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvJenisService.Columns["harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvJenisService.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
                connection.Close();

                dgvJenisService.BorderStyle = BorderStyle.None;
                dgvJenisService.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvJenisService.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvJenisService.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvJenisService.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvJenisService.BackgroundColor = Color.White;

                foreach (DataGridViewColumn colm in dgvJenisService.Columns)
                {
                    colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                dgvJenisService.EnableHeadersVisualStyles = false;
                dgvJenisService.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvJenisService.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvJenisService.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dgvJenisService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lbJudul.Text == "TAMBAH SERVICES")
            {

            }
            else
            {
                try
                {
                    CultureInfo culture = new CultureInfo("id-ID");

                    TxtJenisServices.Enabled = true;
                    TxtHarga.Enabled = true;
                    cbStatus.Enabled = true;

                    DataGridViewRow row = this.dgvJenisService.Rows[e.RowIndex];
                    id = row.Cells[0].Value.ToString();
                    TxtJenisServices.Text = row.Cells[2].Value.ToString();
                    String harga = row.Cells[3].Value.ToString();
                    harga = Convert.ToDecimal(harga).ToString("c", culture);
                    TxtHarga.Text = harga.Replace("Rp", "");
                    cbStatus.Text = row.Cells[4].Value.ToString();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void rbTersedia_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tService WHERE status='Tersedia'", connection);
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

            dgvJenisService.DataSource = dt;
            dgvJenisService.Columns[1].HeaderText = "ID";
            dgvJenisService.Columns[2].HeaderText = "Jenis Service";
            dgvJenisService.Columns[3].HeaderText = "Harga";
            dgvJenisService.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvJenisService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvJenisService.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvJenisService.Columns["harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvJenisService.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvJenisService.BorderStyle = BorderStyle.None;
            dgvJenisService.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvJenisService.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvJenisService.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvJenisService.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvJenisService.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvJenisService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvJenisService.EnableHeadersVisualStyles = false;
            dgvJenisService.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvJenisService.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvJenisService.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbTidakTersedia_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tService WHERE status='Tidak Tersedia'", connection);
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

            dgvJenisService.DataSource = dt;
            dgvJenisService.Columns[1].HeaderText = "ID";
            dgvJenisService.Columns[2].HeaderText = "Jenis Service";
            dgvJenisService.Columns[3].HeaderText = "Harga";
            dgvJenisService.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvJenisService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvJenisService.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvJenisService.Columns["harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvJenisService.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvJenisService.BorderStyle = BorderStyle.None;
            dgvJenisService.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvJenisService.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvJenisService.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvJenisService.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvJenisService.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvJenisService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvJenisService.EnableHeadersVisualStyles = false;
            dgvJenisService.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvJenisService.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvJenisService.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDg();
        }

        private void TxtHarga_Leave(object sender, EventArgs e)
        {
            try
            {
                TxtHarga.Text = Program.toRupiah(int.Parse(TxtHarga.Text));
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtHarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
