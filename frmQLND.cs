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
    public partial class frmQLND : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string constr, sql;
        string tmaNV, ttenNV, tEmail, tSDT, tChucvu, tTaikhoan, tMatkhau;
        int i, n;
        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            // cap nhat them moi
            n = grdQLND.RowCount - 1;
            for (i = 0; i < n; i++)
            {
                ttenNV = grdQLND.Rows[i].Cells[0].Value?.ToString();
                tmaNV = grdQLND.Rows[i].Cells[1].Value?.ToString();
                tChucvu = grdQLND.Rows[i].Cells[2].Value?.ToString();
                tSDT = grdQLND.Rows[i].Cells[3].Value?.ToString();
                tEmail = grdQLND.Rows[i].Cells[4].Value?.ToString();
                tTaikhoan = grdQLND.Rows[i].Cells[5].Value?.ToString();
                tMatkhau = grdQLND.Rows[i].Cells[6].Value?.ToString();

                sql = "update dbo.DMNV set TenNV=N'" + ttenNV + "',MaNV= '" + tmaNV + "',ChucVu=N'" + tChucvu + "',SDT ='" + tSDT + "',Email ='" + tEmail + "',TaiKhoan ='" + tTaikhoan + "',MatKhau ='" + tMatkhau + "' where MaNV ='" + tmaNV + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            MessageBox.Show("Sửa đổi thành công!");
        }

        
       

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            MessageBox.Show("Hãy sửa đổi trên ô lưới và Lưu lại kết quả!");
            grdQLND.Focus();
        }

        public frmQLND()
        {
            InitializeComponent();
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQLND_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select TenNV, MaNV, ChucVu, SDT, Email, TaiKhoan, MatKhau from dbo.DMNV";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdQLND.DataSource = dt;
            grdQLND.Refresh();
            NapCTND();

        }
        public void NapCTND()
        {
            i = grdQLND.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            
            txtTenNV.Text = grdQLND.Rows[i].Cells[0].Value?.ToString();            
            txtMaNV.Text = grdQLND.Rows[i].Cells[1].Value?.ToString();
            cmbChucvu.Text = grdQLND.Rows[i].Cells[2].Value?.ToString();
            txtSDT.Text = grdQLND.Rows[i].Cells[3].Value?.ToString();
            txtEmail.Text = grdQLND.Rows[i].Cells[4].Value?.ToString();
            txtTK.Text = grdQLND.Rows[i].Cells[5].Value?.ToString();
            txtMK.Text = grdQLND.Rows[i].Cells[6].Value?.ToString();
        }
    }
}
