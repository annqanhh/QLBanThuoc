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
    public partial class frmLogin : Form
    {
        
        int dem;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void ControlBoxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Data
            
            SqlConnection conn = new SqlConnection("Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True");
            
                conn.Open();
                string tk = txtTK.Text;
                string mk = txtMK.Text;
                string sql = "select TaiKhoan, MatKhau from DMNV where TaiKhoan = '" + tk + "' and MatKhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dt = cmd.ExecuteReader();

                if (dt.Read() == true)
                {
                    frmMain f = new frmMain();
                    this.Hide();
                    txtTK.Clear();
                    txtMK.Clear();
                    f.ShowDialog();
                }
                else
                {
                    dem ++;
                    if (dem < 3)
                    {
                        lblThongbao.Visible = true;
                        lblThongbao.Text = "Tài khoản hoặc mật khẩu không chính xác! Bạn còn " + (4 - dem).ToString() + " lần đăng nhập.";
                        txtTK.Clear();
                        txtMK.Clear();
                        
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã nhập sai quá 3 lần! Chương trình sẽ thoát", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
                
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
