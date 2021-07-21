using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_DealerBengkel.Reports
{
    public partial class Konfirmasi_Retur2 : Form
    {

        public string idR;
        public string tanggalR;
        public string ketR;
        public string idKar;
        public string idMbr;
        public string status;
        int i;

        public Konfirmasi_Retur2()
        {
            InitializeComponent();
        }

        public Konfirmasi_Retur2(string idR, string tanggalR, string ketR, string idKar, string idMbr, string status)
        {
            InitializeComponent();
            this.idR = idR;
            this.tanggalR = tanggalR;
            this.ketR = ketR;
            this.idKar = idKar;
            this.idMbr = idMbr;
            this.status = status;
            tampilData();
        }

        public void tampilData()
        {
            txtIDRetur.Enabled = false;
            txtTanggal.Enabled = false;
            txtKet.Enabled = false;
            txtKar.Enabled = false;
            txtMbr.Enabled = false;
            txtStatus.Enabled = false;

            txtIDRetur.Text = idR;
            txtTanggal.Text = tanggalR;
            txtKet.Text = ketR;
            txtKar.Text = idKar;
            txtMbr.Text = idMbr;
            txtStatus.Text = status;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Konfirmasi_Retur2_Load(object sender, EventArgs e)
        {
            tampilData();
        }

        private void btnSetuju_Click(object sender, EventArgs e)
        {
            var hasil = MessageBox.Show("Apakah anda yakin?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
            if (hasil == DialogResult.Yes)
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlParameter param = new SqlParameter();

                SqlCommand insert = new SqlCommand("[sp_UpdateRetur]", connection);
                insert.CommandType = CommandType.StoredProcedure;

                string setuju = "Disetujui";
                insert.Parameters.AddWithValue("status", setuju);
                insert.Parameters.AddWithValue("id_retur", txtIDRetur.Text);

                try
                {
                    //transaction.Commit();
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    i = 1;
                    Pengiriman_Konfirmasi Pk = new Pengiriman_Konfirmasi(i, idR);
                    Pk.Show();
                    this.Hide();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to update: " + ex.Message);
                }
            }
        }

        private void btnTolak_Click(object sender, EventArgs e)
        {
            var hasil = MessageBox.Show("Apakah anda yakin?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
            if (hasil == DialogResult.Yes)
            {
                SqlConnection connection = new SqlConnection(Program.koneksi());

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlParameter param = new SqlParameter();

                SqlCommand insert = new SqlCommand("[sp_UpdateRetur]", connection);
                insert.CommandType = CommandType.StoredProcedure;

                string ditolak = "Ditolak";
                insert.Parameters.AddWithValue("status", ditolak);
                insert.Parameters.AddWithValue("id_retur", txtIDRetur.Text);

                try
                {
                    //transaction.Commit();
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    i = 2;
                    Pengiriman_Konfirmasi Pk = new Pengiriman_Konfirmasi(i, idR);
                    Pk.Show();
                    this.Hide();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to update: " + ex.Message);
                }
            }
        }
    }
}
