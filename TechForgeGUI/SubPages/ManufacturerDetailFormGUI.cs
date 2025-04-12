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

    public ManufacturerDetailFormGUI(HangSanXuatDTO _thongTinHangSanXuat, HangSanXuatBUS _BUS)
    {
      InitializeComponent();

      this.thongTinHangSanXuat = _thongTinHangSanXuat;
      this.BUS = _BUS;
      this.Text = "Chi tiết hãng sản xuất";

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
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
      pnlFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

      // Create controls
      txtMaHSX = new TextBox
      {
        Text = thongTinHangSanXuat.MaHSX.ToString(),
        Dock = DockStyle.Fill,
        ReadOnly = true
      };

      txtTenHSX = new TextBox
      {
        Text = thongTinHangSanXuat.TenHSX,
        Dock = DockStyle.Fill,
      };

      // Add controls to table layout
      pnlFields.Controls.Add(new Label { Text = "Mã hãng:", Dock = DockStyle.Fill }, 0, 0);
      pnlFields.Controls.Add(txtMaHSX, 1, 0);
      pnlFields.Controls.Add(new Label { Text = "Tên hãng:", Dock = DockStyle.Fill }, 0, 1);
      pnlFields.Controls.Add(txtTenHSX, 1, 1);

      // Add table layout to form
      this.Controls.Add(pnlFields);

      // Set up event handlers
      btnAdd.Click += BtnAdd_Click;
      btnEdit.Click += BtnEdit_Click;
      btnDelete.Click += BtnDelete_Click;
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
