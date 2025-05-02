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
  public partial class SupplierDetailFormGUI : DetailFormGUI
  {
    private NhaCungCapDTO thongTinNcc { get; set; }
    private NhaCungCapBUS BUS { get; set; }
    private RolePermissions permissions { get; set; }
    private UserNotification notify;
    public SupplierDetailFormGUI(RolePermissions _permissions, NhaCungCapBUS _BUS, NhaCungCapDTO _thongTinNcc = null)
    {
      InitializeComponent();

      this.thongTinNcc = _thongTinNcc;

      if (thongTinNcc != null)
        Type = "Detail";
      else
        Type = "Add";

      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết nhà cung cấp";

      if (Type == "Add")
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        this.Load += SupplierDetailFormGUI_LoadAddForm;
      }
      else
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        this.Load += SupplierDetailFormGUI_LoadDetailForm;
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
        }
      }

      btnAdd.Click += btnAdd_Click;
      btnEdit.Click += btnEdit_Click;
    }
    private void SupplierDetailFormGUI_LoadAddForm(object sender, EventArgs e)
    {
      cboTrangThai.Items.AddRange(new string[] { "Hợp tác", "Ngừng hợp tác" });
      cboTrangThai.SelectedIndex = thongTinNcc.TrangThai ? 0 : 1;
    }
    private void SupplierDetailFormGUI_LoadDetailForm(object sender, EventArgs e)
    {
      txtMaNCC.Text = thongTinNcc.MaNCC.ToString();
      txtMaNCC.ReadOnly = true;

      txtTenNCC.Text = thongTinNcc.TenNCC.ToString();
      txtTenNDD.Text = thongTinNcc.Ndd.ToString();
      txtSdt.Text = thongTinNcc.Sdt.ToString();
      txtEmail.Text = thongTinNcc.Email.ToString();

      cboTrangThai.Items.AddRange(new string[] { "Hợp tác", "Ngừng hợp tác" });
      cboTrangThai.SelectedIndex = thongTinNcc.TrangThai ? 0 : 1;
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      NhaCungCapDTO newNcc = new NhaCungCapDTO
      {
       
      };
      if (BUS.Add(newNcc) != -1)
      {
        notify = new UserNotification("Them thanh cong");
        notify.Show();
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      NhaCungCapDTO updatedNcc = new NhaCungCapDTO
      {
        
      };

      if (updatedNcc.TrangThai)
        BUS.Active(thongTinNcc.MaNCC);
      else
        BUS.Deactive(thongTinNcc.MaNCC);


      if (BUS.Update(thongTinNcc, updatedNcc))
      {
        notify = new UserNotification("Cap nhat thanh cong");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
  }
}
