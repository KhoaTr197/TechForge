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
    private List<HoaDonDTO> DsHoaDon { get; set; }
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

      // Attach event handler for search button
      btnSearch.Click += btnSearch_Click;
    }
    private void SetUpFeature()
    {
      this.btnAdd.Enabled = false;
      this.btnAdd.Visible = false;

      if (permissions.Role == "Cashier")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", DsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        });
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", DsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Giá trị hóa đơn gần nhất", DsHoaDon.OrderByDescending(hd => hd.NgLapHD).FirstOrDefault().TongTien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), "money_icon", Color.FromArgb(155, 89, 182)),
        });
      }
      else if (permissions.Role == "Manager")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", DsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Giá trị hóa đơn gần nhất", DsHoaDon.OrderByDescending(hd => hd.NgLapHD).FirstOrDefault().TongTien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), "money_icon", Color.FromArgb(155, 89, 182)),
        });
      }
    }
    private void InitializeBUS()
    {
      sanPhamBus = new SanPhamBUS(this.connStr);
      hoaDonBus = new HoaDonBUS(this.connStr);
    }
    private void GetData()
    {
      // Map data to DTOs
      dsSanPham = sanPhamBus.GetAllConnected();
      DsHoaDon = hoaDonBus.GetAllConnected().OrderByDescending(hd => hd.NgLapHD).ToList();
    }
    private void LoadData()
    {
      dgvMainList.Binding(DsHoaDon);
    }
    protected void ModifyData()
    {
      this.SuspendLayout();

      // Set up DataGridView columns
      dgvMainList.dgvList.Columns["MaHD"].HeaderText = "Mã";
      dgvMainList.dgvList.Columns["MaHD"].DataPropertyName = "MaHD";
      dgvMainList.dgvList.Columns["MaHV"].HeaderText = "Mã HV";
      dgvMainList.dgvList.Columns["MaHV"].DataPropertyName = "MaHV";
      dgvMainList.dgvList.Columns["HoTen"].HeaderText = "Họ tên";
      dgvMainList.dgvList.Columns["HoTen"].DataPropertyName = "HoTen";
      dgvMainList.dgvList.Columns["DiaChi"].HeaderText = "Địa chỉ";
      dgvMainList.dgvList.Columns["DiaChi"].DataPropertyName = "DiaChi";
      dgvMainList.dgvList.Columns["Sdt"].HeaderText = "Số điện thoại";
      dgvMainList.dgvList.Columns["Sdt"].DataPropertyName = "Sdt";
      dgvMainList.dgvList.Columns["NvLapHD"].HeaderText = "Nhân viên lập";
      dgvMainList.dgvList.Columns["NvLapHD"].DataPropertyName = "NvLapHD";
      dgvMainList.dgvList.Columns["TongTien"].HeaderText = "Tổng tiền";
      dgvMainList.dgvList.Columns["TongTien"].DataPropertyName = "TongTien";
      dgvMainList.dgvList.Columns["NgLapHD"].HeaderText = "Ngày lập";
      dgvMainList.dgvList.Columns["NgLapHD"].DataPropertyName = "NgLapHD";

      //Attach event handler for cell formatting
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
          HoaDonDTO hoaDon = DsHoaDon.Find(hd => hd.MaHD == (int)selectedRow.Cells[0].Value);
          hoaDonBus.GetDetailWithReceipts(hoaDon);

          ReceiptDetailFormGUI DetailForm = new ReceiptDetailFormGUI(permissions, hoaDonBus, hoaDon);
          OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

          Overlay.Show(Form.ActiveForm);
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
      summaryCards.Update(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", DsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Giá trị hóa đơn gần nhất", DsHoaDon.OrderByDescending(hd => hd.NgLapHD).FirstOrDefault().TongTien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), "money_icon", Color.FromArgb(155, 89, 182)),
        });
    }
    // Handle edit submit event
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when products are edited
      summaryCards.Update(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", DsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Giá trị hóa đơn gần nhất", DsHoaDon.OrderByDescending(hd => hd.NgLapHD).FirstOrDefault().TongTien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), "money_icon", Color.FromArgb(155, 89, 182)),
        });
    }
    // Handle delete submit event
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when products are edited
      summaryCards.Update(new SummaryCard[] {
          new SummaryCard("Tổng hóa đơn", DsHoaDon.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Giá trị hóa đơn gần nhất", DsHoaDon.OrderByDescending(hd => hd.NgLapHD).FirstOrDefault().TongTien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), "money_icon", Color.FromArgb(155, 89, 182)),
        });
    }
    private void btnSearch_Click(object sender, EventArgs e)
    {
      List<HoaDonDTO> newDsHoaDon = hoaDonBus.FindByAnyProperty(txtSearch.Text.Trim().ToLower());
      if (newDsHoaDon.Count == 0)
      {
        MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      DsHoaDon = newDsHoaDon;

      LoadData();
    }
  }
}
