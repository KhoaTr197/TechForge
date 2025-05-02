using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ReportingServices.Interfaces;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseControls;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class InvoiceTransactionPageGUI : Page
  {
    private NguoiDungDTO CurrentUser { get; set; }

    // BUS objects
    private HoaDonBUS hoaDonBUS;
    private SanPhamBUS sanPhamBUS;
    private HoiVienBUS hoiVienBUS;
    private LichSuHoatDongBUS lichSuHoatDongBUS;

    // Lists to store data
    private List<SanPhamDTO> dsSanPham;
    private List<HoiVienDTO> dsHoiVien;
    private List<ChiTietHoaDonDTO> DsCthd;

    public decimal TongTienHang { get; private set; }
    public decimal GiamGiaHD { get; private set; }
    public decimal ThanhTienHD { get; private set; }
    public decimal TienKhachDua { get; private set; }
    public decimal TienThua { get; private set; }

    public InvoiceTransactionPageGUI(NguoiDungDTO _CurrentUser)
    {
      InitializeComponent();
      this.CurrentUser = _CurrentUser;

      InitializeBUS();
      GetData();

      this.Dock = DockStyle.Fill;
      this.Font = new Font("Segoe UI", 10);
      this.toolMenuChiTietHDXoa.Click += toolMenuChiTietHDXoa_Click;
    }

    private void toolMenuChiTietHDXoa_Click(object sender, EventArgs e)
    {
      if (dgvChiTietHD.SelectedRows.Count > 0)
      {
        int rowIndex = dgvChiTietHD.SelectedRows[0].Index;
        DsCthd.RemoveAt(rowIndex);

        dgvChiTietHD.DataSource = null;
        dgvChiTietHD.DataSource = DsCthd;

        UpdateInvoiceSummary();
      }
    }

    private void UpdateInvoiceSummary()
    {
      TongTienHang = DsCthd.Sum(ct => ct.Gia * ct.SoLuong);
      GiamGiaHD = DsCthd.Sum(ct => ct.SoTienKm);
      ThanhTienHD = TongTienHang - GiamGiaHD;
      TienKhachDua = nudKhachDua.Value;
      TienThua = Math.Max(TienKhachDua - ThanhTienHD, 0);

      lblTongTienHang.Text = string.Format("{0:N0} đ", TongTienHang);
      lblGiamGiaHD.Text = string.Format("{0:N0} đ", GiamGiaHD);
      lblThanhTienHD.Text = string.Format("{0:N0} đ", ThanhTienHD);
      lblTienNhan.Text = string.Format("{0:N0} đ", TienKhachDua);
      lblTienThua.Text = string.Format("{0:N0} đ", TienThua);
    }
    // Initialize BUS
    private void InitializeBUS()
    {
      hoaDonBUS = new HoaDonBUS(connStr);
      sanPhamBUS = new SanPhamBUS(connStr);
      hoiVienBUS = new HoiVienBUS(connStr);
      hoaDonBUS = new HoaDonBUS(connStr);
      lichSuHoatDongBUS = new LichSuHoatDongBUS(connStr);
    }
    // Get data
    private void GetData()
    {
      dsSanPham = sanPhamBUS.GetAllConnected();
      dsHoiVien = hoiVienBUS.GetAllConnected();
      DsCthd = new List<ChiTietHoaDonDTO>();
    }
    private void btnTimKiemSP_Click(object sender, EventArgs e)
    {
      string searchText = txtTimKiemSP.Text.Trim().ToLower();

      lstSearchResults.Items.Clear();

      dsSanPham = sanPhamBUS.FindBy(name: searchText);

      dgvTimKiemSP.DataSource = dsSanPham;
    }

    private void btnTimKiemHV_Click(object sender, EventArgs e)
    {
      string searchText = txtTimKiemHV.Text.Trim().ToLower();

      lstSearchResults.Items.Clear();

      dsHoiVien = hoiVienBUS.FindByIdOrName(searchText);

      lstSearchResults.Items.AddRange(dsHoiVien.Select(hv => $"{hv.MaHV} - {hv.HoTen}").ToArray());
    }

    private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
      var selectedItem = lstSearchResults.SelectedItems[0].ToString().ToLower();

      var filteredResult = dsHoiVien
        .Find(hv => selectedItem.Contains(hv.MaHV.ToString()) && selectedItem.Contains(hv.HoTen.ToString().ToLower()));

      txtMaHV.Text = filteredResult.MaHV.ToString();
      txtHoTen.Text = filteredResult.HoTen;
      txtSdt.Text = filteredResult.Sdt;
      txtDchi.Text = filteredResult.Dchi;

    }
    private void btnThemVaoHD_Click(object sender, EventArgs e)
    {
      if(dgvTimKiemSP.CurrentRow == null)
      {
          MessageBox.Show("Chưa chọn sản phẩm!");
          return;
      }
      SanPhamDTO selectedSP = dgvTimKiemSP.CurrentRow.DataBoundItem as SanPhamDTO;
      
      if (DsCthd.Any(ct => ct.MaSP == selectedSP.MaSP))
      {
        var existingChiTietHD = DsCthd.First(x => x.MaSP == selectedSP.MaSP);
        existingChiTietHD.SoLuong += 1;
        existingChiTietHD.SoTienKm = (existingChiTietHD.Gia * existingChiTietHD.KhuyenMai / 100) * existingChiTietHD.SoLuong;
        existingChiTietHD.ThanhTien = existingChiTietHD.GiaCuoiCung * existingChiTietHD.SoLuong;
        dgvChiTietHD.Refresh();

        UpdateInvoiceSummary();
        return;
      }

      ChiTietHoaDonDTO newDetail = new ChiTietHoaDonDTO
      {
        MaSP = selectedSP.MaSP,
        HinhAnh = selectedSP.HinhAnh,
        TenSP = selectedSP.TenSP,
        Gia = selectedSP.Gia,
        KhuyenMai = selectedSP.KhuyenMai,
        SoTienKm = (selectedSP.Gia * selectedSP.KhuyenMai / 100),
        GiaCuoiCung = selectedSP.Gia - (selectedSP.Gia * selectedSP.KhuyenMai / 100),
        SoLuong = 1,
        ThanhTien = selectedSP.Gia - (selectedSP.Gia * selectedSP.KhuyenMai / 100),
      };

      DsCthd.Add(newDetail);

      dgvChiTietHD.DataSource = null;
      dgvChiTietHD.DataSource = DsCthd;

      UpdateInvoiceSummary();
    }
    private void dgvTimKiemSP_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        string columnName = dgvTimKiemSP.Columns[e.ColumnIndex].DataPropertyName;
        if (columnName == "Gia" || columnName == "ThanhTien")
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

    private void dgvChiTietHD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        string columnName = dgvChiTietHD.Columns[e.ColumnIndex].DataPropertyName;
        if (columnName == "Gia" || columnName == "ThanhTien")
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

    private void nudKhachDua_ValueChanged(object sender, EventArgs e)
    {
      UpdateInvoiceSummary();
    }

    private void btnTaoHoaDon_Click(object sender, EventArgs e)
    {
      if (dgvChiTietHD.Rows.Count == 0)
      {
        MessageBox.Show("Vui lòng thêm sản phẩm vào hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (string.IsNullOrEmpty(txtHoTen.Text))
      {
        MessageBox.Show("Vui lòng nhập thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      if (string.IsNullOrEmpty(txtDchi.Text))
      {
        MessageBox.Show("Vui lòng nhập địa chỉ khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      if (nudKhachDua.Value < ThanhTienHD)
      {
        MessageBox.Show("Tiền khách đưa không đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      HoaDonDTO newHoaDon = new HoaDonDTO
      {
        MaHV = string.IsNullOrWhiteSpace(txtMaHV.Text) ? (int?)null : int.Parse(txtMaHV.Text),
        NgLapHD = DateTime.Now,
        HoTen = txtHoTen.Text,
        Sdt = txtSdt.Text,
        DiaChi = txtDchi.Text,
        NvLapHD = CurrentUser.MaND,
        TongTien = ThanhTienHD,
        Cthd = DsCthd.ToList(),
      };
      int newReceiptId = hoaDonBUS.Add(newHoaDon);
      if (newReceiptId != -1)
      {
        newHoaDon.MaHD = newReceiptId;
        UserNotification notify = new UserNotification("Tạo hóa đơn thành công!");
        notify.Show();

        lichSuHoatDongBUS.Add(new LichSuHoatDongDTO()
        {
          MaND = CurrentUser.MaND,
          NoiDung = $"Tạo hoá đơn #{newReceiptId}",
          ThoiGian = newHoaDon.NgLapHD,
          VaiTro = CurrentUser.VaiTro,
        });
        ReportReceiptDetailFormGUI rdfrm = new ReportReceiptDetailFormGUI(newHoaDon);
        rdfrm.Show();

        DsCthd.Clear();
        dgvChiTietHD.DataSource = null;
        dgvTimKiemSP.DataSource = null;
        txtMaHV.Clear();
        txtHoTen.Clear();
        txtSdt.Clear();
        txtDchi.Clear();
        nudKhachDua.Value = 0;
        lblTongTienHang.Text = "0 đ";
        lblGiamGiaHD.Text = "0 đ";
        lblThanhTienHD.Text = "0 đ";
        lblTienNhan.Text = "0 đ";
        lblTienThua.Text = "0 đ";
      }
      else
      {
        UserNotification notify = new UserNotification("Tạo hóa đơn thất bại!", "error");
        notify.Show();
      }
    }

    private void dgvChiTietHD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      ChiTietHoaDonDTO detail = DsCthd[e.RowIndex];
      detail.SoLuong = Convert.ToInt32(dgvChiTietHD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
      detail.ThanhTien = detail.GiaCuoiCung * detail.SoLuong;

      UpdateInvoiceSummary();
    }
  }
}
