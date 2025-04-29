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
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class ManufacturerDetailFormGUI : DetailFormGUI
  {
    private HangSanXuatDTO ThongTinHangSanXuat { get; set; }
    private HangSanXuatBUS BUS { get; set; }
    private RolePermissions permissions { get; set; }
    public ManufacturerDetailFormGUI(RolePermissions _permissions , HangSanXuatBUS _BUS, HangSanXuatDTO _thongTinHangSanXuat = null)
    {
      InitializeComponent();

      this.ThongTinHangSanXuat = _thongTinHangSanXuat;
      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết hãng sản xuất";
      this.Size = new Size(400, 200);

      if (ThongTinHangSanXuat != null)
        Type = "Detail";
      else
        Type = "Add";

      if (ThongTinHangSanXuat == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;

        LoadAddForm();
      }
      else
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;

        LoadDetailForm();
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
        if (Type == "Detail")
        {
          this.btnAdd.Visible = false;
          this.btnAdd.Enabled = false;
          this.btnEdit.Visible = true;
          this.btnEdit.Enabled = true;
          this.btnDelete.Visible = true;
          this.btnDelete.Enabled = true;
        }
        else
        {
          this.btnAdd.Visible = true;
          this.btnAdd.Enabled = true;
          this.btnEdit.Visible = false;
          this.btnEdit.Enabled = false;
          this.btnDelete.Visible = false;
          this.btnDelete.Enabled = false;
        }
      }

      // Set up event handlers
      btnAdd.Click += BtnAdd_Click;
      btnEdit.Click += BtnEdit_Click;
      btnDelete.Click += BtnDelete_Click;
    }
    private void LoadAddForm()
    {
      txtMaHSX.Text = BUS.GetNextId().ToString();
    }
    private void LoadDetailForm()
    {
      txtMaHSX.Text = ThongTinHangSanXuat.MaHSX.ToString();
      txtMaHSX.Enabled = false;

      txtTenHSX.Text = ThongTinHangSanXuat.TenHSX.ToString();
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      // Get values from form fields
      string tenHSX = txtTenHSX.Text;

      // Create new manufacturer
      HangSanXuatDTO newManufacturer = new HangSanXuatDTO
      {
        TenHSX = tenHSX
      };

      // Add to database
      if (BUS.Add(newManufacturer) > 0)
      {
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
      // Get values from form fields
      string tenHSX = txtTenHSX.Text;

      // Validate input
      if (string.IsNullOrWhiteSpace(tenHSX))
      {
        MessageBox.Show("Vui lòng nhập tên hãng sản xuất", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // Update manufacturer
      ThongTinHangSanXuat.TenHSX = tenHSX;

      // Update in database
      if (BUS.Update(ThongTinHangSanXuat))
      {
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(ThongTinHangSanXuat.MaHSX))
      {
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs());
      }
    }
  }
}
