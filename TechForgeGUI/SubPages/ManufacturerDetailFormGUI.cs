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
    private HangSanXuatDTO thongTinHangSanXuat { get; set; }
    private HangSanXuatBUS BUS { get; set; }
    private TableLayoutPanel pnlFields;
    private TextBox txtMaHSX;
    private TextBox txtTenHSX;
    private RolePermissions permissions { get; set; }
    public ManufacturerDetailFormGUI(RolePermissions _permissions , HangSanXuatBUS _BUS, HangSanXuatDTO _thongTinHangSanXuat = null)
    {
      InitializeComponent();

      this.thongTinHangSanXuat = _thongTinHangSanXuat;
      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết hãng sản xuất";
      this.Size = new Size(400, 200);

      // Create table layout panel
      pnlFields = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 2,
        Padding = new Padding(10),
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };

      // Add rows
      pnlFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
      pnlFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

      // Add columns
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

      // Add table layout to form
      this.Controls.Add(pnlFields);

      if (thongTinHangSanXuat == null)
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
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;
        this.btnEdit.Visible = true;
        this.btnEdit.Enabled = true;
        this.btnDelete.Visible = true;
        this.btnDelete.Enabled = true;
      }

      // Set up event handlers
      btnAdd.Click += BtnAdd_Click;
      btnEdit.Click += BtnEdit_Click;
      btnDelete.Click += BtnDelete_Click;
    }
    private void LoadAddForm()
    {
      // Create controls
      txtMaHSX = new TextBox
      {
        Text = BUS.GetNextId().ToString(),
        Dock = DockStyle.Fill,
        ReadOnly = true,
        Font = new Font(DefaultFontName, 12)
      };

      txtTenHSX = new TextBox
      {
        Text = "",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12)
      };

      // Add controls to table layout
      pnlFields.Controls.Add(new Label
      {
        Text = "Mã hãng:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12)
      }, 0, 0);
      pnlFields.Controls.Add(txtMaHSX, 1, 0);
      pnlFields.Controls.Add(new Label
      {
        Text = "Tên hãng:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
        Padding = new Padding(0, 4, 0, 0)
      }, 0, 1);
      pnlFields.Controls.Add(txtTenHSX, 1, 1);
    }
    private void LoadDetailForm()
    {
      // Create controls
      txtMaHSX = new TextBox
      {
        Text = thongTinHangSanXuat.MaHSX.ToString(),
        Dock = DockStyle.Fill,
        ReadOnly = true,
        Font = new Font(DefaultFontName, 12)
      };

      txtTenHSX = new TextBox
      {
        Text = thongTinHangSanXuat.TenHSX,
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12)
      };

      // Add controls to table layout
      pnlFields.Controls.Add(new Label
      {
        Text = "Mã hãng:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12)
      }, 0, 0);
      pnlFields.Controls.Add(txtMaHSX, 1, 0);
      pnlFields.Controls.Add(new Label
      {
        Text = "Tên hãng:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
        Padding = new Padding(0, 4, 0, 0)
      }, 0, 1);
      pnlFields.Controls.Add(txtTenHSX, 1, 1);
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
      thongTinHangSanXuat.TenHSX = tenHSX;

      // Update in database
      if (BUS.Update(thongTinHangSanXuat))
      {
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(thongTinHangSanXuat.MaHSX))
      {
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs());
      }
    }
  }
}
