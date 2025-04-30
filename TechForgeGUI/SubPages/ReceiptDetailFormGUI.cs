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

namespace TechForgeGUI.SubPages
{
  public partial class ReceiptDetailFormGUI : DetailFormGUI
  {
    private HoaDonDTO ThongTinHoaDon { get; set; }
    private HoaDonBUS BUS { get; set; }
    private ChiTietHoaDonDTO selectedProduct;
    private RolePermissions permissions { get; set; }
    public ReceiptDetailFormGUI(RolePermissions _permissions, HoaDonBUS _BUS, HoaDonDTO _thongTinHoaDon=null)
    {
      InitializeComponent();

      this.ThongTinHoaDon = _thongTinHoaDon;
      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết hóa đơn";

      this.btnAdd.Enabled = false;
      this.btnAdd.Visible = false;

      InitializeDataGridView();

      this.Load += ReceiptDetailFormGUI_LoadDetailForm;
    }
    private void ReceiptDetailFormGUI_LoadDetailForm(object sender, EventArgs e)
    {
      txtMaHD.Text = ThongTinHoaDon.MaHD.ToString();
      txtMaHD.ReadOnly = true;

      txtMaHV.Text = ThongTinHoaDon.MaHV.ToString();
      txtMaHV.ReadOnly = true;

      txtHoTen.Text = ThongTinHoaDon.HoTen.ToString();
      txtDchi.Text = ThongTinHoaDon.DiaChi.ToString();
      txtSdt.Text = ThongTinHoaDon.Sdt.ToString();
      
      txtNhanVienLap.Text = ThongTinHoaDon.NvLapHD.ToString();
      txtNhanVienLap.ReadOnly = true;

      dtpNgayLap.Value = ThongTinHoaDon.NgLapHD;
      dtpNgayLap.Enabled = false;

      lblTongTien.Text = string.Format("{0:N0} đ", ThongTinHoaDon.TongTien);
    }
    private void InitializeDataGridView()
    {
      // Load data
      dgvMainList.DataSource = ThongTinHoaDon.Cthd;
    }
    private void Dgv_CellFormating(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null && e.ColumnIndex >= 0)
      {
        string columnName = dgvMainList.Columns[e.ColumnIndex].DataPropertyName;
        if (columnName == "Gia" || columnName == "SoTienKm" || columnName == "GiaCuoiCung" || columnName == "ThanhTien")
        {
          e.Value = string.Format("{0:N0} đ", Convert.ToDecimal(e.Value));
          e.FormattingApplied = true;
        }
        else if (columnName == "HinhAnh")
        {
          string imagePath = Path.Combine(Application.StartupPath, "Resources", "ProductImages", $"{e.Value}.png");

          if (File.Exists(imagePath))
          {
            e.Value = Image.FromFile(imagePath);
            e.FormattingApplied = true;
          }
        }
      }
    }
  }
}
