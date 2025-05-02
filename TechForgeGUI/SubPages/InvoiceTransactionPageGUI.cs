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
      TongTienHang = DsCthd.Sum(ct => ct.ThanhTien);
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

      txtHoTen.Text = filteredResult.HoTen;
      txtSdt.Text = filteredResult.Sdt;
      txtDchi.Text = filteredResult.Dchi;

    }
    private void btnThemVaoHD_Click(object sender, EventArgs e)
    {
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

      ChiTietHoaDonDTO newDetail = new ChiTietHoaDonDTO {
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
      HoaDonDTO newHoaDon = new HoaDonDTO
      {
        MaHV = dsHoiVien.FirstOrDefault(hv => hv.HoTen == txtHoTen.Text).MaHV,
        NgLapHD = DateTime.Now,
        HoTen = txtHoTen.Text,
        Sdt = txtSdt.Text,
        DiaChi = txtDchi.Text,
        NvLapHD = CurrentUser.MaND,
        TongTien = ThanhTienHD,
        Cthd = DsCthd
      };
      if (hoaDonBUS.Add(newHoaDon) != -1)
      {
        UserNotification notify = new UserNotification("Tạo hóa đơn thành công!");
        notify.Show();
      } else
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

<<<<<<< HEAD
=======
        subtotal += quantity * price;
        discount += quantity * price * (itemDiscount / 100);
      }

      decimal total = (subtotal - discount);

      newHoaDon.TongTien = total;
      // Update labels
      lblSubtotalValue.Text = subtotal.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
      lblDiscountValue.Text = discount.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
      lblTotalValue.Text = total.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
      lblCashTakenValue.Text = cashPaid.ToString("C0", new CultureInfo("vi-VN"));
      lblCustomerChangeGivenValue.Text = (cashPaid - total).ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
    }
    private void BtnCreateInvoice_Click(object sender, EventArgs e)
    {
      // Validate
      if (dgvInvoiceItems.Rows.Count == 0)
      {
        MessageBox.Show("Vui lòng thêm sản phẩm vào hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      
      if (string.IsNullOrEmpty(txtCustomerName.Text))
      {
        MessageBox.Show("Vui lòng nhập thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
        if (string.IsNullOrEmpty(txtCustomerAddress.Text))
        {
            MessageBox.Show("Vui lòng nhập địa chỉ khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }


            if(selectedCustomer != null && selectedCustomer.MaHV > 0)
            {
                newHoaDon.MaHV = selectedCustomer.MaHV;
            }
            newHoaDon.DiaChi = txtCustomerAddress.Text;
            newHoaDon.Sdt = txtCustomerPhone.Text;
            newHoaDon.HoTen = txtCustomerName.Text;
            newHoaDon.NgLapHD = DateTime.Now;
            newHoaDon.NvLapHD = currentUser.MaND;

            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                int soLuong = (int)row.Cells["SoLuong"].Value;
                decimal gia = (decimal)row.Cells["Gia"].Value;
                int km = int.Parse(row.Cells["KhuyenMai"].Value.ToString());
                decimal soTienKm = gia * (km / (decimal)100);
                decimal giaCuoiCung = gia - soTienKm;
                
                dsChiTietHoaDon.Add(new ChiTietHoaDonDTO()
                {
                    MaSP = (int)row.Cells["MaSP"].Value,
                    TenSP = row.Cells["TenSP"].Value.ToString(),
                    Gia = gia,
                    SoLuong = soLuong,
                    KhuyenMai = km,
                    SoTienKm = soTienKm,
                    GiaCuoiCung = giaCuoiCung,
                    ThanhTien = giaCuoiCung * soLuong,
                });
            }

            newHoaDon.Cthd = dsChiTietHoaDon;
            
            int newReceiptId = hoaDonBUS.Add(newHoaDon);
            if (newReceiptId > 0)
            {
                MessageBox.Show("Tạo hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lichSuHoatDongBUS.Add(new LichSuHoatDongDTO()
                {
                    MaND = currentUser.MaND,
                    NoiDung = $"Tạo hoá đơn #{newReceiptId}",
                    ThoiGian = newHoaDon.NgLapHD,
                    VaiTro = currentUser.VaiTro,
                });
                ReportReceiptDetailFormGUI rdfrm = new ReportReceiptDetailFormGUI(newHoaDon);
                rdfrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tạo hóa đơn không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

      // Clear form
      dgvInvoiceItems.Rows.Clear();
      dsCTHD.Clear();
      dsChiTietHoaDon.Clear();
      newHoaDon = new HoaDonDTO();

      txtCustomerSearch.Text = "";
      txtCustomerName.Text = "";
      txtCustomerPhone.Text = "";
      txtCustomerAddress.Text = "";
      selectedCustomer = null;
>>>>>>> 6e49bb3 (update Report Receipt Detail)
      UpdateInvoiceSummary();
    }
  }
}
