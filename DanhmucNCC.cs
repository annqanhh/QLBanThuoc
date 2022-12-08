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
    public partial class frmDMNCC : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string constr, sql;
        string tmaNCC, ttenNCC, tDchi, tEmail, tSDT, tMSThue;
        int i, n;
        Boolean addnewflag = false;
        public frmDMNCC()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            grdNCC.ClearSelection();
            grdNCC.CurrentCell = grdNCC[0, 0];
            NapCTNCC();
        }

        private void DanhmucNCC_Load(object sender, EventArgs e)
        {
          
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select MaNCC, TenNCC, MST, DChi,SDT, Email  from dbo.DMNCC";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdNCC.DataSource = dt;
            grdNCC.Refresh();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng hiện thời?(Y/N)", "Xác nhận yêu cầu",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                sql = "Delete from dbo.DMNCC where MaNCC = '" + txtMaNCC.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                grdNCC.Rows.RemoveAt(grdNCC.CurrentRow.Index);
            }
            NapCTNCC();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdNCC.CurrentCell = grdNCC[0, grdNCC.RowCount - 1];
            NapCTNCC();
            txtMaNCC.Focus();
            btnUpdate.Enabled = true;
            addnewflag = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            MessageBox.Show("Hãy sửa đổi trên ô lưới và Lưu lại kết quả!");
            grdNCC.Focus();
        }

        private void grdNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtMaNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void NapCTNCC()
        {
            i = grdNCC.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            txtMaNCC.Text = grdNCC.Rows[i].Cells["MaNCC"].Value.ToString();
            txtTenNCC.Text = grdNCC.Rows[i].Cells["TenNCC"].Value.ToString();
            txtMSThue.Text = grdNCC.Rows[i].Cells["MST"].Value.ToString();
            txtSDT.Text = grdNCC.Rows[i].Cells["SDT"].Value.ToString();
            txtDchi.Text = grdNCC.Rows[i].Cells["Dchi"].Value.ToString();
            txtEmail.Text = grdNCC.Rows[i].Cells["Email"].Value.ToString();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(grdNCC.CurrentRow.Index.ToString());
            if (i < grdNCC.RowCount - 1)
            {
                grdNCC.CurrentCell = grdNCC[0, i + 1];
                NapCTNCC();
            }


        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            grdNCC.CurrentCell = grdNCC[0, grdNCC.RowCount - 1];
            NapCTNCC();
        }

        private void grdNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCTNCC();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewflag == true)
            {
                // cap nhat them moi
                sql = "insert into dbo.DMNCC (MaNCC, TenNCC, Dchi, MST, SDT, Email) values"
                    + "('" + txtMaNCC.Text + "',N'" + txtTenNCC.Text + "',N'" + txtDchi.Text + "'," + txtMSThue.Text + "," + txtSDT.Text + ",'" + txtEmail.Text + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                addnewflag = false;
                btnUpdate.Enabled = false;
                NapLaiNCC();

            }
            else
            {
                //cap nhat sua chua
                n = grdNCC.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tmaNCC = grdNCC.Rows[i].Cells["MaNCC"].Value.ToString();
                    ttenNCC = grdNCC.Rows[i].Cells["TenNCC"].Value.ToString();
                    tDchi = grdNCC.Rows[i].Cells["Dchi"].Value.ToString();
                    tMSThue = grdNCC.Rows[i].Cells["MST"].Value.ToString();
                    tSDT = grdNCC.Rows[i].Cells["SDT"].Value.ToString();
                    tEmail = grdNCC.Rows[i].Cells["Email"].Value.ToString();


                    sql = "update dbo.DMNCC set MaNCC= '" + tmaNCC + "',TenNCC=N'" + ttenNCC + "',Dchi=N'" + tDchi + "',MST=" + tMSThue + ",SDT =" + tSDT + ",Email ='" + tEmail + "'where MaNCC ='" + tmaNCC + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                btnUpdate.Enabled = false;
                MessageBox.Show("Sửa đổi thành công!");
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(grdNCC.CurrentRow.Index.ToString());
            if (i > 0)
            {
                grdNCC.CurrentCell = grdNCC[0, i - 1];
                NapCTNCC();
            }
        }
        public void NapLaiNCC()
        {
            sql = "select *  from dbo.DMNCC order by MaNCC";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdNCC.DataSource = dt;
            grdNCC.Refresh();
        }
    }

       
    
}
