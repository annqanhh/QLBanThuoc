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
    public partial class frmDMNThuoc : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string constr, sql ;
        string tmanhom, ttennhom;
        int i,n;
        Boolean addnewflag = false;

        public frmDMNThuoc()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GrdDataNhomThuoc.CurrentCell = GrdDataNhomThuoc[0, GrdDataNhomThuoc.RowCount - 1];
            NapCTNhomThuoc();
            txtNhomthuoc.Focus();
            btnUpdate.Enabled = true;
            addnewflag = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng hiện thời?(Y/N)", "Xác nhận yêu cầu",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                sql = "Delete from dbo.DMNhomthuoc where MaNhom = '" + txtNhomthuoc.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                GrdDataNhomThuoc.Rows.RemoveAt(GrdDataNhomThuoc.CurrentRow.Index);
            }
            NapCTNhomThuoc();

        }

        private void DanhmucNhomThuoc_Load(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select * from dbo.DMNhomThuoc";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            GrdDataNhomThuoc.DataSource = dt;
            GrdDataNhomThuoc.Refresh();
            NapCTNhomThuoc();

        }

        private void GrdDataNhomThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            NapCTNhomThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            MessageBox.Show("Hãy sửa đổi trên ô lưới và Lưu lại kết quả!");
            GrdDataNhomThuoc.Focus();
        }

        private void GrdDataNhomThuoc_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NapCTNhomThuoc();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewflag == true)
            {
                // cap nhat them moi
                sql = "insert into dbo.DMNhomthuoc (MaNhom, TenNhom) values"
                    + "('" + txtNhomthuoc.Text + "',N'" + txtTennhom.Text + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                addnewflag = false;
                btnUpdate.Enabled = false;
                NapLaiNhomthuoc();

            }
            else
            {
                //cap nhat sua chua
                n = GrdDataNhomThuoc.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tmanhom = GrdDataNhomThuoc.Rows[i].Cells["MaNhom"].Value.ToString();
                    ttennhom = GrdDataNhomThuoc.Rows[i].Cells["TenNhom"].Value.ToString();
                    

                    sql = "update dbo.DMNhomthuoc set MaNhom= '" + tmanhom + "',TenNhom=N'" + ttennhom + "'where MaNhom ='" + tmanhom + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                btnUpdate.Enabled = false;
                MessageBox.Show("Sửa đổi thành công!");
            }
        }

        public void NapCTNhomThuoc()
        {
            i = GrdDataNhomThuoc.CurrentRow.Index;//lấy số thứ tự dòng hiện thời
            txtNhomthuoc.Text = GrdDataNhomThuoc.Rows[i].Cells["MaNhom"].Value.ToString();
            txtTennhom.Text = GrdDataNhomThuoc.Rows[i].Cells["TenNhom"].Value.ToString();
           
        }
        public void NapLaiNhomthuoc()
        {
            sql = "select *  from dbo.DMNhomthuoc order by Manhom";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            GrdDataNhomThuoc.DataSource = dt;
            GrdDataNhomThuoc.Refresh();
        }

    }
}
