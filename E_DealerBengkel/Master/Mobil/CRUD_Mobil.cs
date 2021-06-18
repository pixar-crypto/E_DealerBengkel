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

namespace E_DealerBengkel.Master.Mobil
{
    public partial class CRUD_Mobil : Form
    {

        //---SERVER UMUM---

        string connectionstring =
                "integrated security=true;data source=localhost;initial catalog=VroomDG";
        String id;

        Timer timer = new Timer();

        public CRUD_Mobil()
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

        private void CRUD_Mobil_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vroomDGDataSet2.tSupplier' table. You can move, or remove it, as needed.
            this.tSupplierTableAdapter.Fill(this.vroomDGDataSet2.tSupplier);

            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;

            RefreshDg();
            cmbSup.Text = " - PILIH SUPPLIER -";
            cbJenis.Text = " - PILIH JENIS -";
            BtnHapus.Visible = false;
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
            TxtMerek.Text = "";
            TxtWarna.Text = "";
            cbJenis.Text = " - PILIH JENIS -";
            TxtHargaBeli.Text = "";
            TxtHargaJual.Text = "";
            TxtJumlah.Text = "";
            cmbSup.Text = " - PILIH SUPPLIER -";

            TxtWarna.Enabled = false;
            cbJenis.Enabled = false;
            TxtHargaBeli.Enabled = false;
            TxtHargaJual.Enabled = false;
            TxtJumlah.Enabled = false;
            cmbSup.Enabled = false;
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            Clear();
            cbJenis.Enabled = true;
            TxtWarna.Enabled = true;
            TxtHargaBeli.Enabled = true;
            TxtHargaJual.Enabled = true;
            TxtJumlah.Enabled = true;
            cmbSup.Enabled = true;
            cbStatus.Enabled = false;

            lbJudul.Text = "TAMBAH MOBIL";
            BtnHapus.Visible = false;
            lbStatus.Visible = false;
            cbStatus.Visible = false;
            cmbSup.Text = " - PILIH SUPPLIER -";
            cbJenis.Text = " - PILIH JENIS -";
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            Clear();
            cbJenis.Enabled = false;
            TxtWarna.Enabled = false;
            TxtHargaBeli.Enabled = false;
            TxtHargaJual.Enabled = false;
            TxtJumlah.Enabled = false;
            cmbSup.Enabled = false;
            cbStatus.Enabled = false;

            lbJudul.Text = "UBAH MOBIL";
            BtnHapus.Visible = true;
            lbStatus.Visible = true;
            cbStatus.Visible = true;
            cbStatus.Text = " - PILIH STATUS -";
            cmbSup.Text = " - PILIH SUPPLIER -";
            cbJenis.Text = " - PILIH JENIS -";
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

        public void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tMobil", connection);
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

            dgvMobil.DataSource = dt;
            dgvMobil.Columns[1].HeaderText = "ID";
            dgvMobil.Columns[2].HeaderText = "Merek";
            dgvMobil.Columns[3].HeaderText = "Warna";
            dgvMobil.Columns[4].HeaderText = "Jenis";
            dgvMobil.Columns[5].HeaderText = "Harga Beli";
            dgvMobil.Columns[6].HeaderText = "Harga Jual";
            dgvMobil.Columns[7].HeaderText = "Jumlah";
            dgvMobil.Columns[8].HeaderText = "Supplier";
            dgvMobil.Columns[9].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMobil.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMobil.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMobil.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMobil.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMobil.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMobil.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
            dgvMobil.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvMobil.BorderStyle = BorderStyle.None;
            dgvMobil.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMobil.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMobil.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMobil.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMobil.BackgroundColor = Color.White;

            dgvMobil.EnableHeadersVisualStyles = false;
            dgvMobil.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMobil.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMobil.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void TxtHargaBeli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtHargaJual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtJumlah_KeyPress(object sender, KeyPressEventArgs e)
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
            if (TxtMerek.Text == "" || TxtWarna.Text == "" || cbJenis.Text == " - PILIH JENIS -" ||
                TxtHargaBeli.Text == "" || TxtJumlah.Text == "" || cmbSup.Text == " - PILIH SUPPLIER -")
            {
                MessageBox.Show("Data ada yang kosong!!", "Information!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    SqlCommand insert = new SqlCommand("[sp_DeleteMobil]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_mobil", id);

                    try
                    {
                        insert.ExecuteNonQuery();
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
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MOBIL")
            {
                if (TxtMerek.Text == "" || TxtWarna.Text == "" || cbJenis.Text == " - PILIH JENIS -" ||
                    TxtHargaBeli.Text == "" || TxtJumlah.Text == "" || cmbSup.Text == " - PILIH SUPPLIER -")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Information!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string query = "select top 1 id_mobil from tMobil order by id_mobil desc";
                    String id = autogenerateID("MBL-", query);

                    SqlConnection connection = new SqlConnection(Program.koneksi());

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand insert = new SqlCommand("[sp_InputMobil]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_mobil", id);
                    insert.Parameters.AddWithValue("merek_mobil", TxtMerek.Text);
                    insert.Parameters.AddWithValue("warna", TxtWarna.Text);
                    insert.Parameters.AddWithValue("jenis_mobil", cbJenis.Text);
                    string hargaBeli = Program.toAngka(TxtHargaBeli.Text).ToString();
                    insert.Parameters.AddWithValue("harga_beli", hargaBeli);
                    string hargaJual = Program.toAngka(TxtHargaJual.Text).ToString();
                    insert.Parameters.AddWithValue("harga_jual", hargaJual);
                    insert.Parameters.AddWithValue("jumlah", TxtJumlah.Text);
                    insert.Parameters.AddWithValue("id_supplier", cmbSup.SelectedValue);
                    insert.Parameters.AddWithValue("status", "Tersedia");

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
            }
            else
            {
                if (TxtMerek.Text == "" || TxtWarna.Text == "" || cbJenis.Text == " - PILIH JENIS -" ||
                    TxtHargaBeli.Text == "" || TxtJumlah.Text == "" || cmbSup.Text == " - PILIH SUPPLIER -")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Information!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlConnection connection = new SqlConnection(Program.koneksi());

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand insert = new SqlCommand("[sp_UpdateMobil]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_mobil", id);
                    insert.Parameters.AddWithValue("merek_mobil", TxtMerek.Text);
                    insert.Parameters.AddWithValue("warna", TxtWarna.Text);
                    insert.Parameters.AddWithValue("jenis_mobil", cbJenis.Text);
                    string hargaBeli = Program.toAngka(TxtHargaBeli.Text).ToString();
                    insert.Parameters.AddWithValue("harga_beli", hargaBeli);
                    string hargaJual = Program.toAngka(TxtHargaJual.Text).ToString();
                    insert.Parameters.AddWithValue("harga_jual", hargaJual);
                    insert.Parameters.AddWithValue("jumlah", TxtJumlah.Text);
                    insert.Parameters.AddWithValue("id_supplier", cmbSup.SelectedValue);
                    insert.Parameters.AddWithValue("status", cbStatus.Text);

                    try
                    {
                        //transaction.Commit();
                        insert.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil diperbarui", "Information",
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

        private void TxtMerek_TextChanged(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MOBIL")
            {

            }
            else
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());
                SqlDataAdapter adapt = new SqlDataAdapter("select * from tMobil where merek_mobil like '" + TxtMerek.Text + "%'", connection);
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

                dgvMobil.DataSource = dt;
                dgvMobil.Columns[1].HeaderText = "ID";
                dgvMobil.Columns[2].HeaderText = "Merek";
                dgvMobil.Columns[3].HeaderText = "Warna";
                dgvMobil.Columns[4].HeaderText = "Jenis";
                dgvMobil.Columns[5].HeaderText = "Harga Beli";
                dgvMobil.Columns[6].HeaderText = "Harga Jual";
                dgvMobil.Columns[7].HeaderText = "Jumlah";
                dgvMobil.Columns[8].HeaderText = "Supplier";
                dgvMobil.Columns[9].HeaderText = "Status";

                foreach (DataGridViewColumn colm in dgvMobil.Columns)
                {
                    colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                this.dgvMobil.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvMobil.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvMobil.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvMobil.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMobil.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
                dgvMobil.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
                connection.Close();

                dgvMobil.BorderStyle = BorderStyle.None;
                dgvMobil.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvMobil.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvMobil.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvMobil.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvMobil.BackgroundColor = Color.White;

                dgvMobil.EnableHeadersVisualStyles = false;
                dgvMobil.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvMobil.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvMobil.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dgvMobil_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MOBIL")
            {

            }
            else
            {
                try
                {
                    CultureInfo culture = new CultureInfo("id-ID");

                    cbJenis.Enabled = true;
                    TxtWarna.Enabled = true;
                    TxtHargaBeli.Enabled = true;
                    TxtHargaJual.Enabled = true;
                    TxtJumlah.Enabled = true;
                    cmbSup.Enabled = true;
                    cbStatus.Enabled = true;

                    DataGridViewRow row = this.dgvMobil.Rows[e.RowIndex];
                    id = row.Cells[1].Value.ToString();
                    TxtMerek.Text = row.Cells[2].Value.ToString();
                    TxtWarna.Text = row.Cells[3].Value.ToString();
                    cbJenis.Text = row.Cells[4].Value.ToString();
                    String hargabeli = row.Cells[5].Value.ToString();
                    hargabeli = Convert.ToDecimal(hargabeli).ToString("c", culture);
                    String hargajual = row.Cells[6].Value.ToString();
                    hargajual = Convert.ToDecimal(hargajual).ToString("c", culture);
                    TxtHargaBeli.Text = hargabeli.Replace("Rp", "");
                    TxtHargaJual.Text = hargajual.Replace("Rp", "");
                    TxtJumlah.Text = row.Cells[7].Value.ToString();
                    cmbSup.Text = row.Cells[8].Value.ToString();
                    cbStatus.Text = row.Cells[9].Value.ToString();
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
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tMobil WHERE status='Tersedia'", connection);
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

            dgvMobil.DataSource = dt;
            dgvMobil.Columns[1].HeaderText = "ID";
            dgvMobil.Columns[2].HeaderText = "Merek";
            dgvMobil.Columns[3].HeaderText = "Warna";
            dgvMobil.Columns[4].HeaderText = "Jenis";
            dgvMobil.Columns[5].HeaderText = "Harga Beli";
            dgvMobil.Columns[6].HeaderText = "Harga Jual";
            dgvMobil.Columns[7].HeaderText = "Jumlah";
            dgvMobil.Columns[8].HeaderText = "Supplier";
            dgvMobil.Columns[9].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMobil.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMobil.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMobil.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMobil.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMobil.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMobil.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
            dgvMobil.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvMobil.BorderStyle = BorderStyle.None;
            dgvMobil.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMobil.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMobil.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMobil.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMobil.BackgroundColor = Color.White;

            dgvMobil.EnableHeadersVisualStyles = false;
            dgvMobil.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMobil.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMobil.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbTidakTersedia_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tMobil WHERE status='Tidak Tersedia'", connection);
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

            dgvMobil.DataSource = dt;
            dgvMobil.Columns[1].HeaderText = "ID";
            dgvMobil.Columns[2].HeaderText = "Merek";
            dgvMobil.Columns[3].HeaderText = "Warna";
            dgvMobil.Columns[4].HeaderText = "Jenis";
            dgvMobil.Columns[5].HeaderText = "Harga Beli";
            dgvMobil.Columns[6].HeaderText = "Harga Jual";
            dgvMobil.Columns[7].HeaderText = "Jumlah";
            dgvMobil.Columns[8].HeaderText = "Supplier";
            dgvMobil.Columns[9].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMobil.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMobil.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMobil.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMobil.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMobil.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMobil.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
            dgvMobil.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvMobil.BorderStyle = BorderStyle.None;
            dgvMobil.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMobil.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMobil.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMobil.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMobil.BackgroundColor = Color.White;

            dgvMobil.EnableHeadersVisualStyles = false;
            dgvMobil.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMobil.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMobil.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDg();
        }

        private void TxtHargaBeli_Leave(object sender, EventArgs e)
        {
            try
            {
                TxtHargaBeli.Text = Program.toRupiah(int.Parse(TxtHargaBeli.Text));
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtHargaJual_Leave(object sender, EventArgs e)
        {
            try
            {
                TxtHargaJual.Text = Program.toRupiah(int.Parse(TxtHargaJual.Text));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
