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
  public partial class CategoryDetailFormGUI : DetailFormGUI
  {
    private DanhMucDTO ThongTinDanhMuc { get; set; }
    private DanhMucBUS BUS { get; set; }
    private RolePermissions permissions { get; set; }
    public CategoryDetailFormGUI(RolePermissions _permissions, DanhMucBUS _BUS, DanhMucDTO _ThongTinDanhMuc = null)
    {
      InitializeComponent();

      this.ThongTinDanhMuc = _ThongTinDanhMuc;
      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết danh mục";

      if (ThongTinDanhMuc != null)
        Type = "Detail";
      else
        Type = "Add";

      if(ThongTinDanhMuc == null)
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
      txtMaDM.Text = BUS.GetNextId().ToString();
    }
    private void LoadDetailForm()
    {
      txtMaDM.Text = ThongTinDanhMuc.MaDM.ToString();
      txtMaDM.Enabled = false;

      txtTenDM.Text = ThongTinDanhMuc.TenDM.ToString();
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      // Get values from form fields
      string tenDM = txtTenDM.Text;

      // Create new category
      DanhMucDTO newCategory = new DanhMucDTO
      {
        TenDM = tenDM
      };

      if (BUS.Add(newCategory) != -1)
      {
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
      string tenDM = txtTenDM.Text;

      if (BUS.Update(ThongTinDanhMuc))
      {
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(ThongTinDanhMuc.MaDM))
      {
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs());
      }
    }
  }
}
