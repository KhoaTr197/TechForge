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
    private SanPhamBUS busSanPham { get; set; }
    private List<NguoiDungDTO> DsNhanVienKho { get; set; }
    private List<SanPhamDTO> dsSanPham { get; set; }
    private ChiTietLichSuKhoDTO ChiTietLichSuKho { get; set; }
    private Notification notify;
    public ImportExportDetailFormGUI(LichSuKhoBUS _BUS, SanPhamBUS _busSanPham, LichSuKhoDTO _thongTinLichSu = null, List<NguoiDungDTO> _DsNhanVienKho = null)
    {
      InitializeComponent();

      this.ThongTinLichSu = _thongTinLichSu;
      this.BUS = _BUS;
      this.busSanPham = _busSanPham;
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
        notify = new Notification("Thêm thành công");
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
        notify = new Notification("Cập nhật thành công");
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

    private void InitializeInfoPanel()
    {
      // Right panel for product details
      pnlRight = new Panel
      {
        Dock = DockStyle.Fill,
        Margin = new Padding(5),
        Padding = new Padding(10),
        BorderStyle = BorderStyle.FixedSingle
      };

      // Search controls
      Panel pnlSearch = new Panel
      {
        Dock = DockStyle.Top,
        Height = 40,
        Padding = new Padding(3)
      };

      txtSearch = new TextBox
      {
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
        Text = "Tìm kiếm sản phẩm..."
      };

      txtSearch.TextChanged += txtSearch_TextChanged;

      // Search results list
      lstSearchResults = new ListBox
      {
        Dock = DockStyle.Top,
        Height = 100,
        DisplayMember = "TenSP",
        Font = new Font(DefaultFontName, 12),
        BorderStyle = BorderStyle.FixedSingle,
        Visible = false,
        ScrollAlwaysVisible = true,
        SelectionMode = SelectionMode.One,
        HorizontalScrollbar = true,
      };

      lstSearchResults.SelectedIndexChanged += lstSearchResults_SelectedIndexChanged;

      pnlSearch.Controls.Add(txtSearch);

      // Product info panel
      tlpProductInfo = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 6,
        ColumnStyles =
        {
          new ColumnStyle(SizeType.Absolute, 95F),
          new ColumnStyle(SizeType.Percent, 100F)
        },
        RowStyles =
        {
          new RowStyle(SizeType.Absolute, 35F),
          new RowStyle(SizeType.Absolute, 35F),
          new RowStyle(SizeType.Absolute, 35F),
          new RowStyle(SizeType.Absolute, 35F),
          new RowStyle(SizeType.Percent, 100F),
          new RowStyle(SizeType.AutoSize)
        },
        Padding = new Padding(5),
        Margin = new Padding(0, 5, 0, 0),
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };

      // Product info labels with larger font
      lblProductName = CreateInfoLabel("Tên SP:", 12);
      lblProductPrice = CreateInfoLabel("Giá:", 12);
      lblProductStock = CreateInfoLabel("Số lượng:", 12);
      lblProductTotal = CreateInfoLabel("Tổng Tiền:", 12);

      lblProductNameValue = CreateInfoLabel("", 12);
      lblProductPriceValue = CreateInfoLabel("", 12);
      lblProductTotalValue = CreateInfoLabel("", 12);
      lblProductNameValue.AutoEllipsis = true;
      lblProductPriceValue.AutoEllipsis = true;
      lblProductTotalValue.AutoEllipsis = true;

      nudQuantity = new NumericUpDown
      {
        Dock = DockStyle.Fill,
        Minimum = 1,
        Maximum = 1000,
        Value = 1,
        Font = new Font(DefaultFontName, 12),
        Margin = new Padding(3, 8, 3, 3),
      };
      nudQuantity.ValueChanged += nudQuantity_ValueChanged;

      // Add to receipt button
      btnAddToLog = new Button
      {
        Text = "Thêm vào lịch sử",
        Height = 35,
        Font = new Font(DefaultFontName, 12),
        BackColor = Color.FromArgb(0, 123, 255),
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Dock = DockStyle.Fill,
      };

      btnAddToLog.Click += btnAddToLog_Click;

      btnUpdateToLog = new Button
      {
        Text = "Cập nhật",
        Height = 35,
        Font = new Font(DefaultFontName, 12),
        BackColor = Color.Orange,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Dock = DockStyle.Fill,
        Enabled = false,
      };

      btnUpdateToLog.Click += btnUpdateToLog_Click;

      // Create a panel to center the button
      TableLayoutPanel buttonPanel = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        Height = 45,
        ColumnCount = 2,
        ColumnStyles =
        {
          new ColumnStyle(SizeType.Percent, 50F),
          new ColumnStyle(SizeType.Percent, 50F)
        }
      };

      tlpProductInfo.Controls.Add(lblProductName, 0, 0);
      tlpProductInfo.Controls.Add(lblProductPrice, 0, 1);
      tlpProductInfo.Controls.Add(lblProductTotal, 0, 2);
      tlpProductInfo.Controls.Add(lblProductStock, 0, 3);

      tlpProductInfo.Controls.Add(lblProductNameValue, 1, 0);
      tlpProductInfo.Controls.Add(lblProductPriceValue, 1, 1);
      tlpProductInfo.Controls.Add(lblProductTotalValue, 1, 2);

      tlpProductInfo.Controls.Add(nudQuantity, 1, 3);

      buttonPanel.Controls.Add(btnAddToLog, 0, 0);
      buttonPanel.Controls.Add(btnUpdateToLog, 1, 0);

      tlpProductInfo.Controls.Add(buttonPanel, 0, 5);
      tlpProductInfo.SetColumnSpan(buttonPanel, 2);

      // Set all label fonts and styles
      foreach (Control control in tlpProductInfo.Controls)
      {
        if (control is Label lbl)
        {
          lbl.Font = new Font(DefaultFontName, 12);
          lbl.AutoSize = true;
          if (control.Name.EndsWith("Value"))
          {
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
          }
        }
      }

      // Add panels to right panel
      pnlRight.Controls.Add(tlpProductInfo);
      pnlRight.Controls.Add(lstSearchResults);
      pnlRight.Controls.Add(pnlSearch);
    }
    private void InitializeDetailList()
    {
      // Info table
      tlpInfo = new TableLayoutPanel
      {
        ColumnCount = 4,
        RowCount = 2,
        ColumnStyles =
        {
          new ColumnStyle(SizeType.Absolute, 100F), // Increased width for labels
          new ColumnStyle(SizeType.Percent, 50F),
          new ColumnStyle(SizeType.Absolute, 100F), // Increased width for labels
          new ColumnStyle(SizeType.Percent, 50F),
        },
        Dock = DockStyle.Fill,
        AutoSize = true,
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };
    }

    private void GetData()
    {
      this.dsSanPham = busSanPham.GetAllConnected();
    }
    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
      lstSearchResults.Items.Clear();

      string searchText = txtSearch.Text?.Trim().ToLower() ?? string.Empty;

      var filteredResults = dsSanPham
        .FindAll(sp => sp.TenSP != null && sp.TenSP.ToLower().Contains(searchText))
        .Select(sp => $"{sp.MaSP} - {sp.TenSP}");

      lstSearchResults.Items.AddRange(filteredResults.ToArray());

      lstSearchResults.Visible = true;
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
      cboNhanVienLap.DataSource = DsNhanVienKho;
      cboNhanVienLap.DisplayMember = "MaTenND";
      cboNhanVienLap.ValueMember = "MaND";

      cboHoatDong.DataSource = new List<string> { "Nhập", "Xuất" };
      cboHoatDong.SelectedIndex = ThongTinLichSu.HoatDong ? 1 : 0;
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
