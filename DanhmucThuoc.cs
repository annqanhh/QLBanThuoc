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
    public partial class frmDMThuoc : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt1 = new DataTable();
        DataTable comdt = new DataTable();
        string constr, sql;
        string tmaNhom, tmaThuoc, ttenThuoc, tHoattinh, tDvt, tNSX, tHSD, tgianhap, tgiaban, tsoluong;
        int i, n;
        Boolean addnewflag = false;
        public frmDMThuoc()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdThuoc.CurrentCell = grdThuoc[0, grdThuoc.RowCount - 1];
            NapCTThuoc();
            cmbNhomthuoc.Focus();
            btnUpdate.Enabled = true;
            addnewflag = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtDonvi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            sql = " select * from dbo.DMThuoc where MaNhom = '" + cmbNhomthuoc.Text + "' order by MaThuoc";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdThuoc.DataSource = dt;
            grdThuoc.Refresh();
            NapCTThuoc();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            sql = " select * from dbo.DMThuoc order by MaThuoc";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdThuoc.DataSource = dt;
            grdThuoc.Refresh();
            NapCTThuoc();
        }

        private void cmbNhomthuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void grdThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng hiện thời?(Y/N)", "Xác nhận yêu cầu",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                sql = "Delete from dbo.DMThuoc where MaThuoc = '" + txtMathuoc.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                grdThuoc.Rows.RemoveAt(grdThuoc.CurrentRow.Index);
            }
            NapCTThuoc();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DanhmucThuoc_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select * from dbo.DMThuoc";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdThuoc.DataSource = dt;
            grdThuoc.Refresh();
            NapCTThuoc();

            sql = "Select distinct MaNhom from dbo.DMThuoc";
            da = new SqlDataAdapter(sql, conn);
            comdt1.Clear();
            da.Fill(comdt1);
            cmbNhomthuoc.DataSource = comdt1;
            cmbNhomthuoc.DisplayMember = "manhom";
            cmbNhomthuoc.ValueMember = "manhom";

        }

        private void grdThuoc_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NapCTThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            MessageBox.Show("Hãy sửa đổi trên ô lưới và Lưu lại kết quả!");
            grdThuoc.Focus();
        }

        public void NapCTThuoc()
        {
             int i = grdThuoc.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            cmbNhomthuoc.Text = grdThuoc.Rows[i].Cells["MaNhom"].Value.ToString();
            txtMathuoc.Text = grdThuoc.Rows[i].Cells["MaThuoc"].Value.ToString();
            txtTenthuoc.Text = grdThuoc.Rows[i].Cells["TenThuoc"].Value.ToString();
            txtDonvi.Text = grdThuoc.Rows[i].Cells["Dvt"].Value.ToString();
            txthoattinh.Text = grdThuoc.Rows[i].Cells["HoatTinh"].Value.ToString();
            txtNSX.Text = grdThuoc.Rows[i].Cells["Ngaysx"].Value.ToString();
            txtHSD.Text = grdThuoc.Rows[i].Cells["HSD"].Value.ToString();
            txtGiaNhap.Text = grdThuoc.Rows[i].Cells["GiaNhap"].Value.ToString();
            txtGiaban.Text = grdThuoc.Rows[i].Cells["GiaBan"].Value.ToString();
            txtSoluong.Text = grdThuoc.Rows[i].Cells["SoLuong"].Value.ToString();
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewflag == true)
            {
                // cap nhat them moi
                sql = "insert into dbo.DMThuoc (Manhom, MaThuoc, TenThuoc, HoatTinh,  Dvt, Ngaysx, HSD, GiaNhap, GiaBan, SoLuong) values"
                    + "('" + cmbNhomthuoc.Text + "',N'" + txtMathuoc.Text + "',N'" + txtTenthuoc.Text + "',N'" + txthoattinh.Text + "',N'" 
                    + txtDonvi.Text + "','" + txtNSX.Text + "','" + txtHSD.Text + "'," + txtGiaNhap.Text + "," + txtGiaban.Text + "," + txtSoluong.Text + ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                addnewflag = false;
                btnUpdate.Enabled = false;
                NapLaiThuoc();

            }
            else
            {
                //cap nhat sua chua
                n = grdThuoc.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tmaNhom = grdThuoc.Rows[i].Cells["Manhom"].Value.ToString();
                    tmaThuoc = grdThuoc.Rows[i].Cells["MaThuoc"].Value.ToString();
                    ttenThuoc = grdThuoc.Rows[i].Cells["TenThuoc"].Value.ToString();
                    tHoattinh = grdThuoc.Rows[i].Cells["HoatTinh"].Value.ToString();
                    tDvt = grdThuoc.Rows[i].Cells["Dvt"].Value.ToString();
                    tNSX = grdThuoc.Rows[i].Cells["Ngaysx"].Value.ToString();
                    tHSD = grdThuoc.Rows[i].Cells["HSD"].Value.ToString();
                    tgianhap = grdThuoc.Rows[i].Cells["GiaNhap"].Value.ToString();
                    tgiaban = grdThuoc.Rows[i].Cells["GiaBan"].Value.ToString();
                    tsoluong = grdThuoc.Rows[i].Cells["SoLuong"].Value.ToString();


                    sql = "update dbo.DMThuoc set MaNhom= '" + tmaNhom + "',TenThuoc=N'" + ttenThuoc + "',HoatTinh=N'" + tHoattinh + "',Dvt=N'" + tDvt + "',Ngaysx ='" + tNSX + "',HSD ='" + tHSD + "',GiaNhap ='" + tgianhap + "',GiaBan ='" + tgiaban + "',SoLuong ='" + tsoluong + "'where MaThuoc ='" + tmaThuoc + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                btnUpdate.Enabled = false;
                MessageBox.Show("Sửa đổi thành công!");
            }
        }
        public void NapLaiThuoc()
        {
            sql = "select *  from dbo.DMThuoc order by MaThuoc ";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdThuoc.DataSource = dt;
            grdThuoc.Refresh();
        }
    }

    
    
}
