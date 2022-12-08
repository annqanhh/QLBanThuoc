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
    public partial class frmDMNV : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string constr, sql;
        string tmaNV, ttenNV, tDchi, tEmail, tSDT, tMucluong, tChucvu, tTaikhoan, tMatkhau;
        int i, n;
        Boolean addnewflag = false;
        public frmDMNV()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            MessageBox.Show("Hãy sửa đổi trên ô lưới và Lưu lại kết quả!");
            grdDMNV.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdDMNV.CurrentCell = grdDMNV[0, grdDMNV.RowCount - 1];
            NapCTNV();
            txtMaNV.Focus();
            btnUpdate.Enabled = true;
            addnewflag = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng hiện thời?(Y/N)", "Xác nhận yêu cầu",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                sql = "Delete from dbo.DMNV where MaNV = '" + txtMaNV.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                grdDMNV.Rows.RemoveAt(grdDMNV.CurrentRow.Index);
            }
            NapCTNV();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DMNV_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select * from dbo.DMNV";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdDMNV.DataSource = dt;
            grdDMNV.Refresh();
            NapCTNV();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }

        private void grdDMNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCTNV();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewflag == true)
            {
                // cap nhat them moi
                sql = "insert into dbo.DMNV (MaNV, TenNV, ChucVu, Dchi, Luong, SDT, Email, TaiKhoan, MatKhau) values"
                    + "('" + txtMaNV.Text + "',N'" + txtTenNV.Text + "','" + txtChucvu.Text + "',N'" + txtDchi.Text + "',"+ txtLuong.Text + ",'" + txtSDT.Text + "','" + txtEmail.Text + "','" + txtTK.Text + "','" + txtMK.Text + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                addnewflag = false;
                btnUpdate.Enabled = false;
                NapLaiNV();

            }
            else
            {
                //cap nhat sua chua
                n = grdDMNV.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tmaNV = grdDMNV.Rows[i].Cells["MaNV"].Value.ToString();
                    ttenNV = grdDMNV.Rows[i].Cells["TenNV"].Value.ToString();
                    tChucvu = grdDMNV.Rows[i].Cells["ChucVu"].Value.ToString();
                    tDchi = grdDMNV.Rows[i].Cells["Dchi"].Value.ToString();
                    tMucluong = grdDMNV.Rows[i].Cells["Luong"].Value.ToString();
                    tSDT = grdDMNV.Rows[i].Cells["SDT"].Value.ToString();
                    tEmail = grdDMNV.Rows[i].Cells["Email"].Value.ToString();
                    tTaikhoan = grdDMNV.Rows[i].Cells["TaiKhoan"].Value.ToString();
                    tMatkhau = grdDMNV.Rows[i].Cells["MatKhau"].Value.ToString();


                    sql = "update dbo.DMNV set MaNV= '" + tmaNV + "',TenNV=N'" + ttenNV + "',ChucVu=N'"+ tChucvu +"',Dchi=N'" + tDchi +"',Luong=" + tMucluong + ",SDT ='"+ tSDT + "',Email ='" + tEmail + "',TaiKhoan ='" + tTaikhoan + "',MatKhau ='" + tMatkhau + "'where MaNV ='" + tmaNV + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                btnUpdate.Enabled = false;
                MessageBox.Show("Sửa đổi thành công!");
            }
        }

        public void NapCTNV()
        {
            i = grdDMNV.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            txtMaNV.Text = grdDMNV.Rows[i].Cells["MaNV"].Value.ToString();
            txtTenNV.Text = grdDMNV.Rows[i].Cells["TenNV"].Value.ToString();
            txtChucvu.Text = grdDMNV.Rows[i].Cells["ChucVu"].Value.ToString();
            txtLuong.Text = grdDMNV.Rows[i].Cells["Luong"].Value.ToString();
            txtSDT.Text = grdDMNV.Rows[i].Cells["SDT"].Value.ToString();
            txtDchi.Text = grdDMNV.Rows[i].Cells["Dchi"].Value.ToString();
            txtEmail.Text = grdDMNV.Rows[i].Cells["Email"].Value.ToString();
            txtTK.Text = grdDMNV.Rows[i].Cells["TaiKhoan"].Value.ToString();
            txtMK.Text = grdDMNV.Rows[i].Cells["MatKhau"].Value.ToString();

        }
        public void NapLaiNV()
        {
            sql = "select *  from dbo.DMNV order by MaNV";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdDMNV.DataSource = dt;
            grdDMNV.Refresh();
        }
    }
}
