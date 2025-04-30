using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
    private Notification notify;
    public UserDetailFormGUI(RolePermissions _permissions, NguoiDungBUS _BUS, TaiKhoanBUS _taiKhoanBUS, NguoiDungDTO _thongTinNguoiDung = null, TaiKhoanDTO _thongTinTaiKhoan = null)
    {
      InitializeComponent();

      this.BUS = _BUS;
      this.ThongTinNguoiDung = _thongTinNguoiDung;
      this.taiKhoanBUS = _taiKhoanBUS;
      this.thongTinTaiKhoan = _thongTinTaiKhoan;
      this.dsVaiTro = BUS.GetAllRoles();
      this.permissions = _permissions;

      if (ThongTinNguoiDung == null && thongTinTaiKhoan == null)
      {
        Type = "Add";
      }
      else if (ThongTinNguoiDung != null && thongTinTaiKhoan != null)
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
    private void UserDetailFormGUI_LoadAddForm(object sender, EventArgs e)
    {
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
      NguoiDungDTO newNguoiDung = new NguoiDungDTO {
      };
      newNguoiDung.MaND = BUS.GetNextId(newNguoiDung.VaiTro);
      TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
      {
      };

      if (String.IsNullOrWhiteSpace(newTaiKhoan.TenTK) || String.IsNullOrWhiteSpace(newTaiKhoan.MatKhau))
      {
        MessageBox.Show("Tài khoản, mật khẩu khônng được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      if (BUS.Add(newNguoiDung) != -1 && taiKhoanBUS.Add(newTaiKhoan))
      {
        notify = new Notification("Thêm người dùng thành công");
        notify.Show();
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Bạn có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        return;

      NguoiDungDTO updatedNguoiDung = new NguoiDungDTO
      {
      };
      TaiKhoanDTO updatedTaiKhoan = new TaiKhoanDTO
      {
      };

      bool result;

      if (updatedTaiKhoan.TrangThai)
        result = taiKhoanBUS.Active(thongTinTaiKhoan.MaND);
      else
        result = taiKhoanBUS.Deactive(thongTinTaiKhoan.MaND);


      if (result && BUS.Update(ThongTinNguoiDung, updatedNguoiDung) && taiKhoanBUS.Update(thongTinTaiKhoan, updatedTaiKhoan))
      {
        notify = new Notification("Cập nhật người dùng thành công");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
  }
}
