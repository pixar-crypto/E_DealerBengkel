using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
//using E_DealerBengkel.Transaksi.Penjualan;
//using E_DealerBengkel.Transaksi.Retur;
//using E_DealerBengkel.Transaksi.Services;
using System.Configuration;

namespace E_DealerBengkel.Transaksi.Pembelian
{
    public partial class Pembelian : Form
    {
        //---SERVER UMUM---
        string connectionString =
          "integrated security=true; data source=localhost;initial catalog=VroomDG";

        string jenis, idTran, id, idKat;
        string sup, user, idKendaraan, hargaBeli;
        double total = 0;
        int y;
        string Hargabeli;

        CultureInfo culture = new CultureInfo("id-ID");

        private void BtnPembelian_Click(object sender, EventArgs e)
        {
            Pembelian beli = new Pembelian();
            beli.Show();
            this.Hide();
        }

        private void BtnPenjualan_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnRetur_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnServices_Click(object sender, EventArgs e)
        {

        }


        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Kasir_Transaksi kasir = new Kasir_Transaksi();
            kasir.Show();
            this.Hide();
        }

        Timer timer = new Timer();

        public Pembelian()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(timer_Tick);
            //1000 = 1 detik
            timer.Interval = (1000) * (1);
            timer.Enabled = true;
            timer.Start();
        }

        private void cbJenisBarang_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbJenisBarang.Text == "Mobil")
            {
                jenis = "tMobil";
            }
            else if (cbJenisBarang.Text == "Motor")
            {
                jenis = "tMotor";
            }
            else if (cbJenisBarang.Text == "Sukucadang")
            {
                jenis = "tSukucadang";
            }

            try
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());

                sup = cbSupplier.SelectedValue.ToString();

                SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM " + jenis + " WHERE id_supplier='" + sup + "'", connection);
                DataTable dt = new DataTable();

                connection.Open();
                adapt.Fill(dt);

                //DataColumn col = dt.Columns.Add("Check", typeof(bool));
                //col.SetOrdinal(0);
                //foreach (DataRow r in dt.Rows)
                //{
                //    r["Check"] = 0;
                //}

                dgvStok.DataSource = dt;
                dgvStok.Columns[0].HeaderText = "ID";
                dgvStok.Columns[1].HeaderText = "Merek";
                dgvStok.Columns[2].HeaderText = "Tipe/Warna";
                dgvStok.Columns[3].HeaderText = "Jenis";
                dgvStok.Columns[4].HeaderText = "Harga Beli";
                dgvStok.Columns[5].HeaderText = "Harga Jual";
                dgvStok.Columns[6].HeaderText = "Jumlah";
                dgvStok.Columns[7].HeaderText = "Id Supplier";
                dgvStok.Columns[8].HeaderText = "Status";

                foreach (DataGridViewColumn col in dgvStok.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
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
                dgvStok.Columns[5].DefaultCellStyle.Format = "Rp #,###";
                connection.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvStok_CellClick(object sender, DataGridViewCellEventArgs e)
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

                i = dgvKeranjang.Rows.Add();
                dgvKeranjang.Rows[i].Cells[0].Value = dgvStok.Rows[n].Cells[0].Value.ToString();
                dgvKeranjang.Rows[i].Cells[1].Value = dgvStok.Rows[n].Cells[1].Value.ToString();
                dgvKeranjang.Rows[i].Cells[2].Value = dgvStok.Rows[n].Cells[2].Value.ToString();
                dgvKeranjang.Rows[i].Cells[3].Value = dgvStok.Rows[n].Cells[3].Value.ToString();
                Hargabeli = dgvStok.Rows[n].Cells[4].Value.ToString();
                Hargabeli = Convert.ToDecimal(Hargabeli).ToString("Rp #,###", culture);
                dgvKeranjang.Rows[i].Cells[4].Value = Hargabeli;
                dgvKeranjang.Rows[i].Cells[5].Value = j;
                dgvKeranjang.Rows[i].Cells[6].Value = dgvStok.Rows[n].Cells[7].Value.ToString();

                cekBeli();
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
            TxtJumlahBayar.Text = total.ToString("#,###");
        }

        private void dgvKeranjang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BtnTambah.Enabled = true;
            BtnKurang.Enabled = true;
        }

        private void CariId(string user)
        {
            string query = "select * from tKaryawan where username='" + user + "'";

            SqlConnection connection = new SqlConnection(Program.koneksi());
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


        private void isiJenisBarang()
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

                    SqlConnection connection = new SqlConnection(Program.koneksi());
                    SqlDataAdapter adapt = new SqlDataAdapter(
                        "SELECT * FROM tKategoriBarangPenjualan WHERE id_jenisBarang='" + idKat + "'", connection);

                    DataSet ds = new DataSet();

                    connection.Open();
                    adapt.Fill(ds);
                    int hitung = ds.Tables[0].Rows.Count;
                    if (hitung == 1)
                    {
                        penambahanBarang(idKat, jum, i);
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
                    SqlConnection connection = new SqlConnection(Program.koneksi());

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
                        penambahanBarang(idKat, jum, i);
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
                    SqlConnection connection = new SqlConnection(Program.koneksi());

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
                        penambahanBarang(idKat, jum, i);
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
                    SqlConnection connection = new SqlConnection(Program.koneksi());

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
                        penambahanBarang(idKat, jum, i);
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


        private void penambahanBarang(string idKat, string jum, int i)
        {
            string kategori = idKat.Substring(0, 3);

            if (kategori == "MBL")
            {
                string query = "select * from tMobil where id_mobil='" + idKat + "'";

                SqlConnection connection = new SqlConnection(Program.koneksi());
                SqlCommand search = new SqlCommand(query, connection);

                connection.Open();
                search.Parameters.AddWithValue("@id_mobil", idKat.Trim());

                SqlDataReader reader = search.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string jumlah = Convert.ToString(reader["jumlah"]);
                    int hasil = int.Parse(jumlah) + int.Parse(jum);
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

                SqlConnection connection = new SqlConnection(Program.koneksi());
                SqlCommand search = new SqlCommand(query, connection);

                connection.Open();
                search.Parameters.AddWithValue("@id_motor", idKat.Trim());

                SqlDataReader reader = search.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string jumlah = Convert.ToString(reader["jumlah"]);
                    int hasil = int.Parse(jumlah) + int.Parse(jum);
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

                SqlConnection connection = new SqlConnection(Program.koneksi());
                SqlCommand search = new SqlCommand(query, connection);

                connection.Open();
                search.Parameters.AddWithValue("@id_sukucadang", idKat.Trim());

                SqlDataReader reader = search.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string jumlah = Convert.ToString(reader["jumlah"]);
                    int hasil = int.Parse(jumlah) + int.Parse(jum);
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

            SqlConnection connection = new SqlConnection(Program.koneksi());

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlParameter param = new SqlParameter();

            SqlCommand insert = new SqlCommand("[sp_UpdateMobil]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_mobil", idMobil);
            insert.Parameters.AddWithValue("merk_mobil", merk);
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
                //insert.ExecuteNonQuery();
                //MessageBox.Show(idMobil + " " + hasil, "Information",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message, "UpdateMobil");
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
            SqlConnection connection = new SqlConnection(Program.koneksi());

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlParameter param = new SqlParameter();

            SqlCommand insert = new SqlCommand("[sp_UpdateMotor]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_motor", idMotor);
            insert.Parameters.AddWithValue("merk_motor", merk);
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
                //MessageBox.Show(idMotor + " " + hasil, "Information",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message, "UpdateMotor");
            }
        }

        private void updateBarangSpare(int hasil, int i)
        {
            string idSukuCadang = dgvKeranjang.Rows[i].Cells[0].Value.ToString();
            string merk = dgvKeranjang.Rows[i].Cells[1].Value.ToString();
            string tipe = dgvKeranjang.Rows[i].Cells[2].Value.ToString();
            string jenis = dgvKeranjang.Rows[i].Cells[3].Value.ToString();
            string hargaJual = dgvKeranjang.Rows[i].Cells[4].Value.ToString();
            //hasil;
            string sup = dgvKeranjang.Rows[i].Cells[6].Value.ToString();
            CariSukuCadang(idSukuCadang);
            hargaJual = hargaJual.Replace(".", "");
            hargaJual = hargaJual.Replace("Rp ", "");
            SqlConnection connection = new SqlConnection(Program.koneksi());

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlParameter param = new SqlParameter();

            SqlCommand insert = new SqlCommand("[sp_UpdateSukuCadang]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_sukucadang", idSukuCadang);
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
                //MessageBox.Show(idSpare + " " + hasil, "Information",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update: " + ex.Message, "UpdateSukuCadang");
            }
        }

        private void CariMobil(string idMobil)
        {
            string query = "select * from tMobil where id_mobil='" + idMobil + "'";

            SqlConnection connection = new SqlConnection(Program.koneksi());
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
            string query = "select * from T_Motor where id_motor='" + idMotor + "'";

            SqlConnection connection = new SqlConnection(Program.koneksi());
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

        private void CariSukuCadang(string idSukuCadang)
        {
            string query = "select * from T_Sparepart where id_sparepart='" + idSukuCadang + "'";

            SqlConnection connection = new SqlConnection(Program.koneksi());
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

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            int jumla;

            int n = dgvKeranjang.CurrentCell.RowIndex;
            jumla = int.Parse(dgvKeranjang.Rows[n].Cells[5].Value.ToString());

            jumla = jumla + 1;

            dgvKeranjang.Rows[n].Cells[5].Value = jumla;
            cekBeli();
        }

        private void BtnKurang_Click(object sender, EventArgs e)
        {
            int jumla;

            int n = dgvKeranjang.CurrentCell.RowIndex;
            jumla = int.Parse(dgvKeranjang.Rows[n].Cells[5].Value.ToString());

            jumla = jumla - 1;

            if (jumla < 1)
            {
                dgvKeranjang.Rows.RemoveAt(n);

                btnTambah.Enabled = false;
                btnKurang.Enabled = false;
            }
            else
            {
                dgvKeranjang.Rows[n].Cells[5].Value = jumla;
            }
            cekBeli();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lbWaktu.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }


        private void Pembelian_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vroomDGDataSet.tSupplier' table. You can move, or remove it, as needed.
            this.tSupplierTableAdapter.Fill(this.vroomDGDataSet.tSupplier);

            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;

            cbSupplier.Text = " -- PILIH SUPPLIER --";
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
            cbSupplier.Text = " -- PILIH SUPPLIER --";
            cbJenisBarang.Text = " -- PILIH JENIS BARANG --";
            TxtJumlahBayar.Text = "";
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


    }
}
