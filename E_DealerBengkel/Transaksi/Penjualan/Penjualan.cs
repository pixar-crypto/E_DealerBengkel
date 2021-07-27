using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using E_DealerBengkel.Transaksi.Pembelian;
using System.Configuration;

namespace E_DealerBengkel.Transaksi.Penjualan
{
    public partial class Penjualan : Form
    {
        string jenis, idTran, id, idKat, kolom;
        string user, idKendaraan, Harga, Jumlah, hargaBeli;
        double total = 0;
        string Hargabeli;

        CultureInfo culture = new CultureInfo("id-ID");
        Timer timer = new Timer();

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public Penjualan()
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

        private void BtnRetur_Click(object sender, EventArgs e)
        {
            Retur.Retur retur = new Retur.Retur();
            retur.Show();
            this.Hide();
        }

        private void BtnPenjualan_Click(object sender, EventArgs e)
        {
            Penjualan jual = new Penjualan();
            jual.Show();
            this.Hide();
        }

        private void BtnPembelian_Click(object sender, EventArgs e)
        {
            Pembelian.Pembelian beli = new Pembelian.Pembelian();
            beli.Show();
            this.Hide();
        }

        private void BtnServices_Click(object sender, EventArgs e)
        {
            Services.Services services = new Services.Services();
            services.Show();
            this.Hide();
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Kasir_Transaksi kasir = new Kasir_Transaksi();
            kasir.Show();
            this.Hide();
        }

        private void cbJenisBarang_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbJenisBarang.Text == "Mobil")
            {
                jenis = "tMobil";
                kolom = "id_mobil , merek_mobil , warna , jenis_mobil , harga_jual , jumlah , id_supplier ";

            }
            else if (cbJenisBarang.Text == "Motor")
            {
                jenis = "tMotor";
                kolom = "id_motor , merek_motor , warna , jenis_motor , harga_jual , jumlah , id_supplier ";
            }
            else if (cbJenisBarang.Text == "Suku Cadang")
            {
                jenis = "tSukucadang";
                kolom = "id_sukucadang , merek_sukucadang , tipe , jenis_sukucadang , harga_jual , jumlah , id_supplier ";
            }

            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT " + kolom + "FROM " + jenis + " WHERE status='Tersedia'", connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapt.Fill(dt);

                dgvStok.DataSource = dt;
                dgvStok.Columns[0].HeaderText = "ID";
                dgvStok.Columns[1].HeaderText = "Merek";
                dgvStok.Columns[2].HeaderText = "Tipe/Warna";
                dgvStok.Columns[3].HeaderText = "Jenis";
                dgvStok.Columns[4].HeaderText = "Harga Jual";
                dgvStok.Columns[5].HeaderText = "Jumlah";
                dgvStok.Columns[6].HeaderText = "Id Supplier";
                //dgvStok.Columns[7].HeaderText = "Status";

                foreach (DataGridViewColumn colm in dgvStok.Columns)
                {
                    colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                dgvStok.BorderStyle = BorderStyle.None;
                dgvStok.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvStok.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvStok.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvStok.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvStok.BackgroundColor = Color.White;

                dgvStok.EnableHeadersVisualStyles = false;
                dgvStok.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvStok.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvStok.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvStok.Columns[4].DefaultCellStyle.Format = "Rp #,###";

                connection.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Clear()
        {
            txtIdCus.Text = "";
            TxtCariBarang.Text = "";
            txtNamaCus.Text = "";
            txtJumlahBayar.Text = "";
            txtUangBayar.Text = "";
            txtUangKembali.Text = "";
            cbJenisBarang.Text = " -- PILIH JENIS BARANG --";
        }

        private void Penjualan_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;

            cbJenisBarang.Text = " -- PILIH JENIS BARANG --";

            foreach (DataGridViewColumn col in dgvKeranjang.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            dgvKeranjang.BorderStyle = BorderStyle.None;
            dgvKeranjang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvKeranjang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKeranjang.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvKeranjang.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvKeranjang.BackgroundColor = Color.White;

            dgvKeranjang.EnableHeadersVisualStyles = false;
            dgvKeranjang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvKeranjang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvKeranjang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        
        private void dgvKeranjang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BtnTambah.Enabled = true;
            BtnKurang.Enabled = true;
        }

        private void txtUangBayar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtUangBayar_TextChanged(object sender, EventArgs e)
        {
            if (txtUangBayar.Text == "")
            {
                return;
            }
            else
            {
                txtUangBayar.Text = string.Format("{0:n0}", double.Parse(txtUangBayar.Text));
                txtUangBayar.SelectionStart = txtUangBayar.Text.Length;
            }

            try
            {
                double kembali = double.Parse(txtUangBayar.Text) - double.Parse(txtJumlahBayar.Text);
                txtUangKembali.Text = kembali.ToString("#,###");
            }
            catch (Exception ex)
            {

            }
        }

        private void CariId(string user)
        {
            string query = "select * from tKaryawan where username='" + user + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@username", user.Trim());

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                id = Convert.ToString(reader["id_karyawan"]);
            }
            else
                MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = 0;
            int j;
            int i;
            int n = dgvStok.CurrentCell.RowIndex;
            int z = dgvKeranjang.Rows.Count;

            for (int a = 0; a < z; a++)
            {
                if (dgvKeranjang.Rows[a].Cells[0].Value.ToString() == dgvStok.Rows[n].Cells[0].Value.ToString())
                {
                    x = 1;
                    break;
                }
                else
                {
                    x = 0;
                }
            }
            if (x == 0)
            {
                j = 1;

                int cek = int.Parse(dgvStok.Rows[n].Cells[5].Value.ToString());
                if (cek == 0)
                {
                    MessageBox.Show("Maaf, Stock Sudah Habis", "Pemberitahuan!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    i = dgvKeranjang.Rows.Add();
                    dgvKeranjang.Rows[i].Cells[0].Value = dgvStok.Rows[n].Cells[0].Value.ToString();
                    dgvKeranjang.Rows[i].Cells[1].Value = dgvStok.Rows[n].Cells[1].Value.ToString();
                    dgvKeranjang.Rows[i].Cells[2].Value = dgvStok.Rows[n].Cells[2].Value.ToString();
                    dgvKeranjang.Rows[i].Cells[3].Value = dgvStok.Rows[n].Cells[3].Value.ToString();
                    Hargabeli = dgvStok.Rows[n].Cells[4].Value.ToString();
                    Hargabeli = Convert.ToDecimal(Hargabeli).ToString("Rp #,###", culture);
                    dgvKeranjang.Rows[i].Cells[4].Value = Hargabeli;
                    dgvKeranjang.Rows[i].Cells[5].Value = j;
                    dgvKeranjang.Rows[i].Cells[6].Value = dgvStok.Rows[n].Cells[6].Value.ToString();
                    cekBeli();
                }
            }
        }

        private void cekBeli()
        {
            total = 0;
            for (int i = 0; i < dgvKeranjang.Rows.Count; i++)
            {
                dgvKeranjang.Rows[i].Cells[4].Value = dgvKeranjang.Rows[i].Cells[4].Value.ToString().Replace(".", "");
                dgvKeranjang.Rows[i].Cells[4].Value = dgvKeranjang.Rows[i].Cells[4].Value.ToString().Replace("Rp ", "");

                total = total + (double.Parse(dgvKeranjang.Rows[i].Cells[4].Value.ToString()) * double.Parse(dgvKeranjang.Rows[i].Cells[5].Value.ToString()));

                dgvKeranjang.Rows[i].Cells[4].Value = Convert.ToDecimal(dgvKeranjang.Rows[i].Cells[4].Value).ToString("Rp #,###", culture);
                dgvKeranjang.Rows[i].Cells[4].Value = dgvKeranjang.Rows[i].Cells[4].Value;
            }
            txtJumlahBayar.Text = total.ToString("#,###");
        }

        public void isiJenisBarang()
        {
            int a = 0;

            for (int i = 0; i < dgvKeranjang.Rows.Count; i++)
            {
                idKat = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
                idKendaraan = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
                string jum = dgvKeranjang.Rows[i].Cells[5].Value.ToString();

                string kategori = idKat.Substring(0, 3);

                try
                {
                    string cari = TxtCariBarang.Text;

                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                    SqlDataAdapter adapt = new SqlDataAdapter(
                        "SELECT * FROM tKategoriBarangPenjualan WHERE id_jenisBarang='" + idKat + "'", connection);

                    DataSet ds = new DataSet();

                    connection.Open();
                    adapt.Fill(ds);
                    int hitung = ds.Tables[0].Rows.Count;
                    if (hitung == 1)
                    {
                        penguranganBarang(idKat, jum, i);
                        continue;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pemberitahuan!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (a == 1)
                {
                    continue;
                }

                if (kategori == "MBL")
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                    connection.Open();

                    SqlCommand insert = new SqlCommand("[sp_InputKategoriBarang]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_jenisBarang", idKat);
                    insert.Parameters.AddWithValue("id_mobil", idKendaraan);
                    insert.Parameters.AddWithValue("id_motor", SqlDbType.Text).Value = DBNull.Value;
                    insert.Parameters.AddWithValue("id_sukucadang", SqlDbType.Text).Value = DBNull.Value;

                    try
                    {
                        //transaction.Commit();
                        insert.ExecuteNonQuery();
                        penguranganBarang(idKat, jum, i);
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update: " + ex.Message);
                    }
                    connection.Close();
                }
                else if (kategori == "MTR")
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                    connection.Open();

                    SqlCommand insert = new SqlCommand("[sp_InputKategoriBarang]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_jenisBarang", idKat);
                    insert.Parameters.AddWithValue("id_mobil", SqlDbType.Text).Value = DBNull.Value;
                    insert.Parameters.AddWithValue("id_motor", idKendaraan);
                    insert.Parameters.AddWithValue("id_sukucadang", SqlDbType.Text).Value = DBNull.Value;

                    try
                    {
                        //transaction.Commit();
                        insert.ExecuteNonQuery();
                        penguranganBarang(idKat, jum, i);
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update: " + ex.Message);
                    }
                    connection.Close();
                }
                else if (kategori == "SCD")
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                    connection.Open();

                    SqlCommand insert = new SqlCommand("[sp_InputKategoriBarang]", connection);
                    insert.CommandType = CommandType.StoredProcedure;

                    insert.Parameters.AddWithValue("id_jenisBarang", idKat);
                    insert.Parameters.AddWithValue("id_mobil", SqlDbType.Text).Value = DBNull.Value;
                    insert.Parameters.AddWithValue("id_motor", SqlDbType.Text).Value = DBNull.Value;
                    insert.Parameters.AddWithValue("id_sukucadang", idKendaraan);

                    try
                    {
                        //transaction.Commit();
                        insert.ExecuteNonQuery();
                        penguranganBarang(idKat, jum, i);
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update: " + ex.Message);
                    }
                    connection.Close();
                }
            }
        }

        private void txtUangBayar_Leave(object sender, EventArgs e)
        {
            try
            {
                //txtUangBayar.Text = Program.toRupiah(int.Parse(txtUangBayar.Text));
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCariIDmbr_Click(object sender, EventArgs e)
        {
            string query = "select * from tMember where id_member='" + txtIdCus.Text + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_member", txtIdCus.Text.Trim());

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                txtNamaCus.Text = Convert.ToString(reader["nama_member"]);
            }
            else
                MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }

        private void btnCariBarang_Click(object sender, EventArgs e)
        {
            try
            {
                string cari = TxtCariBarang.Text;

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlDataAdapter adapt = new SqlDataAdapter(
                    "SELECT id_mobil AS ID, merek_mobil AS Merek, warna AS Warna, jenis_mobil AS Jenis, harga_jual AS Harga, jumlah AS Jumlah, id_supplier AS Supplier FROM tMobil "
                    + "WHERE merek_mobil='" + cari + "'"
                    + " UNION "
                    + "SELECT id_motor AS ID, merek_motor AS Merek, warna AS Warna, jenis_motor AS Jenis, harga_jual AS Harga, jumlah AS Jumlah, id_supplier AS Supplier FROM tMotor "
                    + "WHERE merek_motor='" + cari + "'"
                    + " UNION "
                    + "SELECT id_sukucadang AS ID, merek_sukucadang AS Merek, tipe AS Warna, jenis_sukucadang AS Jenis, harga_jual AS Harga, jumlah AS Jumlah, id_supplier AS Supplier FROM tSukucadang "
                    + "WHERE jenis_sukucadang='" + cari + "'", connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapt.Fill(dt);
                dgvStok.DataSource = dt;
                dgvStok.Columns[4].DefaultCellStyle.Format = "Rp #,###";
                dgvStok.Columns[5].DefaultCellStyle.Format = "Rp #,###";
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pemberitahuan!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            int jumlah;
            int cek;

            int n = dgvKeranjang.CurrentCell.RowIndex;
            int a = dgvStok.CurrentCell.RowIndex;
            jumlah = int.Parse(dgvKeranjang.Rows[n].Cells[5].Value.ToString());

            jumlah = jumlah + 1;

            cek = int.Parse(dgvStok.Rows[a].Cells[5].Value.ToString()) - jumlah;
            if (cek < 0)
            {
                MessageBox.Show("Maaf, Stok Tidak Cukup", "Warning!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dgvKeranjang.Rows[n].Cells[5].Value = jumlah;
                cekBeli();
            }
           
        }

        private void BtnKurang_Click(object sender, EventArgs e)
        {
            int jumlah;

            int n = dgvKeranjang.CurrentCell.RowIndex;
            jumlah = int.Parse(dgvKeranjang.Rows[n].Cells[5].Value.ToString());

            jumlah = jumlah - 1;

            if (jumlah < 1)
            {
                dgvKeranjang.Rows.RemoveAt(n);
                BtnTambah.Enabled = false;
                BtnKurang.Enabled = false;
            }
            else
            {
                dgvKeranjang.Rows[n].Cells[5].Value = jumlah;
            }
            cekBeli();
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            if (txtNamaCus.Text == "" || txtUangBayar.Text == "" || txtJumlahBayar.Text == "")
            {
                MessageBox.Show("Data masih ada yang kosong", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (double.Parse(txtJumlahBayar.Text) > double.Parse(txtUangBayar.Text))
            {
                MessageBox.Show("Uang anda kurang", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var hasil = MessageBox.Show("Apakah anda yakin?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
                if (hasil == DialogResult.Yes)
                {
                    isiJenisBarang();
                    isiTPenjualan();
                    isiDetailJual();
                    MessageBox.Show("Data transaksi telah disimpan", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    this.dgvKeranjang.Rows.Clear();
                    this.dgvStok.DataSource = null;
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

        public void isiTPenjualan()
        {
            //idTran = Program.autogenerateID("TJB-", "sp_IdPenjualan");
            IdOtomatis a = new IdOtomatis();
            string sp = "sp_IdPenjualan";
            a.setID("TJB-", sp);
            idTran = a.getID();

            string waktu = DateTime.Now.ToString("yyyy-MM-dd");
            user = lbUser.Text.Replace("Hallo, kasir ", "");
            CariId(user);
            string idMember = txtIdCus.Text;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            connection.Open();

            SqlCommand insert = new SqlCommand("[sp_InputTPenjualan]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_penjualan", idTran);
            insert.Parameters.AddWithValue("tanggal", waktu);
            insert.Parameters.AddWithValue("total_harga", Int64.Parse(total.ToString()));
            insert.Parameters.AddWithValue("id_karyawan", id);
            insert.Parameters.AddWithValue("id_jenisBarang", idKat);
            insert.Parameters.AddWithValue("id_member", idMember);

            try
            {
                //transaction.Commit();
                insert.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message);
            }
            connection.Close();
        }

        private void isiDetailJual()
        {
            for (int i = 0; i < dgvKeranjang.Rows.Count; i++)
            {
                string idKat = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
                Harga = dgvKeranjang.Rows[i].Cells[4].Value.ToString();
                Jumlah = dgvKeranjang.Rows[i].Cells[5].Value.ToString();

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                connection.Open();

                SqlCommand input = new SqlCommand("[sp_InputDetailTPenjualan]", connection);
                input.CommandType = CommandType.StoredProcedure;
                Harga = Harga.Replace(".", "");
                Harga = Harga.Replace("Rp ", "");

                input.Parameters.AddWithValue("id_penjualan", idTran);
                input.Parameters.AddWithValue("jumlah_pembelian", int.Parse(Jumlah));
                input.Parameters.AddWithValue("harga_jual_satuan", double.Parse(Harga));
                input.Parameters.AddWithValue("id_jenisBarang", idKat);

                try
                {
                    //transaction.Commit();
                    input.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to update: " + ex.Message);
                }
            }
        }

        private void penguranganBarang(string idKat, string jum, int i)
        {
            string kategori = idKat.Substring(0, 3);

            if (kategori == "MBL")
            {
                string query = "select * from tMobil where id_mobil='" + idKat + "'";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlCommand search = new SqlCommand(query, connection);

                connection.Open();
                search.Parameters.AddWithValue("@id_mobil", idKat.Trim());

                SqlDataReader reader = search.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string jumlah = Convert.ToString(reader["jumlah"]);
                    int hasil = int.Parse(jumlah) - int.Parse(jum);
                    updateBarangMobil(hasil, i);
                }
                else
                    MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                connection.Close();
            }
            else if (kategori == "MTR")
            {
                string query = "select * from tMotor where id_motor='" + idKat + "'";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlCommand search = new SqlCommand(query, connection);

                connection.Open();
                search.Parameters.AddWithValue("@id_motor", idKat.Trim());

                SqlDataReader reader = search.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string jumlah = Convert.ToString(reader["jumlah"]);
                    int hasil = int.Parse(jumlah) - int.Parse(jum);
                    updateBarangMotor(hasil, i);
                }
                else
                    MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                connection.Close();
            }
            else if (kategori == "SCD")
            {
                string query = "select * from tSukucadang where id_sukucadang='" + idKat + "'";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlCommand search = new SqlCommand(query, connection);

                connection.Open();
                search.Parameters.AddWithValue("@id_sukucadang", idKat.Trim());

                SqlDataReader reader = search.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string jumlah = Convert.ToString(reader["jumlah"]);
                    int hasil = int.Parse(jumlah) - int.Parse(jum);
                    updateBarangSpare(hasil, i);
                }
                else
                    MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                connection.Close();
            }
        }

        private void updateBarangMobil(int hasil, int i)
        {
            string idMobil = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
            string merk = dgvKeranjang.Rows[i].Cells[1].Value.ToString();
            string tipe = dgvKeranjang.Rows[i].Cells[2].Value.ToString();
            string jenis = dgvKeranjang.Rows[i].Cells[3].Value.ToString();
            string hargaJual = dgvKeranjang.Rows[i].Cells[4].Value.ToString();
            //hasil;
            string sup = dgvKeranjang.Rows[i].Cells[6].Value.ToString();
            CariMobil(idMobil);
            hargaJual = hargaJual.Replace(".", "");
            hargaJual = hargaJual.Replace("Rp ", "");
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlParameter param = new SqlParameter();

            SqlCommand insert = new SqlCommand("[sp_UpdateMobil]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_mobil", idMobil);
            insert.Parameters.AddWithValue("merek_mobil", merk);
            insert.Parameters.AddWithValue("warna", tipe);
            insert.Parameters.AddWithValue("jenis_mobil", jenis);
            insert.Parameters.AddWithValue("harga_beli", double.Parse(hargaBeli));
            insert.Parameters.AddWithValue("harga_jual", double.Parse(hargaJual));
            insert.Parameters.AddWithValue("jumlah", hasil);
            insert.Parameters.AddWithValue("id_supplier", sup);
            insert.Parameters.AddWithValue("status", "Tersedia");

            try
            {
                //transaction.Commit();
                insert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message, "UpdateBarang");
            }
        }

        private void updateBarangMotor(int hasil, int i)
        {
            string idMotor = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
            string merk = dgvKeranjang.Rows[i].Cells[1].Value.ToString();
            string tipe = dgvKeranjang.Rows[i].Cells[2].Value.ToString();
            string jenis = dgvKeranjang.Rows[i].Cells[3].Value.ToString();
            string hargaJual = dgvKeranjang.Rows[i].Cells[4].Value.ToString();
            //hasil;
            string sup = dgvKeranjang.Rows[i].Cells[6].Value.ToString();
            CariMotor(idMotor);
            hargaJual = hargaJual.Replace(".", "");
            hargaJual = hargaJual.Replace("Rp ", "");
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlParameter param = new SqlParameter();

            SqlCommand insert = new SqlCommand("[sp_UpdateMotor]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_motor", idMotor);
            insert.Parameters.AddWithValue("merek_motor", merk);
            insert.Parameters.AddWithValue("warna", tipe);
            insert.Parameters.AddWithValue("jenis_motor", jenis);
            insert.Parameters.AddWithValue("harga_beli", double.Parse(hargaBeli));
            insert.Parameters.AddWithValue("harga_jual", double.Parse(hargaJual));
            insert.Parameters.AddWithValue("jumlah", hasil);
            insert.Parameters.AddWithValue("id_supplier", sup);
            insert.Parameters.AddWithValue("status", "Tersedia");

            try
            {
                //transaction.Commit();
                insert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message, "UpdateBarang");
            }
        }

        private void updateBarangSpare(int hasil, int i)
        {
            string idSpare = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
            string merk = dgvKeranjang.Rows[i].Cells[1].Value.ToString();
            string tipe = dgvKeranjang.Rows[i].Cells[2].Value.ToString();
            string jenis = dgvKeranjang.Rows[i].Cells[3].Value.ToString();
            string hargaJual = dgvKeranjang.Rows[i].Cells[4].Value.ToString();
            //hasil;
            string sup = dgvKeranjang.Rows[i].Cells[6].Value.ToString();
            CariSpare(idSpare);
            hargaJual = hargaJual.Replace(".", "");
            hargaJual = hargaJual.Replace("Rp ", "");
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlParameter param = new SqlParameter();

            SqlCommand insert = new SqlCommand("[sp_UpdateSukuCadang]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_sukucadang", idSpare);
            insert.Parameters.AddWithValue("merk_sukucadang", merk);
            insert.Parameters.AddWithValue("tipe", tipe);
            insert.Parameters.AddWithValue("jenis_sukucadang", jenis);
            insert.Parameters.AddWithValue("harga_beli", double.Parse(hargaBeli));
            insert.Parameters.AddWithValue("harga_jual", double.Parse(hargaJual));
            insert.Parameters.AddWithValue("jumlah", hasil);
            insert.Parameters.AddWithValue("id_supplier", sup);
            insert.Parameters.AddWithValue("status", "Tersedia");

            try
            {
                //transaction.Commit();
                insert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message, "UpdateBarang");
            }
        }

        private void CariMobil(string idMobil)
        {
            string query = "select * from tMobil where id_mobil='" + idMobil + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                hargaBeli = Convert.ToString(reader["harga_beli"]);
            }
            else
                MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }

        private void CariMotor(string idMotor)
        {
            string query = "select * from tMotor where id_motor='" + idMotor + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                hargaBeli = Convert.ToString(reader["harga_beli"]);
            }
            else
                MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }

        private void CariSpare(string idSpare)
        {
            string query = "select * from tSukucadang where id_sukucadang='" + idSpare + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                hargaBeli = Convert.ToString(reader["harga_beli"]);
            }
            else
                MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }
    }
}
