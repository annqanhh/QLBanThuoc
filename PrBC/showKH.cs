using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBanThuoc
{
    public partial class showKH : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable datarpt = new DataTable();
        string constr, sql;
        public showKH()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrBC.rptThongkeKH rpt = new PrBC.rptThongkeKH();
            sql = "select DMKH.MaKH, DMKH.TenKH, DMKH.DChi,DMKH.SDT, HDBan.NgayLap, HDBan.TongTien from DMKH, HDBan where DMKH.MaKH = HDBan.MaKH and YEAR(NgayLap) ='" + cmbNamloc.Text + "'";
           
            da = new SqlDataAdapter(sql, conn);
            datarpt.Clear();
            da.Fill(datarpt);
            rpt.SetDataSource(datarpt);
            rpt.DataDefinition.FormulaFields["namloc"].Text = "'" + cmbNamloc.Text + "'";
            PrBC.frmBCKH rp = new PrBC.frmBCKH(rpt);
            rp.Show();
        }

        private void cmbNamloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = " select distinct YEAR(ngaylap) from dbo.HDBan ";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
        }

        private void showKH_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
        }
    }
}
