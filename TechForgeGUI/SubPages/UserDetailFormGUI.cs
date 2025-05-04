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

namespace TechForgeGUI.SubPages
{
    public partial class UserDetailFormGUI : DetailFormGUI
    {
        private NguoiDungBUS BUS { get; set; }
        private TaiKhoanBUS taiKhoanBUS { get; set; }
        private NguoiDungDTO ThongTinNguoiDung { get; set; }
        private TaiKhoanDTO thongTinTaiKhoan { get; set; }
        private List<string> dsVaiTro { get; set; }
        private RolePermissions permissions { get; set; }
        private UserNotification notify;
        public UserDetailFormGUI(RolePermissions _permissions, NguoiDungBUS _BUS, TaiKhoanBUS _taiKhoanBUS, NguoiDungDTO _thongTinNguoiDung = null, TaiKhoanDTO _thongTinTaiKhoan = null)
        {
            InitializeComponent();

            this.BUS = _BUS;
            this.ThongTinNguoiDung = _thongTinNguoiDung;
            this.taiKhoanBUS = _taiKhoanBUS;
            this.thongTinTaiKhoan = _thongTinTaiKhoan;
            this.dsVaiTro = BUS.GetAllRoles();
            this.permissions = _permissions;

            if (ThongTinNguoiDung == null)
            {
                Type = "Add";
            }
            else if (ThongTinNguoiDung != null)
            {
                Type = "Detail";
            }
            else
            {
                return;
            }

            this.Text = "Chi tiết người dùng";

            this.btnDelete.Visible = false;
            this.btnDelete.Enabled = false;

            if (Type == "Add")
            {
                this.btnEdit.Visible = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Visible = false;
                this.btnDelete.Enabled = false;


                this.Load += UserDetailFormGUI_LoadAddForm;
            }
            else
            {
                this.btnAdd.Visible = false;
                this.btnAdd.Enabled = false;


                this.Load += UserDetailFormGUI_LoadDetailForm;
            }

            if (permissions.Role == "Cashier")
            {
                this.btnAdd.Visible = false;
                this.btnAdd.Enabled = false;
                this.btnEdit.Visible = false;
                this.btnEdit.Enabled = false;
            }
            else if (permissions.Role == "WarehouseStaff")
            {
                this.btnAdd.Visible = false;
                this.btnAdd.Enabled = false;
                this.btnEdit.Visible = false;
                this.btnEdit.Enabled = false;
            }
            else if (permissions.Role == "Manager")
            {
                if (Type == "Detail")
                {
                    this.btnAdd.Visible = false;
                    this.btnAdd.Enabled = false;
                }
                else
                {
                    this.btnAdd.Visible = true;
                    this.btnAdd.Enabled = true;
                    this.btnEdit.Visible = false;
                    this.btnEdit.Enabled = false;
                }
                this.btnEdit.Visible = true;
                this.btnEdit.Enabled = true;
            }

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;


        }

        private void CboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaND.Text = BUS.GetNextId(cboVaiTro.SelectedItem.ToString());
        }

        private void UserDetailFormGUI_LoadAddForm(object sender, EventArgs e)
        {
            foreach (var vaiTro in dsVaiTro)
            {
                cboVaiTro.Items.Add(vaiTro);
            }
            cboVaiTro.SelectedIndex = 0;
            txtMaND.Text = BUS.GetNextId(cboVaiTro.SelectedItem.ToString());
            txtMaND.ReadOnly = true;

            radNam.Checked = true;

            cboVaiTro.SelectedIndexChanged += CboVaiTro_SelectedIndexChanged;
        }
        private void UserDetailFormGUI_LoadDetailForm(object sender, EventArgs e)
        {
            txtMaND.Text = ThongTinNguoiDung.MaND.ToString();
            txtMaND.Enabled = false;

            txtHoTen.Text = ThongTinNguoiDung.HoTen.ToString();
            txtSdt.Text = ThongTinNguoiDung.Sdt.ToString();
            txtDchi.Text = ThongTinNguoiDung.Dchi.ToString();

            radNam.Checked = ThongTinNguoiDung.GioiTinh ? true : false;
            radNu.Checked = ThongTinNguoiDung.GioiTinh ? false : true;

            dtpNgaySinh.Value = ThongTinNguoiDung.NgSinh;
            dtpNgayVaoLam.Value = ThongTinNguoiDung.NgVaoLam;

            foreach (var vaiTro in dsVaiTro)
            {
                cboVaiTro.Items.Add(vaiTro);
            }
            cboVaiTro.SelectedItem = ThongTinNguoiDung.VaiTro;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            NguoiDungDTO newNguoiDung = new NguoiDungDTO
            {
                MaND = txtMaND.Text,
                HoTen = txtHoTen.Text.Trim(),
                Sdt = txtSdt.Text.Trim(),
                Dchi = txtDchi.Text.Trim(),
                GioiTinh = radNam.Checked,
                Cccd = txtCccd.Text.Trim(),
                VaiTro = cboVaiTro.SelectedItem.ToString(),
                NgSinh = dtpNgaySinh.Value,
                NgVaoLam = dtpNgayVaoLam.Value,
            };
            newNguoiDung.MaND = BUS.GetNextId(newNguoiDung.VaiTro);

            if (BUS.Add(newNguoiDung) != -1)
            {
                notify = new UserNotification("Thêm người dùng thành công");
                notify.Show();
                OnAddSubmit(new DetailFormAddSubmitEventArgs());
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (MessageBox.Show("Bạn có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            NguoiDungDTO updatedNguoiDung = new NguoiDungDTO
            {
                MaND = txtMaND.Text,
                HoTen = txtHoTen.Text.Trim(),
                Sdt = txtSdt.Text.Trim(),
                Dchi = txtDchi.Text.Trim(),
                GioiTinh = radNam.Checked,
                Cccd = txtCccd.Text.Trim(),
                VaiTro = cboVaiTro.SelectedItem.ToString(),
                NgSinh = dtpNgaySinh.Value,
                NgVaoLam = dtpNgayVaoLam.Value,
            };

            if (BUS.Update(ThongTinNguoiDung, updatedNguoiDung))
            {
                notify = new UserNotification("Cập nhật người dùng thành công");
                notify.Show();
                OnEditSubmit(new DetailFormEditSubmitEventArgs());
            }
        }

        private bool ValidateInput()
        {
            //validate input
            string hoten = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoten))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string sdt = txtSdt.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string pattern = @"^(03|05|07|08|09)\d{8}$";
            if (!Regex.IsMatch(sdt, pattern))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string ndd = txtDchi.Text.Trim();
            if (string.IsNullOrEmpty(ndd))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string cccd = txtCccd.Text.Trim();
            if (string.IsNullOrEmpty(ndd))
            {
                MessageBox.Show("Số căn cước không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            pattern = @"^\d{12}$";
            if(cccd.Length != 12 || !cccd.StartsWith("0") || !Regex.IsMatch(cccd, pattern))
            {
                MessageBox.Show("Số căn cước không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DateTime ngsinh = dtpNgaySinh.Value;
            if(DateTime.Now.Year - ngsinh.Year < 18)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DateTime ngvaolam = dtpNgayVaoLam.Value;

            if(ngvaolam > DateTime.Now)
            {
                MessageBox.Show("Ngày vào làm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            return true;


        }

    }
}
