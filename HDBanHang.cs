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
    public partial class frmHDBanHang : Form
    {

       public static SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable comdt1 = new DataTable();
        DataTable comdt2 = new DataTable();
        DataTable comdt3 = new DataTable();
        DataTable comdtNV = new DataTable();
        DataTable comdtKH = new DataTable();
        DataTable comdtThuoc = new DataTable();
        DataTable dtThuoc = new DataTable();


        string sql, constr,str;

        public frmHDBanHang()
        {
            InitializeComponent();
        }
        private void btnThoatHD_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HDBanHang_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "SELECT  HDBanCT.MaHD,DMThuoc.MaThuoc,TenThuoc,HDBanCT.SL, HDBanCT.DonGia, ThanhTien,MaNV,MaKH,NgayLap From HDBanCT,DMThuoc,HDBan where HDBanCT.MaThuoc=DMThuoc.MaThuoc  and HDBanCT.MaHD=HDBan.MaHD";
            da = new SqlDataAdapter(sql, conn); //Cau DL
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

            sql = "Select Distinct MaKH,TenKH from DMKH order by MaKH";
            da = new SqlDataAdapter(sql, conn);
            comdtKH.Clear();
            da.Fill(comdtKH);
            cboMaKH.DataSource = comdtKH;
            cboMaKH.DisplayMember = "MaKH"; //truyền gt hiện thị ở combobox value 
            cboMaKH.ValueMember = "MaKH";

            sql = "Select Distinct MaThuoc,TenThuoc,GiaBan from DMThuoc order by MaThuoc";
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


        private void hDBanBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        private void btnLapHD_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1]; //khi bấm cập nhật sẽ chuyển về dòng cuối cùng
            NapCT();
            txtMaHD.Focus(); //chuyển con trỏ nhấp nháy đến mã HD
            btnCapNhat.Enabled = true; //nút câp nhật sáng lên
            btnXoaHD.Enabled = false;
            resetvalue();

        }

        private void grdData_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
            NapThongTin();

        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "Delete from HDBanCT where MaHD='" + txtMaHD.Text + "' and MaThuoc='" + cboMaThuoc.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index); //xóa dòng hiện thời của ô lưới
                
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

            {
                //cập nhật thêm mới
                int sl,tong,Tongmoi,SLcon;
                if (txtMaHD.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaHD.Focus();
                    return;
                }
                if (txtNgayLap.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNgayLap.Focus();
                    return;
                }
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKH.Focus();
                    return;
                }
                sql = "Insert into HDBan(MaHD,NgayLap,TongTien,MaKH,MaNV) Values (N'" + txtMaHD.Text.Trim() + "'," +
                    "'" + txtNgayLap.Value + "'," + txtTongTien.Text + ",N'" + cboMaKH.Text.Trim() + "',N'" + cboMaNV.Text.Trim() + "' )";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery(); //câu lệnh cho insert
                //Lưu thông tin thuốc
                if (cboMaThuoc.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaThuoc.Focus();
                    return;
                }
                if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
                {
                    MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;
                }
                // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
                sl = int.Parse(GetFieldValues("Select SoLuong from DMThuoc where MaThuoc=N'" + cboMaThuoc.SelectedValue + "'"));
                if (Convert.ToDouble(txtSoLuong.Text) > sl)
                {
                    MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;
                }
                sql = "Insert into HDBanCT(MaHD,MaThuoc,SL,DonGia,ThanhTien) Values (N'" + txtMaHD.Text.Trim() + "'," +
                    "N'" + cboMaThuoc.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtThanhTien.Text + " )";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery(); //câu lệnh cho insert
                NapCT();
                // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
                SLcon = sl - int.Parse(txtSoLuong.Text);
                sql = "UPDATE DMThuoc SET SoLuong =" + SLcon + " WHERE MaThuoc= N'" + cboMaThuoc.SelectedValue + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery(); //câu lệnh cho insert
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = int.Parse(GetFieldValues("SELECT TongTien FROM HDBan WHERE MaHD = N'" + txtMaHD.Text + "'"));
                Tongmoi = tong + int.Parse(txtThanhTien.Text);
                sql = "UPDATE HDBan SET TongTien =" + Tongmoi + " WHERE MaHD = N'" + txtMaHD.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery(); //câu lệnh cho insert
                txtTongTien.Text = Tongmoi.ToString();
                ResetValuesHang();
                btnXoaHD.Enabled = true;
                btnLapHD.Enabled = true;
            }

        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn column in grdData.Columns)
            {
                sb.AppendFormat("CONVERT({0}, System.String) LIKE '%{1}%' OR ", column.Name, txtTimkiem.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            (grdData.DataSource as DataTable).DefaultView.RowFilter = sb.ToString();
        }

        private void NapCT()
        {
        
           int i = grdData.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            cboMaThuoc.Text = grdData.Rows[i].Cells["MaThuoc"].Value.ToString();
            txtMaHD.Text = grdData.Rows[i].Cells["MaHD"].Value.ToString();
            cboMaNV.Text = grdData.Rows[i].Cells["MaNV"].Value.ToString();
            cboMaKH.Text = grdData.Rows[i].Cells["MaKH"].Value.ToString();
            txtNgayLap.Text = grdData.Rows[i].Cells["NgayLap"].Value.ToString();
            txtTenThuoc.Text = grdData.Rows[i].Cells["TenThuoc"].Value.ToString();
            txtSoLuong.Text = grdData.Rows[i].Cells["SL"].Value.ToString();
            txtDonGia.Text = grdData.Rows[i].Cells["DonGia"].Value.ToString();
            txtThanhTien.Text = grdData.Rows[i].Cells["ThanhTien"].Value.ToString();

        }
        private void NapThongTin()
        {
            str= "SELECT TenNV,MaNV from DMNV Where MaNV = N'" + cboMaNV.Text + "'";
            da = new SqlDataAdapter(str, conn);
            comdt1.Clear();
            da.Fill(comdt1);
            cboTenNV.DataSource = comdt1;
            cboTenNV.DisplayMember = "TenNV"; //truyền gt hiện thị 
            cboTenNV.ValueMember = "MaNV";       

            sql= "SELECT MaKH,TenKH,DChi,SDT from DMKH Where MaKH = N'" + cboMaKH.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            comdt2.Clear();
            da.Fill(comdt2);
            cboTenKH.DataSource = comdt2;
            cboTenKH.DisplayMember = "TenKH"; //truyền gt hiện thị 
            cboTenKH.ValueMember = "MaKH";
            cboMaKH.DataSource = comdt2;
            cboMaKH.DisplayMember = "MaKH"; //truyền gt hiện thị 
            cboMaKH.ValueMember = "MaKH";
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", comdt2, "DChi", true);
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", comdt2, "SDT", true);

            sql = "SELECT TongTien from HDBan Where MaHD = N'" + txtMaHD.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            dt1.Clear();
            da.Fill(dt1);
            txtTongTien.DataBindings.Clear();
            txtTongTien.DataBindings.Add("Text", dt1, "TongTien", true);
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboTenNV.DataBindings.Clear();
            this.cboTenNV.DataBindings.Add("Text", comdtNV, "TenNV");

        }

        private void cboMaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTenThuoc.DataBindings.Clear();
            this.txtTenThuoc.DataBindings.Add("Text", comdtThuoc, "TenThuoc");
            this.txtDonGia.DataBindings.Clear();
            this.txtDonGia.DataBindings.Add("Text", comdtThuoc, "GiaBan");
        }

        private void txtTenThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboMaThuoc.DataBindings.Clear();
            this.cboMaThuoc.DataBindings.Add("Text", comdtThuoc, "MaThuoc");
            this.txtDonGia.DataBindings.Clear();
            this.txtDonGia.DataBindings.Add("Text", comdtThuoc, "GiaBan");
        }

        private void resetvalue()
        {
            txtMaHD.Text = "";
            txtNgayLap.Text = DateTime.Now.ToShortDateString();
            cboMaNV.Text = "";
            cboTenNV.Text = "";
            cboMaKH.Text = "";
            cboTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            cboMaThuoc.Text = "";
            txtTenThuoc.Text = "";
            txtSoLuong.Text = "0";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";
            txtTongTien.Text = "0";

        }
        private void ResetValuesHang()
        {
            cboMaThuoc.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
        }
        public void NapLai()
        {
            sql = "SELECT HDBan.MaHD, NgayLap,MaNV,TenNV,MaKH,TenKH,DChi,SDT,DMThuoc.MaThuoc,TenThuoc,SL,DonGia,ThanhTien,TongTien from HDBan,HDBanCT,DMThuoc" +
                "where HDBan.MaHD=HDBanCT.MaHD and DMThuoc.MaThuoc=HDBanCT.MaThuoc";
            da = new SqlDataAdapter(sql, conn); //Cau DL
            dt.Clear(); //xóa dl trc đi
            da.Fill(dt); //Do dl vao table
            grdData.DataSource = dt; //do DL vao o luoi
            grdData.Refresh();
        }
        public static bool IsDate(string date) //Kiemr tra 1 biến có ở dạng ngày tháng k
        {
            string[] elements = date.Split('/');
            if ((Convert.ToInt32(elements[0]) >= 1) && (Convert.ToInt32(elements[0]) <= 31) && (Convert.ToInt32(elements[1]) >= 1) && (Convert.ToInt32(elements[1]) <= 12) && (Convert.ToInt32(elements[2]) >= 1900))
                return true;
            else return false;
        }
        public static string ConvertDateTime(string date) //chuyển đổi chuỗi do ng nhập dạng dd/mm/yy thành mm/dd/yy để lưu vào CSDL
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);

            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg ;
            txtThanhTien.Text = tt.ToString();
        }


        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                cboTenKH.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenKH from DMKH where MaKH = N'" + cboMaKH.SelectedValue + "'";
            cboTenKH.Text = GetFieldValues(str);
            str = "Select DChi from DMKH where MaKH = N'" + cboMaKH.SelectedValue + "'";
            txtDiaChi.Text = GetFieldValues(str);
            str = "Select SDT from DMKH where MaKH= N'" + cboMaKH.SelectedValue + "'";
            txtSDT.Text = GetFieldValues(str);
        }

        private void cboMaHD_DropDown(object sender, EventArgs e)
        {
            sql = "Select Distinct MaHD From HDBanCT order by MaHD";
            da = new SqlDataAdapter(sql, conn);
            comdt3.Clear();
            da.Fill(comdt3);
            
        }

        private void cboMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Hàm kiểm tra khoá trùng
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
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
