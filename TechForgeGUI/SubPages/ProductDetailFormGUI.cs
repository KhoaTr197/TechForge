using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseControls;
using TechForgeGUI.BaseForms;
using TechForgeGUI.Utils;

namespace TechForgeGUI.SubPages
{
  public partial class ProductDetailFormGUI : DetailFormGUI
  {
    private SanPhamDTO ThongTinSanPham { get; set; }
    private List<DanhMucDTO> DsDanhMuc { get; set; }
    private List<HangSanXuatDTO> DsHangSanXuat { get; set; }
    private List<NhaCungCapDTO> DsNhaCungCap { get; set; }
    private SanPhamBUS BUS { get; set; }
    private Notification notify;
    private RolePermissions permissions { get; set; }
    public ProductDetailFormGUI(RolePermissions _permissions, SanPhamBUS _BUS, SanPhamDTO _thongTinSanPham=null, List<DanhMucDTO> _dsDanhMuc = null, List<HangSanXuatDTO> _dsHangSanXuat = null, List<NhaCungCapDTO> _dsNhaCungCap = null)
    {
      InitializeComponent();

      this.ThongTinSanPham = _thongTinSanPham;
      this.DsDanhMuc = _dsDanhMuc;
      this.DsHangSanXuat = _dsHangSanXuat;
      this.DsNhaCungCap = _dsNhaCungCap;
      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết sản phẩm";

      btnUploadImg.Click += btnUploadImg_Click;

      if (ThongTinSanPham == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        this.Load += ProductDetailFormGUI_LoadAddForm;
      }
      else
      {
        this.Size = new Size(1000, 400);

        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;


        this.Load += ProductDetailFormGUI_LoadDetailForm;
      }

      if (permissions.Role == "Cashier")
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;
        this.btnEdit.Visible = true;
        this.btnEdit.Enabled = true;
        this.btnDelete.Visible = true;
        this.btnDelete.Enabled = true;
      }
      else if (permissions.Role == "Manager")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;
        this.btnEdit.Visible = true;
        this.btnEdit.Enabled = true;
        this.btnDelete.Visible = true;
        this.btnDelete.Enabled = true;
      }

      btnAdd.Click += btnAdd_Click;
      btnEdit.Click += btnEdit_Click;
    }

    private void ProductDetailFormGUI_LoadAddForm(object sender, EventArgs e)
    {
      cboDanhMuc.DataSource = DsDanhMuc;
      cboDanhMuc.DisplayMember = "TenDM";
      cboDanhMuc.ValueMember = "MaDM";

      cboHangSanXuat.DataSource = DsHangSanXuat;
      cboHangSanXuat.DisplayMember = "TenHSX";
      cboHangSanXuat.ValueMember = "MaHSX";

      cboNhaCungCap.DataSource = DsNhaCungCap;
      cboNhaCungCap.DisplayMember = "TenNCC";
      cboNhaCungCap.ValueMember = "MaNCC";

      cboTrangThai.Items.AddRange(new string[] { "Đang kinh doanh", "Ngừng kinh doanh" });
      cboTrangThai.SelectedIndex = ThongTinSanPham.TrangThai ? 0 : 1;
    }
    private void ProductDetailFormGUI_LoadDetailForm(object sender, EventArgs e)
    {
      txtMaSP.Text = ThongTinSanPham.MaSP.ToString();
      txtMaSP.Enabled = false;

      txtTenSP.Text = ThongTinSanPham.TenSP.ToString();
      txtMoTa.Text = ThongTinSanPham.MoTa.ToString();
      nudGiaNhap.Value = ThongTinSanPham.GiaNhap;
      nudGia.Value = ThongTinSanPham.Gia;
      nudKhuyenMai.Value = ThongTinSanPham.KhuyenMai;
      nudSoLuong.Value = ThongTinSanPham.SoLuong;
      txtDonViTinh.Text = ThongTinSanPham.DonViTinh.ToString();

      cboDanhMuc.DataSource = DsDanhMuc;
      cboDanhMuc.DisplayMember = "TenDM";
      cboDanhMuc.ValueMember = "MaDM";

      cboHangSanXuat.DataSource = DsHangSanXuat;
      cboHangSanXuat.DisplayMember = "TenHSX";
      cboHangSanXuat.ValueMember = "MaHSX";

      cboNhaCungCap.DataSource = DsNhaCungCap;
      cboNhaCungCap.DisplayMember = "TenNCC";
      cboNhaCungCap.ValueMember = "MaNCC";

      cboTrangThai.Items.AddRange(new string[] { "Đang kinh doanh", "Ngừng kinh doanh" });
      cboTrangThai.SelectedIndex = ThongTinSanPham.TrangThai ? 0 : 1;
    }
    private void btnUploadImg_Click(object sender, EventArgs e)
    {
      if (picHinh != null)
      {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
          openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
          openFileDialog.Title = "Chọn một hình ảnh";
          openFileDialog.Multiselect = false;

          if (openFileDialog.ShowDialog() == DialogResult.OK)
          {
            try
            {
              picHinh.Image = Image.FromFile(openFileDialog.FileName);
              picHinh.Tag = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
              MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message);
            }
          }
        }
      }
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      SanPhamDTO newSanPham = new SanPhamDTO()
      {
      };

      if (BUS.Add(newSanPham) != -1)
      {
        notify = new Notification("Thêm thành công");
        notify.Show();
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      SanPhamDTO updatedSanPham = new SanPhamDTO()
      {
      };

      if (BUS.Update(ThongTinSanPham, updatedSanPham))
      {
        notify = new Notification("Cập nhật thành công");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(ThongTinSanPham.MaSP))
      {
        notify = new Notification("Xóa thành công");
        notify.Show();
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs());
      }
    }
  }
}
