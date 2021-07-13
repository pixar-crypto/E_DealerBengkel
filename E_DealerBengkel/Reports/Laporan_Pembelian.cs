using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_DealerBengkel.Reports
{
    public partial class Laporan_Pembelian : Form
    {
        public Laporan_Pembelian()
        {
            InitializeComponent();
        }

        private void Laporan_Pembelian_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
