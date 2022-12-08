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
    public partial class frmDMKH : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string constr, sql;
        string  tmaKH, ttenKH, tSDT, tDiachi, tEmail;
        int i,n ;
        Boolean addnewflag = false;
        public frmDMKH()
        {
            InitializeComponent();
        }

        private void txtMakhach_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmDMKH_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select *  from dbo.DMKH";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdDMKH.DataSource = dt;
            grdDMKH.Refresh();
            NapCTKH();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            grdDMKH.ClearSelection();
            grdDMKH.CurrentCell = grdDMKH[0, 0];
            NapCTKH();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(grdDMKH.CurrentRow.Index.ToString());
            if (i < grdDMKH.RowCount - 1)
            {
                grdDMKH.CurrentCell = grdDMKH[0, i + 1];
                i = Convert.ToInt16(grdDMKH.CurrentRow.Index.ToString());
            if (i < grdDMKH.RowCount - 1)
            {
                    grdDMKH.CurrentCell = grdDMKH[0, i + 1];
                NapCTKH();
                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            grdDMKH.CurrentCell = grdDMKH[0, grdDMKH.RowCount - 1];
            NapCTKH();
        }

        private void grdDMKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {

                NapCTKH();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            MessageBox.Show("Hãy sửa đổi trên ô lưới và Lưu lại kết quả!");
            grdDMKH.Focus();
        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDchi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(grdDMKH.CurrentRow.Index.ToString());
            if (i > 0)
            {
                grdDMKH.CurrentCell = grdDMKH[0, i - 1];
                NapCTKH();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdDMKH.CurrentCell = grdDMKH[0, grdDMKH.RowCount - 1];
            NapCTKH();
            txtMakhach.Focus();
            btnUpdate.Enabled = true;
            addnewflag = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng hiện thời?(Y/N)", "Xác nhận yêu cầu",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                sql = "Delete from dbo.DMKH where MaKH = '" + txtMakhach.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                grdDMKH.Rows.RemoveAt(grdDMKH.CurrentRow.Index);
            }
            NapCTKH();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewflag == true)
            {
                // cap nhat them moi
                sql = "insert into dbo.DMKH (MaKH, TenKH, DChi,  SDT, Email) values"
                    + "('" + txtMakhach.Text + "',N'" + txtTenKH.Text + "',N'" + txtDchi.Text + "'," + txtSDT.Text + ",'" + txtEmail.Text + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                addnewflag = false;
                btnUpdate.Enabled = false;
                NapLaiKH();

            }
            else
            {
                //cap nhat sua chua
                n = grdDMKH.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tmaKH = grdDMKH.Rows[i].Cells["MaKH"].Value.ToString();
                    ttenKH = grdDMKH.Rows[i].Cells["TenKH"].Value.ToString();
                    tDiachi = grdDMKH.Rows[i].Cells["DChi"].Value.ToString();
                    tSDT = grdDMKH.Rows[i].Cells["SDT"].Value.ToString();
                    tEmail = grdDMKH.Rows[i].Cells["Email"].Value.ToString();


                    sql = "update dbo.DMKH set MaKH= '" + tmaKH + "',TenKH=N'" + ttenKH + "',DChi=N'" + tDiachi + "',SDT ='" + tSDT + "',Email ='" + tEmail + "'where MaKH ='" + tmaKH + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                btnUpdate.Enabled = false;
                MessageBox.Show("Sửa đổi thành công!");
            }
        }

        public void NapCTKH()
        {
            i = grdDMKH.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            txtMakhach.Text = grdDMKH.Rows[i].Cells["MaKH"].Value.ToString();
            txtTenKH.Text = grdDMKH.Rows[i].Cells["Tenkh"].Value.ToString();
            txtSDT.Text = grdDMKH.Rows[i].Cells["SDT"].Value.ToString();
            txtDchi.Text = grdDMKH.Rows[i].Cells["DChi"].Value.ToString();
            txtEmail.Text = grdDMKH.Rows[i].Cells["Email"].Value.ToString();
            
        }
        public void NapLaiKH()
        {
            sql = "select *  from dbo.DMKH order by MaKH";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdDMKH.DataSource = dt;
            grdDMKH.Refresh();
        }
    }
}
