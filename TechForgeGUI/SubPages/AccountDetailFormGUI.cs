using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseControls;
using TechForgeGUI.BaseForms;
using static System.Net.Mime.MediaTypeNames;


namespace TechForgeGUI.SubPages
{
    public partial class AccountDetailFormGUI : DetailFormGUI
    {
        private TaiKhoanBUS taiKhoanBus {  get; set; }
        private NguoiDungBUS nguoiDungBus { get; set; }
        private TaiKhoanDTO thongTinTaiKhoan {  get; set; }

        private List<NguoiDungDTO> dsNDChuaCoTK {  get; set; }
        private UserNotification notify;

        public AccountDetailFormGUI(TaiKhoanBUS _taiKhoanBus, List<NguoiDungDTO> _dsNDChuaCoTK, TaiKhoanDTO _thongTinTaiKhoan = null)
        {
            InitializeComponent();
            this.taiKhoanBus = _taiKhoanBus;
            this.thongTinTaiKhoan = _thongTinTaiKhoan;
            this.dsNDChuaCoTK = _dsNDChuaCoTK;
            

            if (thongTinTaiKhoan == null)
            {
                this.Text = "Thêm tài khoản";
                this.btnEdit.Visible = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Visible = false;
                this.btnDelete.Enabled = false;
                txtTenTK.Enabled = true;
                cboMaND.Enabled = true;


                this.Load += AccountDetailFormGUI_LoadAddForm;
            }
            else
            {
                this.Text = "Chi tiết tài khoản";
                this.btnAdd.Visible = false;
                this.btnAdd.Enabled = false;
                txtTenTK.Enabled = false;
                cboMaND.Enabled = false;

                this.Load += AccountDetailFormGUI_LoadDetailForm;
            }

            this.btnAdd.Click += btnAdd_Click;
            this.btnEdit.Click += btnEdit_Click;
            this.btnDelete.Click += btnDelete_Click;
        }

        private void AccountDetailFormGUI_LoadAddForm(object sender, EventArgs e)
        {
            
            cboMaND.DataSource = dsNDChuaCoTK;
            cboMaND.ValueMember = "MaND";
            cboMaND.DisplayMember = "MaTenND";

            txtMatKhau.PasswordChar = '*';

            cboTrangThai.Items.AddRange(new string[] { "Kích hoạt", "Huỷ kích hoạt" });
            cboTrangThai.SelectedIndex = 0;
        }

        private void AccountDetailFormGUI_LoadDetailForm(object sender, EventArgs e)
        {
            cboMaND.DataSource = taiKhoanBus.GetAllConnected();
            cboMaND.ValueMember = "MaND";
            cboMaND.DisplayMember = "MaTenND";
            cboMaND.SelectedValue = thongTinTaiKhoan.MaND;
            txtTenTK.Text = thongTinTaiKhoan.TenTK;
            txtMatKhau.Text = thongTinTaiKhoan.MatKhau;
            txtMatKhau.PasswordChar = '\0';
            
            cboTrangThai.Items.AddRange(new string[] { "Kích hoạt", "Huỷ kích hoạt" });
            cboTrangThai.SelectedIndex = thongTinTaiKhoan.TrangThai ? 0 : 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var kiemTraTonTaiTK = taiKhoanBus.GetAllConnected().All(tk => tk.TenTK != txtTenTK.Text.Trim());
            if (!kiemTraTonTaiTK)
            {
                MessageBox.Show("Đã tồn tại tài khoản này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO()
            {
                MaND = cboMaND.SelectedValue.ToString(),
                TenTK = txtTenTK.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                TrangThai = cboTrangThai.SelectedIndex == 0,
            };

            if (taiKhoanBus.Add(newTaiKhoan))
            {
                notify = new UserNotification("Thêm tài khoản thành công");
                notify.Show();
                OnAddSubmit(new DetailFormAddSubmitEventArgs(this));
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            TaiKhoanDTO updatedTaiKhoan = new TaiKhoanDTO()
            {
                MaND = cboMaND.SelectedValue.ToString(),
                TenTK = txtTenTK.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                TrangThai = cboTrangThai.SelectedIndex == 0,
            };

            if (MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            if (taiKhoanBus.Update(thongTinTaiKhoan, updatedTaiKhoan))
            {
                notify = new UserNotification("Cập nhật tài khoản thành công");
                notify.Show();
                OnEditSubmit(new DetailFormEditSubmitEventArgs(this));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            if (taiKhoanBus.Deactive(thongTinTaiKhoan.MaND))
                OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs(this));
        }

        private bool ValidateInput()
        {
            //validate input
            string tentk = txtTenTK.Text.Trim();
            if (string.IsNullOrEmpty(tentk))
            {
                MessageBox.Show("Tên tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string mk = txtMatKhau.Text.Trim();
            if (string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("Mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;


        }
    }
}
