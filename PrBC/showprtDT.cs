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

namespace QLBanThuoc
{
    public partial class showprtDT : Form
    {
        DataTable datarptdt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string constr, sql;

        public showprtDT()
        {
            InitializeComponent();
        }

        private void showprtDT_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
        }

        private void cmbNamloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = " select distinct YEAR(ngaylap) from dbo.HDBan ";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Baocao.rptDoanhThu rptdt = new Baocao.rptDoanhThu();
            sql = " select MaHD, NgayLap, MaKH, MaNV,TongTien from dbo.HDBan where YEAR(NgayLap) ='" + cmbNamloc.Text + "'";

            da = new SqlDataAdapter(sql, conn);
            datarptdt.Clear();
            da.Fill(datarptdt);
            rptdt.SetDataSource(datarptdt);
            rptdt.DataDefinition.FormulaFields["namloc"].Text = "'" + cmbNamloc.Text + "'";

            PrBC.frmBCDT ptdt = new PrBC.frmBCDT(rptdt);

            ptdt.Show();
        }
    }
}
