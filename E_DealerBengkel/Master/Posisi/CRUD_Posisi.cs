﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace E_DealerBengkel.Master.Posisi
{
    public partial class CRUD_Posisi : Form
    {
        String id;

        Timer timer = new Timer();

        public CRUD_Posisi()
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

        private void Clear()
        {
            TxtDeskripsi.Text = "";
            TxtGaji.Text = "";
            CbStatus.Text = "-- Pilih Status --";

            if (lbJudul.Text == "TAMBAH POSISI")
            {

            }
            else
            {
                TxtGaji.Enabled = false;
                CbStatus.Enabled = false;
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            Clear();
            lbJudul.Text = "TAMBAH POSISI";
            TxtGaji.Enabled = true;
            CbStatus.Enabled = true;
            lbledit.Visible = false;

            BtnSimpan.Text = "SIMPAN";
            BtnHapus.Visible = false;
            lbStatus.Visible = false;
            CbStatus.Visible = false;
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            Clear();
            lbJudul.Text = "UBAH POSISI";
            TxtGaji.Enabled = false;
            CbStatus.Enabled = false;
            lbledit.Visible = true;

            BtnSimpan.Text = "UBAH";
            BtnHapus.Visible = true;
            lbStatus.Visible = true;
            CbStatus.Visible = true;
            CbStatus.Text = "-- Pilih Status --";
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            Admin_Master Adm_M = new Admin_Master();
            Adm_M.Show();
            this.Hide();
        }

      

        private void CRUD_Posisi_Load(object sender, EventArgs e)
        {
            RefreshDg();
            lbUser.Text = lbUser.Text + Thread.CurrentPrincipal.Identity.Name;
            BtnHapus.Visible = false;
            lbledit.Visible = false;
        }



        public void RefreshDg()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            connection.Open();
            SqlCommand view = new SqlCommand("sp_dgvPosisi", connection);
            view.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapt = new SqlDataAdapter(view);

            DataTable dt = new DataTable();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvPosisi.DataSource = dt;
            dgvPosisi.Columns[1].HeaderText = "ID";
            dgvPosisi.Columns[2].HeaderText = "Jabatan";
            dgvPosisi.Columns[3].HeaderText = "Gaji";
            dgvPosisi.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvPosisi.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvPosisi.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvPosisi.Columns["Gaji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPosisi.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvPosisi.BorderStyle = BorderStyle.None;
            dgvPosisi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvPosisi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPosisi.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvPosisi.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvPosisi.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvPosisi.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvPosisi.EnableHeadersVisualStyles = false;
            dgvPosisi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPosisi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvPosisi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void TxtDeskripsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtGaji_KeyPress(object sender, KeyPressEventArgs e)
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
            if (TxtDeskripsi.Text == "" || TxtGaji.Text == "" || CbStatus.Text == "-- Pilih Status --")
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
                    SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                    Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlParameter param = new SqlParameter();

                    SqlCommand delete = new SqlCommand("[sp_DeletePosisi]", Connection);
                    delete.CommandType = CommandType.StoredProcedure;

                    delete.Parameters.AddWithValue("id_posisi", id);

                    try
                    {
                        delete.ExecuteNonQuery();
                        MessageBox.Show("Hapus data berhasil", "Information",
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
            if (lbJudul.Text == "TAMBAH POSISI")
            {
                if (TxtDeskripsi.Text == "" || TxtGaji.Text == "")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Warning!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var hasil = MessageBox.Show("Data akan diinput?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
                    if (hasil == DialogResult.Yes)
                    {
                        //String id = Program.autogenerateID("ROLE-", "sp_IdPosisi");
                        IdOtomatis a = new IdOtomatis();
                        string sp = "sp_IdPosisi";
                        a.setID("ROLE-", sp);
                        string id = a.getID();

                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlParameter param = new SqlParameter();

                        SqlCommand insert = new SqlCommand("[sp_InputPosisi]", connection);
                        insert.CommandType = CommandType.StoredProcedure;

                        insert.Parameters.AddWithValue("id_posisi", id);
                        insert.Parameters.AddWithValue("deskripsi", TxtDeskripsi.Text);
                        string harga = Program.toAngka(TxtGaji.Text).ToString();
                        insert.Parameters.AddWithValue("gaji", harga);
                        insert.Parameters.AddWithValue("status", "Aktif");

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
                    else
                    {

                    }
                }
            }
            else
            {
                if (TxtDeskripsi.Text == "" || TxtGaji.Text == "" || CbStatus.Text == "-- Pilih Status --")
                {
                    MessageBox.Show("Data ada yang kosong!!", "Information!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var hasil = MessageBox.Show("Data akan diupdate?", "Information",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
                    if (hasil == DialogResult.Yes)
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlParameter param = new SqlParameter();

                        SqlCommand insert = new SqlCommand("[sp_UpdatePosisi]", connection);
                        insert.CommandType = CommandType.StoredProcedure;

                        insert.Parameters.AddWithValue("id_posisi", id);
                        insert.Parameters.AddWithValue("deskripsi", TxtDeskripsi.Text);
                        string harga = Program.toAngka(TxtGaji.Text).ToString();
                        insert.Parameters.AddWithValue("gaji", harga);
                        insert.Parameters.AddWithValue("status", CbStatus.Text);

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
                    else
                    {

                    }
                }
            }
        }

        private void TxtDeskripsi_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvPosisi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lbJudul.Text == "TAMBAH POSISI")
            {

            }
            else
            {
                try
                {
                    CultureInfo culture = new CultureInfo("id-ID");

                    TxtGaji.Enabled = true;
                    CbStatus.Enabled = true;

                    DataGridViewRow row = this.dgvPosisi.Rows[e.RowIndex];
                    id = row.Cells[1].Value.ToString();
                    TxtDeskripsi.Text = row.Cells[2].Value.ToString();
                    String harga = row.Cells[3].Value.ToString();
                    CbStatus.Text = row.Cells[4].Value.ToString();
                    harga = Convert.ToDecimal(harga).ToString("c", culture);
                    TxtGaji.Text = harga.Replace("Rp", "");
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
            SqlCommand view = new SqlCommand("sp_PosisiAktif", connection);
            view.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapt = new SqlDataAdapter(view);
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvPosisi.DataSource = dt;
            dgvPosisi.Columns[1].HeaderText = "ID";
            dgvPosisi.Columns[2].HeaderText = "Jabatan";
            dgvPosisi.Columns[3].HeaderText = "Gaji";
            dgvPosisi.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvPosisi.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvPosisi.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvPosisi.Columns["Gaji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPosisi.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvPosisi.BorderStyle = BorderStyle.None;
            dgvPosisi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvPosisi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPosisi.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvPosisi.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvPosisi.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvPosisi.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvPosisi.EnableHeadersVisualStyles = false;
            dgvPosisi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPosisi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvPosisi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbTidakAktif_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand view = new SqlCommand("sp_PosisiTidakAktif", connection);
            view.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapt = new SqlDataAdapter(view);
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            DataColumn col = dt.Columns.Add("No", typeof(System.Int32));
            col.SetOrdinal(0);
            int a = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["No"] = a;
                a++;
            }

            dgvPosisi.DataSource = dt;
            dgvPosisi.Columns[1].HeaderText = "ID";
            dgvPosisi.Columns[2].HeaderText = "Jabatan";
            dgvPosisi.Columns[3].HeaderText = "Gaji";
            dgvPosisi.Columns[4].HeaderText = "Status";

            foreach (DataGridViewColumn colm in dgvPosisi.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            this.dgvPosisi.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvPosisi.Columns["Gaji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPosisi.Columns[3].DefaultCellStyle.Format = "Rp #,###.00";
            connection.Close();

            dgvPosisi.BorderStyle = BorderStyle.None;
            dgvPosisi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvPosisi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPosisi.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvPosisi.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvPosisi.BackgroundColor = Color.White;

            foreach (DataGridViewColumn colm in dgvPosisi.Columns)
            {
                colm.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colm.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgvPosisi.EnableHeadersVisualStyles = false;
            dgvPosisi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPosisi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvPosisi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDg();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void TxtGaji_TextChanged(object sender, EventArgs e)
        {
            if (TxtGaji.Text == "")
            {
                return;
            }
            else
            {
                TxtGaji.Text = string.Format("{0:n0}", double.Parse(TxtGaji.Text));
                TxtGaji.SelectionStart = TxtGaji.Text.Length;
            }
        }

        private void TxtGaji_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
