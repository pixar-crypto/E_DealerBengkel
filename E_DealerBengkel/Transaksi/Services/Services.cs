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
using System.Configuration;

namespace E_DealerBengkel.Transaksi.Services
{
    public partial class Services : Form
    {
        //---SERVER UMUM---
        string connectionString =
          "integrated security=true; data source=localhost;initial catalog=VroomDG";

        Timer timer = new Timer();
        string idSer, idTran, id, user;
        string harga, jenis;

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
            Retur.Retur retur = new Retur.Retur();
            retur.Show();
            this.Hide();
        }

        private void BtnServices_Click(object sender, EventArgs e)
        {
            Services services = new Services();
            services.Show();
            this.Hide();
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Kasir_Transaksi kasir = new Kasir_Transaksi();
            kasir.Show();
            this.Hide();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void Services_Load(object sender, EventArgs e)
        {
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
            RefreshDg();
        }

        double total = 0;

        public Services()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(timer_Tick);
            //1000 = 1 detik
            timer.Interval = (1000) * (1);
            timer.Enabled = true;
            timer.Start();

            TxtJumlahBayar.Enabled = false;
        }

        private void BtnCari_Click(object sender, EventArgs e)
        {
            string query = "select * from tMember where id_member='" + TxtIdCus.Text + "'";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_member", TxtIdCus.Text.Trim());

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                rbMobil.Enabled = true;
                rbMotor.Enabled = true;
                TxtUangBayar.Enabled = true;
                TxtNoPlat.Enabled = true;
                dgvService.Enabled = true;

                reader.Read();
                TxtNamaCus.Text = Convert.ToString(reader["nama_member"]);
            }
            else
                MessageBox.Show("Data tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }

        private void dgvService_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            cekServis();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lbWaktu.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void TxtUangBayar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtUangBayar_Leave(object sender, EventArgs e)
        {
            try
            {
                TxtUangBayar.Text = Program.toRupiah(int.Parse(TxtUangBayar.Text));
            }
            catch (Exception ex)
            {

            }
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM tService WHERE status='Tersedia'", connection);
            DataTable dt = new DataTable();

            connection.Open();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("Check", typeof(bool));
            col.SetOrdinal(0);
            foreach (DataRow r in dt.Rows)
            {
                r["Check"] = 0;
            }

            dgvService.DataSource = dt;
            dgvService.Columns[1].HeaderText = "ID";
            dgvService.Columns[2].HeaderText = "Jenis";
            dgvService.Columns[3].HeaderText = "Harga";
            dgvService.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvService.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            DataGridViewColumn column = dgvService.Columns[2];
            column.Width = 180;
            dgvService.BorderStyle = BorderStyle.None;
            dgvService.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvService.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvService.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvService.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvService.BackgroundColor = Color.White;

            dgvService.EnableHeadersVisualStyles = false;
            dgvService.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvService.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvService.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvService.Columns[3].DefaultCellStyle.Format = "Rp #,###";
            connection.Close();
        }

        private void cekServis()
        {
            total = 0;
            for (int i = 0; i < dgvService.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvService.Rows[i].Cells[0].Value) == true)
                {
                    idSer = dgvService.Rows[i].Cells[1].Value.ToString();
                    total = total + double.Parse(dgvService.Rows[i].Cells[3].Value.ToString());
                }
            }
            TxtJumlahBayar.Text = total.ToString("#,###");
        }

        private void cekBayar()
        {
            total = 0;
            for (int i = 0; i < dgvService.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvService.Rows[i].Cells[0].Value) == true)
                {
                    idSer = dgvService.Rows[i].Cells[1].Value.ToString();
                }
            }
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            if (TxtNoPlat.Text == "" || TxtUangBayar.Text == "" || TxtJumlahBayar.Text == "" && (rbMobil.Checked == false || rbMotor.Checked == false))
            {
                MessageBox.Show("Data masih ada yang kosong", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (double.Parse(TxtJumlahBayar.Text) > double.Parse(TxtUangBayar.Text))
            {
                MessageBox.Show("Uang anda kurang", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var hasil = MessageBox.Show("Apakah anda yakin?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
                if (hasil == DialogResult.Yes)
                {
                    isiTServis();
                    isiDetailSer();
                    MessageBox.Show("Data transaksi telah disimpan", "Pemberitahuan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
            }
        }

        private void TxtUangBayar_TextChanged(object sender, EventArgs e)
        {
            if (TxtUangBayar.Text == "")
            {
                return;
            }
            else
            {
                TxtUangBayar.Text = string.Format("{0:n0}", double.Parse(TxtUangBayar.Text));
                TxtUangBayar.SelectionStart = TxtUangBayar.Text.Length;
            }

            try
            {
                double kembali = double.Parse(TxtUangBayar.Text) - double.Parse(TxtJumlahBayar.Text);
                TxtUangKembali.Text = kembali.ToString("#,###");
            }
            catch (Exception ex)
            {

            }
        }

        private void Clear()
        {
            TxtIdCus.Text = "";
            TxtNamaCus.Text = "";
            TxtNoPlat.Text = "";
            TxtJumlahBayar.Text = "";
            TxtUangBayar.Text = "";
            TxtUangKembali.Text = "";

            rbMobil.Enabled = false;
            rbMotor.Enabled = false;
            TxtUangBayar.Enabled = false;
            TxtNoPlat.Enabled = false;
            dgvService.Enabled = false;
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

        private void isiTServis()
        {
            //idTran = Program.autogenerateID("TSR-", "sp_IdTrsService");
            IdOtomatis a = new IdOtomatis();
            string sp = "sp_IdTrsService";
            a.setID("TSR-", sp);
            idTran = a.getID();

            user = lbUser.Text.Replace("Hallo, kasir ", "");
            CariId(user);
            string waktu = DateTime.Now.ToString("yyyy-MM-dd");

            if (rbMobil.Checked == true)
            {
                jenis = "Mobil";
            }
            else
            {
                jenis = "Motor";
            }

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            connection.Open();

            SqlCommand insert = new SqlCommand("[sp_InputTServis]", connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("id_transaksi", idTran);
            insert.Parameters.AddWithValue("tanggal", waktu);
            insert.Parameters.AddWithValue("total_harga", total);
            insert.Parameters.AddWithValue("jenis_barang", jenis);
            insert.Parameters.AddWithValue("no_plat", TxtNoPlat.Text);
            insert.Parameters.AddWithValue("id_karyawan", id);
            insert.Parameters.AddWithValue("id_member", TxtIdCus.Text);

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

        private void isiDetailSer()
        {
            for (int i = 0; i < dgvService.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvService.Rows[i].Cells[0].Value) == true)
                {
                    idSer = dgvService.Rows[i].Cells[1].Value.ToString();
                    harga = dgvService.Rows[i].Cells[3].Value.ToString();

                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                    connection.Open();

                    SqlCommand input = new SqlCommand("[sp_InputDetailTServis]", connection);
                    input.CommandType = CommandType.StoredProcedure;
                    harga = harga.Replace(".0000", "");

                    input.Parameters.AddWithValue("id_transaksi", idTran);
                    input.Parameters.AddWithValue("id_service", idSer);
                    input.Parameters.AddWithValue("harga", double.Parse(harga));

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
        }
    }
}
