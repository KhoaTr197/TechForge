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
  public partial class ReceiptManagePageGUI : ManagePage
  {
    private SanPhamBUS sanPhamBus { get; set; }
    private HoaDonBUS hoaDonBus { get; set; }
    private List<SanPhamDTO> dsSanPham { get; set; }
    private List<HoaDonDTO> dsHoaDon { get; set; }
    private RolePermissions permissions;
    public ReceiptManagePageGUI(string role)
    {
      InitializeComponent();

      // Initialize permissions
      permissions = RolePermissions.GetPermissions(role);

      InitializeBUS();
      GetData();
      LoadData();
      ModifyData();
      SetUpFeature();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;
    }
    private void SetUpFeature()
    {
      if (permissions.Role == "Cashier")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", dsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        });
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", dsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        });
      }
      else if (permissions.Role == "Manager")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", dsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        });
      }
    }
    sealed protected override void InitializeBUS()
    {
      sanPhamBus = new SanPhamBUS(this.connStr);
      hoaDonBus = new HoaDonBUS(this.connStr);
    }
    protected void GetData()
    {
      // Map data to DTOs
      dsSanPham = sanPhamBus.GetAllConnected();
      dsHoaDon = hoaDonBus.GetAllConnected();
    }
    protected override void LoadData()
    {
      dgvMainList.BindingData(dsHoaDon);
      var columnMappings = new Dictionary<string, (string, bool)>{
        { "MaHD", ("Mã Hóa Đơn", true) },
        { "MaHV", ("Mã Hội Viên", true) },
        { "HoTen", ("Họ Tên", true) },
        { "Sdt", ("Số Điện Thoại", true) },
        { "DiaChi", ("Địa Chỉ", true) },
        { "NvLapHD", ("Nhân Viên Lập", true) },
        { "TongTien", ("Tổng Tiền", true) },
        { "NgLapHD", ("Ngày Lập", true) },
      };
      dgvMainList.SetColumnNames(columnMappings);
    }
    protected void ModifyData()
    {
      this.SuspendLayout();

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "MaHV")
        {
          // Format the MaHV column (PENDING)
        }
        else if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TongTien")
        {
          decimal price = (decimal)e.Value;
          e.Value = price.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
          e.FormattingApplied = true;
        }
      }
    }
    // Handle cell click event
    protected void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        DataGridView dgvMainList = (DataGridView)sender;

        if (dgvMainList.SelectedRows.Count > 0)
        {
          DataGridViewRow selectedRow = dgvMainList.SelectedRows[0];
          HoaDonDTO hoaDon = dsHoaDon.Find(hd => hd.MaHD == (int)selectedRow.Cells[0].Value);
          hoaDonBus.GetDetailWithProducts(hoaDon);

          ReceiptDetailFormGUI DetailForm = new ReceiptDetailFormGUI(permissions, hoaDonBus, hoaDon);
          OverlayFormGUI overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

          DetailForm.Show(Form.ActiveForm);

          // Assign event handler for submits
          DetailForm.AddSubmit += DetailsForm_AddSubmit;
          DetailForm.EditSubmit += DetailsForm_EditSubmit;
          DetailForm.DeleteSubmit += DetailsForm_DeleteSubmit;
        }
      }
    }
    // Handle add submit event
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when new products are added
      //summaryCards.Update(new SummaryCard[]
      //{
      //  new SummaryCard("Tổng sản phẩm", dsSanPham.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
      //  new SummaryCard("Danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
      //  new SummaryCard("Sắp hết hàng", GetLowStockCount().ToString(), "warning_icon", Color.FromArgb(231, 76, 60)),
      //  new SummaryCard("Giá trị kho", GetTotalInventoryValue().ToString("N0") + " đ", "money_icon", Color.FromArgb(155, 89, 182))
      //});
    }
    // Handle edit submit event
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when products are edited
      //summaryCards.Update(new SummaryCard[]
      //{
      //  new SummaryCard("Tổng sản phẩm", dsSanPham.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
      //  new SummaryCard("Danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
      //  new SummaryCard("Sắp hết hàng", GetLowStockCount().ToString(), "warning_icon", Color.FromArgb(231, 76, 60)),
      //  new SummaryCard("Giá trị kho", GetTotalInventoryValue().ToString("N0") + " đ", "money_icon", Color.FromArgb(155, 89, 182))
      //});
    }
    // Handle delete submit event
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when products are edited
      //summaryCards.Update(new SummaryCard[]
      //{
      //  new SummaryCard("Tổng sản phẩm", dsSanPham.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
      //  new SummaryCard("Danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
      //  new SummaryCard("Sắp hết hàng", GetLowStockCount().ToString(), "warning_icon", Color.FromArgb(231, 76, 60)),
      //  new SummaryCard("Giá trị kho", GetTotalInventoryValue().ToString("N0") + " đ", "money_icon", Color.FromArgb(155, 89, 182))
      //});
    }
  }
}
