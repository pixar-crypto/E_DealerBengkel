using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Master.Motor
{
    public partial class CRUD_Motor : Form
    {
        //---SERVER UMUM---

        string connectionstring =
                "integrated security=true;data source=localhost;initial catalog=VroomDG";
        String id;

        Timer timer = new Timer();

        public CRUD_Motor()
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

        private void CRUD_Motor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vroomDGDataSet.tSupplier' table. You can move, or remove it, as needed.
            this.tSupplierTableAdapter.Fill(this.vroomDGDataSet.tSupplier);
            // TODO: This line of code loads data into the 'vroomDGDataSet2.tSupplier' table. You can move, or remove it, as needed.
       
          
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

            if (lbJudul.Text == "TAMBAH MOTOR")
            {

            }
            else
            {
                TxtWarna.Enabled = false;
                cbJenis.Enabled = false;
                TxtHargaBeli.Enabled = false;
                TxtHargaJual.Enabled = false;
                TxtJumlah.Enabled = false;
                cmbSup.Enabled = false;
                cbStatus.Enabled = false;
            }
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

            BtnSimpan.Text = "SIMPAN";
            lbJudul.Text = "TAMBAH MOTOR";
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

            BtnSimpan.Text = "UBAH";
            lbJudul.Text = "UBAH MOTOR";
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
            SqlDataAdapter adapt = new SqlDataAdapter("select m.id_motor, m.merek_motor, m.warna, m.jenis_motor, m.harga_beli, m.harga_jual, m.jumlah," +
                "s.nama_supplier, m.status from tMotor AS m INNER JOIN tSupplier s on m.id_supplier = s.id_supplier", connection);
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

            dgvMotor.DataSource = dt;
            dgvMotor.Columns[1].HeaderText = "ID";
            dgvMotor.Columns[2].HeaderText = "Merek";
            dgvMotor.Columns[3].HeaderText = "Warna";
            dgvMotor.Columns[4].HeaderText = "Jenis";
            dgvMotor.Columns[5].HeaderText = "Harga Beli";
            dgvMotor.Columns[6].HeaderText = "Harga Jual";
            dgvMotor.Columns[7].HeaderText = "Jumlah";
            dgvMotor.Columns[8].HeaderText = "Supplier";
            dgvMotor.Columns[9].HeaderText = "Status";

            this.dgvMotor.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMotor.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMotor.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMotor.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMotor.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
            dgvMotor.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvMotor.BorderStyle = BorderStyle.None;
            dgvMotor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMotor.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMotor.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMotor.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMotor.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvMotor.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvMotor.EnableHeadersVisualStyles = false;
            dgvMotor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMotor.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMotor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
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

                    SqlCommand delete = new SqlCommand("[sp_DeleteMotor]", connection);
                    delete.CommandType = CommandType.StoredProcedure;

                    delete.Parameters.AddWithValue("id_motor", id);       

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
                        MessageBox.Show("Unable to delete: " + ex.Message);
                    }
                }
                else
                {

                }
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MOTOR")
            {
                if (TxtMerek.Text == "" || TxtWarna.Text == "" || cbJenis.Text == " - PILIH JENIS -" ||
                    TxtHargaBeli.Text == "" || TxtJumlah.Text == "" || cmbSup.Text == " - PILIH SUPPLIER -")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Information!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string query = "select top 1 id_motor from tMotor order by id_motor desc";
                    String id = autogenerateID("MTR-", query);

                    SqlConnection connection = new SqlConnection(Program.koneksi());

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand insert = new SqlCommand("[sp_InputMotor]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_motor", id);
                    insert.Parameters.AddWithValue("merek_motor", TxtMerek.Text);
                    insert.Parameters.AddWithValue("warna", TxtWarna.Text);
                    insert.Parameters.AddWithValue("jenis_motor", cbJenis.Text);
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

                    SqlCommand insert = new SqlCommand("[sp_UpdateMotor]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_motor", id);
                    insert.Parameters.AddWithValue("merek_motor", TxtMerek.Text);
                    insert.Parameters.AddWithValue("warna", TxtWarna.Text);
                    insert.Parameters.AddWithValue("jenis_motor", cbJenis.Text);
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

        }

        private void dgvMotor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lbJudul.Text == "TAMBAH MOTOR")
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

                    DataGridViewRow row = this.dgvMotor.Rows[e.RowIndex];
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
            SqlDataAdapter adapt = new SqlDataAdapter("select m.id_motor, m.merek_motor, m.warna, m.jenis_motor, m.harga_beli, m.harga_jual, m.jumlah," +
                "s.nama_supplier, m.status from tMotor AS m INNER JOIN tSupplier s on m.id_supplier = s.id_supplier WHERE m.status='Tersedia'", connection);
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

            dgvMotor.DataSource = dt;
            dgvMotor.Columns[1].HeaderText = "ID";
            dgvMotor.Columns[2].HeaderText = "Merek";
            dgvMotor.Columns[3].HeaderText = "Warna";
            dgvMotor.Columns[4].HeaderText = "Jenis";
            dgvMotor.Columns[5].HeaderText = "Harga Beli";
            dgvMotor.Columns[6].HeaderText = "Harga Jual";
            dgvMotor.Columns[7].HeaderText = "Jumlah";
            dgvMotor.Columns[8].HeaderText = "Supplier";
            dgvMotor.Columns[9].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMotor.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMotor.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMotor.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMotor.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMotor.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMotor.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
            dgvMotor.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvMotor.BorderStyle = BorderStyle.None;
            dgvMotor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMotor.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMotor.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMotor.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMotor.BackgroundColor = Color.White;

            dgvMotor.EnableHeadersVisualStyles = false;
            dgvMotor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMotor.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMotor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbTidakTersedia_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlDataAdapter adapt = new SqlDataAdapter("select m.id_motor, m.merek_motor, m.warna, m.jenis_motor, m.harga_beli, m.harga_jual, m.jumlah," +
                "s.nama_supplier, m.status from tMotor AS m INNER JOIN tSupplier s on m.id_supplier = s.id_supplier WHERE m.status='Tidak Tersedia'", connection);
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

            dgvMotor.DataSource = dt;
            dgvMotor.Columns[1].HeaderText = "ID";
            dgvMotor.Columns[2].HeaderText = "Merek";
            dgvMotor.Columns[3].HeaderText = "Warna";
            dgvMotor.Columns[4].HeaderText = "Jenis";
            dgvMotor.Columns[5].HeaderText = "Harga Beli";
            dgvMotor.Columns[6].HeaderText = "Harga Jual";
            dgvMotor.Columns[7].HeaderText = "Jumlah";
            dgvMotor.Columns[8].HeaderText = "Supplier";
            dgvMotor.Columns[9].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvMotor.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvMotor.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvMotor.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMotor.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvMotor.Columns["jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMotor.Columns[5].DefaultCellStyle.Format = "Rp #,###.00";
            dgvMotor.Columns[6].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvMotor.BorderStyle = BorderStyle.None;
            dgvMotor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvMotor.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMotor.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvMotor.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMotor.BackgroundColor = Color.White;

            dgvMotor.EnableHeadersVisualStyles = false;
            dgvMotor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMotor.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvMotor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDg();
        }

        private void txtHargaBeli_TextChanged(object sender, EventArgs e)
        {
            if (TxtHargaBeli.Text == "")
            {
                return;
            }
            else
            {
                TxtHargaBeli.Text = string.Format("{0:n0}", double.Parse(TxtHargaBeli.Text));
                TxtHargaBeli.SelectionStart = TxtHargaBeli.Text.Length;
            }
        }

        private void TxtHargaJual_TextChanged(object sender, EventArgs e)
        {
            if (TxtHargaJual.Text == "")
            {
                return;
            }
            else
            {
                TxtHargaJual.Text = string.Format("{0:n0}", double.Parse(TxtHargaJual.Text));
                TxtHargaJual.SelectionStart = TxtHargaJual.Text.Length;
            }
        }
    }
}
