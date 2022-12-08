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
    public partial class showHangNhap : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable datarpthn = new DataTable();
        DataTable comNhap = new DataTable();

        string constr, sql;
        public showHangNhap()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Baocao.rptDSHangNhap rpthn = new Baocao.rptDSHangNhap();
            sql = "select  dbo.HDNhap.NgayLap, dbo.HDNhap.MaNCC, dbo.HDNhapCT.MaThuoc, dbo.HDNhapCT.SoLuong, dbo.HDNhapCT.DonGia, dbo.HDNhapCT.ThanhTien, dbo.DMThuoc.Dvt, dbo.DMThuoc.TenThuoc from dbo.HDnhapCT, dbo.DMthuoc,dbo.HDNhap where HDNhapCT.MaPhieuNhap =HDNhap.MaPhieuNhap and DMThuoc.MaThuoc = HDNhapCT.MaThuoc and dbo.HDnhapCT.MaPhieuNhap = '" + cmbMPNhap.Text + "' order by HDNhapCT.MaThuoc";

            da = new SqlDataAdapter(sql, conn);
            datarpthn.Clear();
            da.Fill(datarpthn);
            rpthn.SetDataSource(datarpthn);
            rpthn.DataDefinition.FormulaFields["MaPhieuNhap"].Text = "'" + cmbMPNhap.Text + "'";
            PrBC.frmBCHN rphn = new PrBC.frmBCHN(rpthn);
            rphn.Show();
        }

        private void showHangNhap_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();

            sql = "Select distinct MaPhieunhap from dbo.HDNhap";
            da = new SqlDataAdapter(sql, conn);
            comNhap.Clear();
            da.Fill(comNhap);
            cmbMPNhap.DataSource = comNhap;
            cmbMPNhap.DisplayMember = "MaPhieuNhap";
            cmbMPNhap.ValueMember = "MaPhieuNhap";
        }
    }
}
