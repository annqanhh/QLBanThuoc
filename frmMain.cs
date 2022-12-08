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
    public partial class frmMain : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable datarpthh = new DataTable();
        string constr, sql;
        public frmMain()
        {
            InitializeComponent();
            customizeDesign();
        }

        public frmMain(string user)
        {
            InitializeComponent();

        }



        private void customizeDesign()
        {
            panelSubMenuHT.Visible = false;
            panelSubMenuDM.Visible = false;
            panelSubMenuHD.Visible = false;
            panelSubMenuBC.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelSubMenuHT.Visible == true)
                panelSubMenuHT.Visible = false;
            if (panelSubMenuDM.Visible == true)
                panelSubMenuDM.Visible = false;
            if (panelSubMenuHD.Visible == true)
                panelSubMenuHD.Visible = false;
            if (panelSubMenuBC.Visible == true)
                panelSubMenuBC.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;               
            }
            else
                subMenu.Visible = false;
        }
        
        private void btnHeThong_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuHT);
        }
        #region MenuSubHT
        private void btnQltk_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmQLND());
            //...
            hideSubMenu();
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            //...
            DialogResult dr = MessageBox.Show("Bạn thực sự muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
                frmLogin f = new frmLogin();
                f.Show();
            }
            else
                return;
            //...
            hideSubMenu();
        }
        #endregion

        private void btnDM_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuDM);
        }
        #region MenuSubDM
        private void btnDMKH_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmDMKH());
            //...
            hideSubMenu();
        }

        private void btnDMNCC_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmDMNCC());
            //...
            hideSubMenu();
        }

        private void btnDMThuoc_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmDMThuoc());
            //...
            hideSubMenu();
        }

        private void btnNhomThuoc_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmDMNThuoc());
            //...
            hideSubMenu();
        }

        private void btnDMNV_Click(object sender, EventArgs e)
        {
            openDetailForm(new frmDMNV());
        }
        #endregion

        private void btnHD_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuHD);
        }
        #region MenuSubHD
        private void btnHDB_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmHDBanHang());
            //...
            hideSubMenu();
        }

        private void btnHDN_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new frmPhieuNhap());
            //...
            hideSubMenu();
        }
        #endregion

        private void btnBC_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuBC);
        }
        #region MenuSubBC
        private void btnBCDT_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new showprtDT());
            //...
            hideSubMenu();
        }

        private void btnBCKH_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new showKH());
            //...
            hideSubMenu();
        }

        private void btnBCTHH_Click(object sender, EventArgs e)
        {
            //...
            Baocao.rptThuocHetHan rpthh = new Baocao.rptThuocHetHan();
            sql = " select MaNhom, MaThuoc,TenThuoc,Dvt, Ngaysx, HSD, SoLuong from DMThuoc where DMThuoc.HSD < GETDATE()";
            da = new SqlDataAdapter(sql, conn);
            datarpthh.Clear();
            da.Fill(datarpthh);
            rpthh.SetDataSource(datarpthh);
            PrBC.frmBCTHH rphh = new PrBC.frmBCTHH(rpthh);
            rphh.Show();
            //...
            hideSubMenu();
        }

        private void btnBCHN_Click(object sender, EventArgs e)
        {
            //...
            openDetailForm(new showHangNhap());
            //...
            hideSubMenu();
        }

        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {
            openDetailForm(new frmHDBanHang());
        }

        private void ctrlbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Form activeForm = null;
        private void openDetailForm( Form detailForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = detailForm;
            detailForm.TopLevel = false;
            detailForm.FormBorderStyle = FormBorderStyle.None;
            detailForm.Dock = DockStyle.Fill;
            panelDetail.Controls.Add(detailForm);
            panelDetail.Tag = detailForm;
            detailForm.BringToFront();
            detailForm.Show();
        }

        private void frmMain_Load_1(object sender, EventArgs e)
        {
            constr = "Data Source=ANN;Initial Catalog=DBQLNTHUOC;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ với người quản lý qua số: 0886685469", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        
    }
}
