using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanThuoc.PrBC
{
    public partial class frmBCDT : Form
    {
        public frmBCDT(object rptDoanhthu)
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = rptDoanhthu;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
