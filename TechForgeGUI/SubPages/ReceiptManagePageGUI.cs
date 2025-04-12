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
      permissions.ApplyToManagePage(this);

      InitializeBUS();
      GetData();
      LoadData();
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
        { "NvlapHD", ("Nhân Viên Lập", true) },
        { "TongTien", ("Tổng Tiền", true) },
        { "NgLapHD", ("Ngày Lập", true) },
      };
      dgvMainList.SetColumnNames(columnMappings);
    }
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TongTien")
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
        //DataGridView dgvMainList = (DataGridView)sender;

        //if (dgvMainList.SelectedRows.Count > 0)
        //{
        //  DataGridViewRow selectedRow = dgvMainList.SelectedRows[0];
        //  SanPhamDTO sanPham = dsSanPham.Find(sp => sp.MaSP == (int)selectedRow.Cells[0].Value);

        //  ProductDetailFormGUI detailsForm = new ProductDetailFormGUI(sanPham, dsDanhMuc, dsHangSanXuat, sanPhamBus, dsNhaCungCap);
        //  detailsForm.parentForm = this;

        //  permissions.ApplyToForm(detailsForm);

        //  detailsForm.Show(Form.ActiveForm);

        //  // Assign event handler for submits
        //  detailsForm.AddSubmit += DetailsForm_AddSubmit;
        //  detailsForm.EditSubmit += DetailsForm_EditSubmit;
        //  detailsForm.DeleteSubmit += DetailsForm_DeleteSubmit;
        //}
      }
    }
  }
}
