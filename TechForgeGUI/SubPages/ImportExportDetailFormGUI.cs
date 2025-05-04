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
  public partial class ImportExportDetailFormGUI : DetailFormGUI
  {
    private LichSuKhoDTO ThongTinLichSu { get; set; }
    private LichSuKhoBUS BUS { get; set; }
    private SanPhamBUS sanPhamBus { get; set; }
    private List<NguoiDungDTO> DsNhanVienKho { get; set; }
    private List<SanPhamDTO> dsSanPham { get; set; }
    private ChiTietLichSuKhoDTO ChiTietLichSuKho { get; set; }
    private UserNotification notify;
    public ImportExportDetailFormGUI(LichSuKhoBUS _BUS, SanPhamBUS _busSanPham, LichSuKhoDTO _thongTinLichSu = null, List<NguoiDungDTO> _DsNhanVienKho = null)
    {
      InitializeComponent();

      this.ThongTinLichSu = _thongTinLichSu;
      this.BUS = _BUS;
      this.sanPhamBus = _busSanPham;
      this.DsNhanVienKho = _DsNhanVienKho;
      this.Text = "Chi tiết lịch sử";

      GetData();

      if (ThongTinLichSu == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;

        ThongTinLichSu = new LichSuKhoDTO
        {
          MaLS = BUS.GetNextId(),
          HoatDong = true,
          TongTien = 0,
          ThoiGian = DateTime.Now,
          Ctlsk = new List<ChiTietLichSuKhoDTO>()
        };

        LoadAddForm();
      }
      else
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;

        LoadDetailForm();
      }

      this.btnDelete.Visible = false;
      this.btnDelete.Enabled = false;

      btnAdd.Click += BtnAdd_Click;
      btnEdit.Click += BtnEdit_Click;
      btnDelete.Click += BtnDelete_Click;
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      if (BUS.Add(ThongTinLichSu) != -1)
      {
        notify = new UserNotification("Thêm thành công");
        notify.Show();
        OnAddSubmit(new DetailFormAddSubmitEventArgs(this));
      }
    }
    private void BtnEdit_Click(object sender, EventArgs e)
    {
      ThongTinLichSu.MaND = cboNhanVienLap.SelectedValue.ToString();
      ThongTinLichSu.TongTien = ThongTinLichSu.Ctlsk.Sum(x => x.ThanhTien ?? 0);

      if (BUS.Update(ThongTinLichSu))
      {
        notify = new UserNotification("Cập nhật thành công");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs(this));
      }
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
      //if (BUS.Delete(ThongTinLichSu.MaLS))
      //{
      //  OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs(this));
      //}
    }
    private void GetData()
    {
      this.dsSanPham = sanPhamBus.GetAllConnected();
    }
    private void btnSearch_Click(object sender, EventArgs e) {
      string searchText = txtSearch.Text?.Trim().ToLower() ?? string.Empty;

      lstSearchResults.Items.Clear();

      dsSanPham = sanPhamBus.FindBy(name: searchText);

      lstSearchResults.Items.AddRange(dsSanPham.Select(sp => $"{sp.MaSP} - {sp.TenSP}").ToArray());
    }
    private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
      var selectedItem = lstSearchResults.SelectedItems[0].ToString().ToLower();

      var filteredResult = dsSanPham
       .Find(sp => selectedItem.Contains(sp.MaSP.ToString()) && selectedItem.Contains(sp.TenSP.ToString().ToLower()));

      ChiTietLichSuKho = new ChiTietLichSuKhoDTO
      {
        MaSP = filteredResult.MaSP,
        TenSP = filteredResult.TenSP,
        Gia = filteredResult.Gia,
        SoLuong = 1,
        ThanhTien = filteredResult.Gia,
        HinhAnh = filteredResult.HinhAnh,
        HoatDong = cboHoatDong.SelectedIndex == 1 ? true : false
      };

      txtMaSP.Text = filteredResult.MaSP.ToString();
      txtTenSP.Text = filteredResult.TenSP;
      nudGia.Value = filteredResult.Gia;
      nudSoLuong.Value = 1;
      nudChiTietTongTien.Value = filteredResult.Gia;

      btnChiTietThem.Enabled = true;
      btnChiTietThem.BackColor = Color.DodgerBlue;

      btnChiTietCapNhat.Enabled = false;
      btnChiTietCapNhat.BackColor = Color.Gray;
    }

    private void btnChiTietThem_Click(object sender, EventArgs e)
    {
      ChiTietLichSuKhoDTO newChiTietLichSuKho = new ChiTietLichSuKhoDTO
      {
        MaSP = int.Parse(txtMaSP.Text),
        TenSP = txtTenSP.Text,
        Gia = decimal.Parse(nudGia.Value.ToString()),
        SoLuong = int.Parse(nudSoLuong.Value.ToString()),
        ThanhTien = decimal.Parse(nudChiTietTongTien.Value.ToString()),
        HinhAnh = ChiTietLichSuKho.HinhAnh,
        HoatDong = cboHoatDong.SelectedIndex == 1 ? true : false
      };

      if (ThongTinLichSu.Ctlsk.Any(x => x.MaSP == newChiTietLichSuKho.MaSP))
      {
        var existingChiTietLichSuKho = ThongTinLichSu.Ctlsk.First(x => x.MaSP == newChiTietLichSuKho.MaSP);
        existingChiTietLichSuKho.SoLuong += newChiTietLichSuKho.SoLuong;
        existingChiTietLichSuKho.ThanhTien += newChiTietLichSuKho.ThanhTien ?? 0;
        dgvDetail.Refresh();
        return;
      }

      ThongTinLichSu.Ctlsk.Add(newChiTietLichSuKho);

      ThongTinLichSu.TongTien += newChiTietLichSuKho.ThanhTien ?? 0;

      nudTongTien.Value = ThongTinLichSu.Ctlsk.Sum(x => x.ThanhTien ?? 0);

      dgvDetail.DataSource = null;
      dgvDetail.DataSource = ThongTinLichSu.Ctlsk;
    }
    private void btnChiTietCapNhat_Click(object sender, EventArgs e)
    {
      int idx = -1;
      foreach (DataGridViewRow row in dgvDetail.Rows)
      {
        if (row.Cells["dgvTxtColMaSP"].Value != null && row.Cells["dgvTxtColMaSP"].Value.ToString().Equals(txtMaSP.Text))
        {
          idx = row.Index;
          break;
        }
      }

      if(idx == -1)
      {
        MessageBox.Show("Không tìm thấy sản phẩm để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      ChiTietLichSuKhoDTO selectedProduct = dgvDetail.Rows[idx].DataBoundItem as ChiTietLichSuKhoDTO;

      if (selectedProduct == null)
      {
        MessageBox.Show("Không tìm thấy sản phẩm để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      selectedProduct.MaSP = int.Parse(txtMaSP.Text);
      selectedProduct.TenSP = txtTenSP.Text;
      selectedProduct.Gia = decimal.Parse(nudGia.Value.ToString());
      selectedProduct.SoLuong = int.Parse(nudSoLuong.Value.ToString());
      selectedProduct.ThanhTien = decimal.Parse(nudChiTietTongTien.Value.ToString());

      nudTongTien.Value = ThongTinLichSu.Ctlsk.Sum(x => x.ThanhTien ?? 0);

      dgvDetail.Refresh();
    }
    private void LoadAddForm()
    {
      txtMa.Text = BUS.GetNextId().ToString();

      cboNhanVienLap.DataSource = DsNhanVienKho;
      cboNhanVienLap.DisplayMember = "MaTenND";
      cboNhanVienLap.ValueMember = "MaND";

      cboHoatDong.DataSource = new List<string> { "Nhập", "Xuất" };
      cboHoatDong.SelectedIndex = 0;
      dgvDetail.AutoGenerateColumns = false;
      dgvDetail.DataSource = ThongTinLichSu.Ctlsk;
    }
    private void LoadDetailForm()
    {
      txtMa.Text = ThongTinLichSu.MaLS.ToString();
      dtpThoiGian.Value = ThongTinLichSu.ThoiGian;
      nudTongTien.Value = ThongTinLichSu.TongTien;

      cboNhanVienLap.DataSource = DsNhanVienKho;
      cboNhanVienLap.DisplayMember = "MaTenND";
      cboNhanVienLap.ValueMember = "MaND";

      cboHoatDong.DataSource = new List<string> { "Nhập", "Xuất" };
      cboHoatDong.SelectedIndex = ThongTinLichSu.HoatDong ? 1 : 0;
      cboHoatDong.Enabled = false;

      dgvDetail.AutoGenerateColumns = false;
      dgvDetail.DataSource = ThongTinLichSu.Ctlsk;
    }
    private void dgvDetail_SelectionChanged(object sender, EventArgs e)
    {
      if (dgvDetail.SelectedRows.Count > 0)
      {
        DataGridViewRow row = dgvDetail.SelectedRows[0];
        ChiTietLichSuKhoDTO selectedProduct = row.DataBoundItem as ChiTietLichSuKhoDTO;

        if (selectedProduct != null) {
          txtMaSP.Text = selectedProduct.MaSP.ToString();
          txtTenSP.Text = selectedProduct.TenSP;
          nudGia.Value = selectedProduct.Gia == null ? 0 : (decimal)selectedProduct.Gia;
          nudSoLuong.Value = selectedProduct.SoLuong;
          nudChiTietTongTien.Value = selectedProduct.ThanhTien ?? 0;
        }

        btnChiTietThem.Enabled = false;
        btnChiTietThem.BackColor = Color.Gray;

        btnChiTietCapNhat.Enabled = true;
        btnChiTietCapNhat.BackColor = Color.Orange;
      }
    }
    private void dgvDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null && e.ColumnIndex >= 0)
      {
        string columnName = dgvDetail.Columns[e.ColumnIndex].DataPropertyName;
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

    private void nudSoLuong_ValueChanged(object sender, EventArgs e)
    {
      nudChiTietTongTien.Value = nudSoLuong.Value * nudGia.Value;

      if (nudSoLuong.Value < 0)
      {
        nudSoLuong.Value = 0;
        nudChiTietTongTien.Value = 0;
      }
    }
  }
}
