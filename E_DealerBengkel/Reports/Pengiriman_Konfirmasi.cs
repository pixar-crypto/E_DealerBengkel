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
using System.Net;
using System.Net.Mail;

namespace E_DealerBengkel.Reports
{
    public partial class Pengiriman_Konfirmasi : Form
    {
        OpenFileDialog ofdAttachment;
        String filename = "";
        String id, idMbr, namaMbr, emailTerima;

        public Pengiriman_Konfirmasi()
        {
            InitializeComponent();

            txtRecipientEmail.Enabled = false;
            txtSenderEmail.Enabled = false;
            txtSenderPassword.Enabled = false;
            txtSubject.Enabled = false;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                ofdAttachment = new OpenFileDialog();
                ofdAttachment.Filter = "Images(.jpg,.png)|*.png;*.jpg;|Pdf Files|*.pdf";
                if (ofdAttachment.ShowDialog() == DialogResult.OK)
                {
                    filename = ofdAttachment.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                SmtpClient clientDetails = new SmtpClient();
                clientDetails.Port = 587;
                clientDetails.Host = "smtp.gmail.com";
                clientDetails.EnableSsl = cbxSSL.Checked;
                clientDetails.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential(txtSenderEmail.Text.Trim(), txtSenderPassword.Text.Trim());

                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress(txtSenderEmail.Text.Trim());
                mailDetails.To.Add(txtRecipientEmail.Text.Trim());

                mailDetails.Subject = txtSubject.Text.Trim();
                mailDetails.IsBodyHtml = cbxHtmlBody.Checked;
                mailDetails.Body = rtbBody.Text.Trim();

                //file attachment
                if (filename.Length > 0)
                {
                    Attachment attachment = new Attachment(filename);
                    mailDetails.Attachments.Add(attachment);
                }

                clientDetails.Send(mailDetails);
                MessageBox.Show("Konfirmasi Email berhasil terkirim...", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                filename = "";

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Pengiriman_Konfirmasi_Load(object sender, EventArgs e)
        {

        }

        private void cbxSSL_CheckedChanged(object sender, EventArgs e)
        {

        }

        public Pengiriman_Konfirmasi(int i, String id)
        {
            this.id = id;
            cariId(id);

            if (i == 1)
            {
                InitializeComponent();
                rtbBody.Text = "Kepada Yth. \n" + namaMbr + " \n Di tempat \n\n Dengan Hormat,\n" +
                    "Berdasarkan keputusan pihak VROOM DG, kami menyatakan SETUJU atas permintaan retur pembelian dengan id pembelian " + id + ". Maka dari itu perlu diperhatikan beberapa hal yang harus dilakukan yaitu : \n" +
                    "1. Pelanggan yang melakukan retur pembelian harus datang langsung ke PT. VROOM DG untuk mengurus retur pembelian lebih lanjut.\n" +
                    "2. Pelanggan wajib mengembalikan barang yang ingin di retur kepada pihak VROOM DG.\n" +
                    "Demikian pemberitahuan ini disampaikan, atas perhatian dan kerja samanya kami ucapkan terimakasih.";
                txtRecipientEmail.Text = emailTerima;
                txtSenderEmail.Text = "vroomdg@gmail.com";
                txtSenderPassword.Text = "vroomdg123";
                txtSubject.Text = "VROOM DG";
            }
            else if (i == 2)
            {
                InitializeComponent();
                rtbBody.Text = "Kepada Yth. \n" + namaMbr + " \n Di tempat \n\n Dengan Hormat,\n" +
                    "Berdasarkan keputusan pihak VROOM DG, kami menyatakan TIDAK SETUJU atas permintaan retur pembelian dengan id pembelian " + id + ". Maka dari itu perlu diperhatikan beberapa hal yang harus dilakukan yaitu : \n" +
                    "1. Pelanggan yang melakukan retur pembelian harus datang langsung ke PT. VROOM DG untuk mengurus retur pembelian lebih lanjut.\n" +
                    "2. Pelanggan wajib mengembalikan barang yang ingin di retur kepada pihak VROOM DG.\n" +
                    "Demikian pemberitahuan ini disampaikan, atas perhatian dan kerja samanya kami ucapkan terimakasih.";
                txtRecipientEmail.Text = emailTerima;
                txtSenderEmail.Text = "vroomdg@gmail.com";
                txtSenderPassword.Text = "vroomdg123";
                txtSubject.Text = "VROOM DG";
            }
        }

        private void cariId(String id)
        {
            string query = "select * from tReturPembelian where id_retur='" + id + "'";

            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_retur", id.Trim());

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                idMbr = Convert.ToString(reader["id_member"]);

                cariMbr(idMbr);
            }
            else
                MessageBox.Show("Data pembelian tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }

        private void cariMbr(String idMbr)
        {
            string query = "select * from tMember where id_member='" + idMbr + "'";

            SqlConnection connection = new SqlConnection(Program.koneksi());
            SqlCommand search = new SqlCommand(query, connection);

            connection.Open();
            search.Parameters.AddWithValue("@id_member", id.Trim());

            SqlDataReader reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                namaMbr = Convert.ToString(reader["nama_member"]);
                emailTerima = Convert.ToString(reader["email"]);
            }
            else
                MessageBox.Show("Data member tidak ditemukan ", "Pemberitahuan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();
        }
    }
}
