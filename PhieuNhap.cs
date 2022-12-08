using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLBanThuoc
{
    public partial class frmPhieuNhap : Form
    {
        public static SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable comdtNV = new DataTable();
        DataTable comdtNCC= new DataTable();
        DataTable comdtThuoc = new DataTable();
        DataTable comdt1 = new DataTable();
        DataTable comdt2 = new DataTable();
        DataTable comdt3 = new DataTable();
        string sql, constr, str;
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void PhieuNhap_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();

           

            sql = "Select HDNhapCT.MaPhieuNhap,HDNhapCT.MaThuoc,TenThuoc,DonGia,HDNhapCT.SoLuong,ThanhTien,MaNCC,MaNV,NgayLap from HDNhapCT,HDNhap,DMThuoc where HDNhap.MaPhieuNhap=HDNhapCT.MaPhieuNhap and HDNhapCT.MaThuoc=DMThuoc.MaThuoc";
            da = new SqlDataAdapter(sql, conn); //Cau DL
            dt.Clear();
            da.Fill(dt); //Do dl vao table
            grdData.DataSource = dt; //do DL vao o luoi
            grdData.Refresh();   
            NapCT();
            NapThongTin();

            sql = "Select Distinct MaNV,TenNV from DMNV order by MaNV";
            da = new SqlDataAdapter(sql, conn);
            comdtNV.Clear();
            da.Fill(comdtNV);
            cboMaNV.DataSource = comdtNV;
            cboMaNV.DisplayMember = "MaNV"; //truyền gt hiện thị ở combobox value 
            cboMaNV.ValueMember = "MaNV";
            cboTenNV.DataSource = comdtNV;
            cboTenNV.DisplayMember = "TenNV"; //truyền gt hiện thị ở combobox value 
            cboTenNV.ValueMember = "TenNV";

            sql = "Select Distinct MaNCC,TenNCC from DMNCC order by MaNCC";
            da = new SqlDataAdapter(sql, conn);
            comdtNCC.Clear();
            da.Fill(comdtNCC);
            cboMaNCC.DataSource = comdtNCC;
            cboMaNCC.DisplayMember = "MaNCC"; //truyền gt hiện thị ở combobox value 
            cboMaNCC.ValueMember = "MaNCC";
            cboTenNCC.DataSource = comdtNCC;
            cboTenNCC.DisplayMember = "TenNCC"; //truyền gt hiện thị ở combobox value 
            cboTenNCC.ValueMember = "MaNCC";

            sql = "Select Distinct MaThuoc,TenThuoc,GiaNhap from DMThuoc order by MaThuoc";
            da = new SqlDataAdapter(sql, conn);
            comdtThuoc.Clear();
            da.Fill(comdtThuoc);
            cboMaThuoc.DataSource = comdtThuoc;
            cboMaThuoc.DisplayMember = "MaThuoc"; //truyền gt hiện thị ở combobox value 
            cboMaThuoc.ValueMember = "MaThuoc";
            txtTenThuoc.DataSource = comdtThuoc;
            txtTenThuoc.DisplayMember = "TenThuoc"; //truyền gt hiện thị ở combobox value 
            txtTenThuoc.ValueMember = "MaThuoc";
        }
        private void NapCT()
        {
   
            int i = grdData.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            cboMaThuoc.Text = grdData.Rows[i].Cells["MaThuoc"].Value.ToString();
            txtTenThuoc.Text = grdData.Rows[i].Cells["TenThuoc"].Value.ToString();
            cboMaPhieuNhap.Text = grdData.Rows[i].Cells["MaPhieuNhap"].Value.ToString();
            cboMaNV.Text = grdData.Rows[i].Cells["MaNV"].Value.ToString();
            cboMaNCC.Text = grdData.Rows[i].Cells["MaNCC"].Value.ToString();        
            txtSoLuong.Text = grdData.Rows[i].Cells["SoLuong"].Value.ToString();
            txtDonGia.Text = grdData.Rows[i].Cells["DonGia"].Value.ToString();
            txtThanhTien.Text = grdData.Rows[i].Cells["ThanhTien"].Value.ToString();
            txtNgayLap.Text = grdData.Rows[i].Cells["NgayLap"].Value.ToString();
        }
        private void NapThongTin()
        {
            str = "SELECT TenNV,MaNV from DMNV Where MaNV = N'" + cboMaNV.Text + "'";
            da = new SqlDataAdapter(str, conn);
            comdt1.Clear();
            da.Fill(comdt1);
            cboTenNV.DataSource = comdt1;
            cboTenNV.DisplayMember = "TenNV"; //truyền gt hiển thị 
            cboTenNV.ValueMember = "MaNV";

            sql = "SELECT MaNCC,TenNCC,DChi,SDT,MST from DMNCC Where MaNCC = N'" + cboMaNCC.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            comdt2.Clear();
            da.Fill(comdt2);
            cboTenNCC.DataSource = comdt2;
            cboTenNCC.DisplayMember = "TenNCC"; //truyền gt hiện thị 
            cboTenNCC.ValueMember = "MaNCC";
            cboMaNCC.DataSource = comdt2;
            cboMaNCC.DisplayMember = "MaNCC"; //truyền gt hiện thị 
            cboMaNCC.ValueMember = "MaNCC";
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", comdt2, "DChi", true);
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", comdt2, "SDT", true);
            txtMaSoThue.DataBindings.Clear();
            txtMaSoThue.DataBindings.Add("Text", comdt2, "MST", true);

            sql = "SELECT TongTien from HDNhap Where MaPhieuNhap = N'" + cboMaPhieuNhap.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            comdt3.Clear();
            da.Fill(comdt3);
            txtTongTien.DataBindings.Clear();
            txtTongTien.DataBindings.Add("Text", comdt3, "TongTien", true);

        }
        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboMaNV.Text == "")
                cboTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenNV from DMNV where MaNV =N'" + cboMaNV.SelectedValue + "'";
            cboTenNV.Text = GetFieldValues(str);
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.Text == "")
            {
                cboTenNCC.Text = "";
                txtMaSoThue.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
            }
            //Khi chọn Mã NCC thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenNCC from DMNCC where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
            cboTenNCC.Text = GetFieldValues(str);
            str = "Select MST from DMNCC where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
            txtMaSoThue.Text = GetFieldValues(str);
            str = "Select DChi from DMNCC where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
            txtDiaChi.Text = GetFieldValues(str);
            str = "Select SDT from DMNCC where MaNCC= N'" + cboMaNCC.SelectedValue + "'";
            txtSDT.Text = GetFieldValues(str);
        }

        private void cboMaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaThuoc.Text == "")
            {
                txtTenThuoc.Text = "";
                txtDonGia.Text = "";
 
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenThuoc from DMThuoc where MaThuoc = N'" + cboMaThuoc.SelectedValue + "'";
            txtTenThuoc.Text = GetFieldValues(str);
            str = "Select GiaNhap from DMThuoc where MaThuoc = N'" + cboMaThuoc.SelectedValue + "'";
            txtDonGia.Text = GetFieldValues(str);
        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
            NapThongTin();
        }

        private void txtTenThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboMaThuoc.DataBindings.Clear();
            this.cboMaThuoc.DataBindings.Add("Text", comdtThuoc, "MaThuoc");
            this.txtDonGia.DataBindings.Clear();
            this.txtDonGia.DataBindings.Add("Text", comdtThuoc, "GiaNhap");
        }

        private void btnThoatPN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLapHD_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1]; //khi bấm cập nhật sẽ chuyển về dòng cuối cùng
            NapCT();
            cboMaPhieuNhap.Focus(); //chuyển con trỏ nhấp nháy đến mã HD
            btnCapNhat.Enabled = true; //nút câp nhật sáng lên
            btnXoaPN.Enabled = false;
            resetvalue();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void resetvalue()
        {
            cboMaPhieuNhap.Text = "";
            txtNgayLap.Text = DateTime.Now.ToShortDateString();
            cboMaNV.Text = "";
            cboTenNV.Text = "";
            cboMaNCC.Text = "";
            cboTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            cboMaThuoc.Text = "";
            txtTenThuoc.Text = "";
            txtSoLuong.Text = "0";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";
            txtTongTien.Text = "0";

        }

        //Phương thức lấy dl từ câu lệnh sql
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
    }
}
