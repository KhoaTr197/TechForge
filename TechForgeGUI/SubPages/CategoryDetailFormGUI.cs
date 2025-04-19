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
    private DanhMucDTO thongTinDanhMuc { get; set; }
    private DanhMucBUS BUS { get; set; }
    private TableLayoutPanel pnlFields;
    private TextBox txtMaDM;
    private TextBox txtTenDM;

    public CategoryDetailFormGUI(DanhMucBUS _BUS, DanhMucDTO _thongTinDanhMuc = null)
    {
      InitializeComponent();

      this.thongTinDanhMuc = _thongTinDanhMuc;
      this.BUS = _BUS;
      this.Text = "Chi tiết danh mục";

      // Create table layout panel
      pnlFields = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 2,
        Padding = new Padding(10),
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };

      // Add table layout to form
      this.Controls.Add(pnlFields);

      if(thongTinDanhMuc == null)
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

      // Set up event handlers
      btnAdd.Click += BtnAdd_Click;
      btnEdit.Click += BtnEdit_Click;
      btnDelete.Click += BtnDelete_Click;
    }
    private void LoadAddForm()
    { 
      // Add rows
      pnlFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
      pnlFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

      // Add columns
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

      // Create controls
      txtMaDM = new TextBox
      {
        Text = BUS.GetNextId().ToString(),
        Dock = DockStyle.Fill,
        ReadOnly = true,
        Font = new Font(DefaultFontName, 12),
      };

      txtTenDM = new TextBox
      {
        Text = "",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
      };

      // Add controls to table layout
      pnlFields.Controls.Add(new Label
      {
        Text = "Mã danh mục:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
      }, 0, 0);
      pnlFields.Controls.Add(txtMaDM, 1, 0);
      pnlFields.Controls.Add(new Label
      {
        Text = "Tên danh mục:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
        Padding = new Padding(0, 4, 0, 0)
      }, 0, 1);
      pnlFields.Controls.Add(txtTenDM, 1, 1);
    }
    private void LoadDetailForm()
    {
      // Add rows
      pnlFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
      pnlFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

      // Add columns
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

      // Create controls
      txtMaDM = new TextBox
      {
        Text = thongTinDanhMuc.MaDM.ToString(),
        Dock = DockStyle.Fill,
        ReadOnly = true,
        Font = new Font(DefaultFontName, 12),
      };

      txtTenDM = new TextBox
      {
        Text = thongTinDanhMuc.TenDM,
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
      };

      // Add controls to table layout
      pnlFields.Controls.Add(new Label
      {
        Text = "Mã danh mục:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
      }, 0, 0);
      pnlFields.Controls.Add(txtMaDM, 1, 0);
      pnlFields.Controls.Add(new Label
      {
        Text = "Tên danh mục:",
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
        Padding = new Padding(0, 4, 0, 0)
      }, 0, 1);
      pnlFields.Controls.Add(txtTenDM, 1, 1);
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

      if (BUS.Update(thongTinDanhMuc))
      {
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(thongTinDanhMuc.MaDM))
      {
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs());
      }
    }
  }
}
