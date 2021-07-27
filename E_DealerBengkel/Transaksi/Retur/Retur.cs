using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Transaksi.Retur
{
    public partial class Retur : Form
    {
        int i;

        Timer timer = new Timer();
        String Id_JenisBarang = "";

        public Retur()
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

        private void BtnPembelian_Click(object sender, EventArgs e)
        {
            Pembelian.Pembelian beli = new Pembelian.Pembelian();
            beli.Show();
            this.Hide();
        }

        private void BtnPenjualan_Click(object sender, EventArgs e)
        {
            Penjualan.Penjualan jual = new Penjualan.Penjualan();
            jual.Show();
            this.Hide();
        }

        private void BtnRetur_Click(object sender, EventArgs e)
        {
            Retur retur = new Retur();
            retur.Show();
            this.Hide();
        }

        private void BtnServices_Click(object sender, EventArgs e)
        {
            Services.Services ser = new Services.Services();
            ser.Show();
            this.Hide();
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Kasir_Transaksi kasir = new Kasir_Transaksi();
            kasir.Show();
            this.Hide();
        }

        private void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tPenjualan", connection);
            DataTable dt = new DataTable();

            connection.Open();
            adapt.Fill(dt);

            //DataColumn col = dt.Columns.Add("Check", typeof(bool));
            //col.SetOrdinal(0);
            //foreach (DataRow r in dt.Rows)
            //{
            //    r["Check"] = 0;
            //}

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Tanggal";
            dataGridView1.Columns[2].HeaderText = "Total Harga";
            dataGridView1.Columns[3].HeaderText = "Id Jenis Barang";
            dataGridView1.Columns[4].HeaderText = "Id Karyawan";
            dataGridView1.Columns[5].HeaderText = "Id Member";

            foreach (DataGridViewColumn colm in dataGridView1.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns[2].DefaultCellStyle.Format = "Rp #,###";
            connection.Close();
        }

        private void tampilNamaMember()
        {
            try
            {
                string query1 = "SELECT * FROM tMember WHERE id_member = '" + txtIdCus.Text + "'";

                SqlConnection connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
                SqlCommand search1 = new SqlCommand(query1, connection1);

                connection1.Open();
                search1.Parameters.AddWithValue("@id_member", txtIdCus.Text.Trim());

                SqlDataReader read1 = search1.ExecuteReader();
                if (read1.Read())
                {
                    txtNamaCus.Text = Convert.ToString(read1["nama_member"]);
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan!", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                connection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Clear()
        {
            txtIdCus.Text = "";
            txtKaryawan.Text = "";
            txtKet.Text = "";
            txtTanggal.Text = "";
            txtTrans.Text = "";
            txtHarga.Text = "";
            txtNamaCus.Text = "";

            txtKet.Enabled = false;
        }

        private void btnCariIDtransaksi_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tPenjualan p, tDetailPenjualan d WHERE d.id_penjualan= '" + txtTrans.Text + "'"
                + "AND p.id_penjualan= '" + txtTrans.Text + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_penjualan", txtTrans.Text.Trim());

            SqlDataReader read = search.ExecuteReader();
            if (read.Read())
            {
                string tanggal = Convert.ToString(read["tanggal"]);
                DateTime Date = DateTime.Parse(tanggal);
                txtTanggal.Text = Date.ToString("yyyy-MM-dd");
                txtHarga.Text = Convert.ToString(read["total_harga"]);
                txtKaryawan.Text = Convert.ToString(read["id_karyawan"]);
                txtIdCus.Text = Convert.ToString(read["id_member"]);
                Id_JenisBarang = Convert.ToString(read["id_jenisBarang"]);

                txtKet.Enabled = true;
            }

            else
            {
                MessageBox.Show("Data tidak ditemukan!", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tampilNamaMember();
            connection.Close();
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

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            cekRetur();
            if (txtKet.Text == "")
            {
                MessageBox.Show("Data ada yang kosong!!", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (i == 1)
            {
                MessageBox.Show("Data sudah ada!", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                String id, status;
                //id = Program.autogenerateID("RTR-", "sp_IdRetur");
                IdOtomatis a = new IdOtomatis();
                string sp = "sp_IdRetur";
                a.setID("RTR-", sp);
                id = a.getID();

                status = "Menunggu";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlParameter param = new SqlParameter();

                SqlCommand insert = new SqlCommand("sp_InputRetur", connection);
                insert.CommandType = CommandType.StoredProcedure;

                insert.Parameters.AddWithValue("id_retur", id);
                insert.Parameters.AddWithValue("tanggal_retur", txtTglRetur.Text);
                insert.Parameters.AddWithValue("keterangan", txtKet.Text);
                insert.Parameters.AddWithValue("id_karyawan", txtKaryawan.Text);
                insert.Parameters.AddWithValue("id_member", txtIdCus.Text);
                insert.Parameters.AddWithValue("status", status);
                insert.Parameters.AddWithValue("id_penjualan", txtTrans.Text);
                insert.Parameters.AddWithValue("id_jenisBarang", Id_JenisBarang);


                try
                {
                    var hasil = MessageBox.Show("Apakah anda yakin?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
                    if (hasil == DialogResult.Yes)
                    {
                        insert.ExecuteNonQuery();
                        MessageBox.Show("Data retur berhasil disimpan", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to update: " + ex.Message);
                }
            }
        }

        private void cekRetur()
        {
            string query = "SELECT * FROM tReturPembelian WHERE id_penjualan= '" + txtTrans.Text + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_penjualan", txtTrans.Text.Trim());

            SqlDataReader read = search.ExecuteReader();
            if (read.Read())
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
        }

        private void btnCariIdCus_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tMember WHERE id_member = '" + txtIdCus.Text + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_member", txtIdCus.Text.Trim());

            SqlDataReader read = search.ExecuteReader();
            if (read.Read())
            {
                txtNamaCus.Text = Convert.ToString(read["nama_member"]);
            }
            else
            {
                MessageBox.Show("Data tidak ditemukan!", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            connection.Close();
        }

        private void Retur_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
            RefreshDg();
            txtTglRetur.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
