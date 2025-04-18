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
    private HoaDonDTO thongTinHoaDon { get; set; }
    private HoaDonBUS BUS { get; set; }
    private TableLayoutPanel mainLayout;
    private TableLayoutPanel tlpInfo;
    private Panel pnlGrid;
    private Panel pnlRight;
    private DataGridView dgv;
    private TextBox txtSearch;
    private TableLayoutPanel tlpProductInfo;
    private Label lblProductName;
    private Label lblProductPrice;
    private Label lblProductDiscount;
    private Label lblProductStock;
    private NumericUpDown nudQuantity;
    private Button btnAddToReceipt;
    private ChiTietHoaDonDTO selectedProduct;
    private ListBox lstSearchResults;

    public ReceiptDetailFormGUI(HoaDonDTO _thongTinHoaDon, HoaDonBUS _BUS)
    {
      InitializeComponent();

      this.thongTinHoaDon = _thongTinHoaDon;
      this.BUS = _BUS;
      this.Text = "Chi tiết hóa đơn";

      // Set up main layout with 2x2 grid
      mainLayout = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 2,
        ColumnStyles =
        {
          new ColumnStyle(SizeType.Percent, 65F), // Increased main content area
          new ColumnStyle(SizeType.Percent, 35F)  // Decreased right panel width
        },
        RowStyles =
        {
          new RowStyle(SizeType.Absolute, 200F),
          new RowStyle(SizeType.Percent, 100F)
        },
        Padding = new Padding(4, 32, 4, 32),
        BackColor = Color.White
      };

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

      // Grid panel
      pnlGrid = new Panel
      {
        Dock = DockStyle.Fill,
        Margin = new Padding(5)
      };

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
        Font = new Font(DefaultFontName, 12),
        BorderStyle = BorderStyle.FixedSingle,
        Visible = false,
        ScrollAlwaysVisible = true
      };

      lstSearchResults.SelectedIndexChanged += lstSearchResults_SelectedIndexChanged;

      pnlSearch.Controls.Add(txtSearch);

      // Product info panel
      tlpProductInfo = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 8,
        ColumnStyles = 
        {
          new ColumnStyle(SizeType.Absolute, 95F),
          new ColumnStyle(SizeType.Percent, 100F)
        },
        RowStyles =
        {
          new RowStyle(SizeType.Absolute, 35F), // Tên SP
          new RowStyle(SizeType.Absolute, 35F), // Giá gốc
          new RowStyle(SizeType.Absolute, 35F), // % KM
          new RowStyle(SizeType.Absolute, 35F), // Tiền KM
          new RowStyle(SizeType.Absolute, 35F), // Giá cuối
          new RowStyle(SizeType.Absolute, 35F), // Số lượng
          new RowStyle(SizeType.Absolute, 35F), // Thành tiền
          new RowStyle(SizeType.Absolute, 45F)  // Button
        },
        Padding = new Padding(5),
        Margin = new Padding(0, 5, 0, 0),
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };

      // Product info labels with larger font
      lblProductName = CreateInfoLabel("Tên SP:", 12);
      lblProductPrice = CreateInfoLabel("Giá gốc:", 12);
      lblProductDiscount = CreateInfoLabel("% KM:", 12);
      var lblDiscountAmount = CreateInfoLabel("Tiền KM:", 12);
      var lblFinalPrice = CreateInfoLabel("Giá cuối:", 12);
      var lblQuantity = CreateInfoLabel("Số lượng:", 12);
      var lblTotal = CreateInfoLabel("Thành tiền:", 12);

      nudQuantity = new NumericUpDown
      {
        Dock = DockStyle.Fill,
        Minimum = 1,
        Maximum = 1000,
        Value = 1,
        Font = new Font(DefaultFontName, 12)
      };

      nudQuantity.ValueChanged += NudQuantity_ValueChanged;

      // Add to receipt button
      btnAddToReceipt = new Button
      {
        Text = "Thêm vào hóa đơn",
        Height = 35,
        Font = new Font(DefaultFontName, 12),
        BackColor = Color.FromArgb(0, 123, 255),
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Margin = new Padding(0, 5, 0, 0),
        Anchor = AnchorStyles.None,
        AutoSize = false,
        Width = 200
      };

      btnAddToReceipt.Click += BtnAddToReceipt_Click;

      // Create a panel to center the button
      Panel buttonPanel = new Panel
      {
        Dock = DockStyle.Fill,
        Height = 45
      };
      buttonPanel.Controls.Add(btnAddToReceipt);
      btnAddToReceipt.Location = new Point((buttonPanel.Width - 200) / 2, 5);

      // Add controls to product info panel
      tlpProductInfo.Controls.Add(lblProductName, 0, 0);
      tlpProductInfo.Controls.Add(new Label { Text = "", Name = "lblProductNameValue", AutoEllipsis = true }, 1, 0);
      tlpProductInfo.Controls.Add(lblProductPrice, 0, 1);
      tlpProductInfo.Controls.Add(new Label { Text = "", Name = "lblProductPriceValue" }, 1, 1);
      tlpProductInfo.Controls.Add(lblProductDiscount, 0, 2);
      tlpProductInfo.Controls.Add(new Label { Text = "", Name = "lblProductDiscountValue" }, 1, 2);
      tlpProductInfo.Controls.Add(lblDiscountAmount, 0, 3);
      tlpProductInfo.Controls.Add(new Label { Text = "", Name = "lblDiscountAmountValue" }, 1, 3);
      tlpProductInfo.Controls.Add(lblFinalPrice, 0, 4);
      tlpProductInfo.Controls.Add(new Label { Text = "", Name = "lblFinalPriceValue" }, 1, 4);
      tlpProductInfo.Controls.Add(lblQuantity, 0, 5);
      tlpProductInfo.Controls.Add(nudQuantity, 1, 5);
      tlpProductInfo.Controls.Add(lblTotal, 0, 6);
      tlpProductInfo.Controls.Add(new Label { Text = "", Name = "lblTotalValue", Font = new Font(DefaultFontName, 12, FontStyle.Bold) }, 1, 6);
      tlpProductInfo.Controls.Add(buttonPanel, 0, 7);
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

      // Add panels to main layout with new structure
      mainLayout.Controls.Add(tlpInfo, 0, 0);
      mainLayout.Controls.Add(pnlGrid, 0, 1);
      mainLayout.Controls.Add(pnlRight, 1, 0);
      mainLayout.SetRowSpan(pnlRight, 2);

      this.Size = new Size(1150, 700); // Increased from 1000x600
      this.MinimumSize = new Size(1000, 600); // Increased from 900x500

      this.btnAdd.Visible = false;
      this.btnAdd.Enabled = false;

      Dictionary<string, string> inputLabels = new Dictionary<string, string>
        {
        { "MaHD", "Mã HĐ" }, // Shortened labels
        { "MaHV", "Mã HV" },
        { "HoTen", "Họ tên" },
        { "Sdt", "SĐT" },
        { "DiaChi", "Địa chỉ" },
        { "NvLapHD", "NV lập" },
        { "TongTien", "Tổng tiền" },
        { "NgLapHD", "Ngày lập" },
      };

      this.Controls.Add(mainLayout);
      LoadDetailForm(inputLabels);
      InitializeDataGridView();
    }

    private Label CreateInfoLabel(string text, float fontSize = 12)
    {
      return new Label
      {
        Text = text,
        AutoSize = true,
        Anchor = AnchorStyles.Left,
        Margin = new Padding(3, 8, 3, 3),
        Font = new Font(DefaultFontName, fontSize)
      };
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
      //TODO
    }

    private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
      //TODO
    }

    private void BtnAddToReceipt_Click(object sender, EventArgs e)
    {
      if (selectedProduct == null)
      {
        MessageBox.Show("Vui lòng chọn sản phẩm trước khi thêm vào hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      int quantity = (int)nudQuantity.Value;
      
      // Create a new receipt detail
      ChiTietHoaDonDTO newDetail = new ChiTietHoaDonDTO
      {
        MaHD = thongTinHoaDon.MaHD,
        MaSP = selectedProduct.MaSP,
        Gia = selectedProduct.Gia,
        KhuyenMai = selectedProduct.KhuyenMai,
        SoTienKm = selectedProduct.SoTienKm,
        GiaCuoiCung = selectedProduct.GiaCuoiCung,
        SoLuong = quantity,
        ThanhTien = selectedProduct.GiaCuoiCung * quantity
      };

      // TODO: Add to database
      // This would typically call a method in your BUS layer to add the detail
      // For now, we'll just show a message
      MessageBox.Show($"Đã thêm {quantity} {selectedProduct.TenSP} vào hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
      
      // Refresh the grid
      dgv.DataSource = BUS.GetDetailWithProducts(thongTinHoaDon);
      
      // Update the total amount
      UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
    {
      // Calculate the new total amount
      decimal totalAmount = thongTinHoaDon.Cthd.Sum(d => d.ThanhTien);
      
      // Update the total amount in the form
      if (Controls.Find("nudTongTien", true).FirstOrDefault() is NumericUpDown nudTongTien)
      {
        nudTongTien.Value = totalAmount;
      }
      
      // Update the total amount in the DTO
      thongTinHoaDon.TongTien = totalAmount;
    }

    private void LoadDetailForm(Dictionary<string, string> inputLabels)
    {
      int row = 0;
      int col = 0;

      foreach (var prop in thongTinHoaDon.GetType().GetProperties())
      {
        if (!inputLabels.ContainsKey(prop.Name)) continue;

        Label lbl = new Label
        {
          Text = inputLabels[prop.Name] + ":",
          AutoSize = true,
          Anchor = AnchorStyles.Left | AnchorStyles.Top,
          Margin = new Padding(3, 8, 3, 3),
          Font = new Font(DefaultFontName, 12)
        };

        Control control;
        if (prop.Name == "Cthd") continue;
        else if (prop.Name == "MaHD" || prop.Name == "NvLapHD")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinHoaDon)?.ToString(),
            Enabled = false,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else if (prop.PropertyType == typeof(DateTime))
        {
          control = new DateTimePicker
          {
            Name = "dtp" + prop.Name,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = (DateTime)prop.GetValue(thongTinHoaDon),
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else if (prop.PropertyType == typeof(decimal))
        {
          control = new NumericUpDown
          {
            Name = "nud" + prop.Name,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            ThousandsSeparator = true,
            Minimum = 0,
            Maximum = 1000000000,
            Value = Convert.ToDecimal(prop.GetValue(thongTinHoaDon)),
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinHoaDon)?.ToString(),
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }

        // Add row styles to ensure consistent height
        if (row >= tlpInfo.RowStyles.Count)
        {
          tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        }

        tlpInfo.Controls.Add(lbl, col, row);
        tlpInfo.Controls.Add(control, col + 1, row);

        col += 2;
        if (col >= 4)
        {
          col = 0;
          row++;
        }
      }

      // Ensure all rows have consistent height
      while (tlpInfo.RowStyles.Count < tlpInfo.RowCount)
      {
        tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
      }
    }
    private void InitializeDataGridView()
    {
      dgv = new DataGridView
      {
        Dock = DockStyle.Fill,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        ReadOnly = true,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        BackgroundColor = Color.White,
        BorderStyle = BorderStyle.Fixed3D,
        RowHeadersVisible = false,
        Font = new Font(DefaultFontName, 12),
        RowTemplate = { Height = 80 },
        AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
        ColumnHeadersHeight = 38
      };

      // Add columns with adjusted widths
      dgv.Columns.AddRange(new DataGridViewColumn[]
      {
        new DataGridViewTextBoxColumn { 
          Name = "MaSP",
          DataPropertyName =
          "MaSP",
          HeaderText = "Mã SP",
          Width = 75
        },
        new DataGridViewImageColumn { 
          Name = "HinhAnh",
          DataPropertyName = "HinhAnh",
          HeaderText = "Hình",
          Width = 100,
          ImageLayout = DataGridViewImageCellLayout.Zoom 
        },
        new DataGridViewTextBoxColumn { 
          Name = "TenSP", 
          DataPropertyName = "TenSP", 
          HeaderText = "Tên SP", 
          Width = 190 
        },
        new DataGridViewTextBoxColumn {
          Name = "Gia", 
          DataPropertyName = "Gia", 
          HeaderText = "Giá gốc", 
          Width = 110 
        },
        new DataGridViewTextBoxColumn { 
          Name = "KhuyenMai", 
          DataPropertyName = "KhuyenMai", 
          HeaderText = "KM", 
          Width = 75 
        },
        new DataGridViewTextBoxColumn { 
          Name = "SoTienKm", 
          DataPropertyName = "SoTienKm", 
          HeaderText = "Tiền KM",
          Width = 110 
        },
        new DataGridViewTextBoxColumn { 
          Name = "GiaCuoiCung", 
          DataPropertyName = "GiaCuoiCung", 
          HeaderText = "Giá cuối", 
          Width = 110 
        },
        new DataGridViewTextBoxColumn {
          Name = "SoLuong", 
          DataPropertyName = "SoLuong", 
          HeaderText = "SL",
          Width = 60
        },
        new DataGridViewTextBoxColumn { 
          Name = "ThanhTien", 
          DataPropertyName = "ThanhTien", 
          HeaderText = "T.Tiền", 
          Width = 110
        }
      });

      // Format currency columns
      dgv.CellFormatting += Dgv_CellFormating;

      // Handle row selection
      dgv.SelectionChanged += Dgv_SelectionChanged;

      pnlGrid.Controls.Add(dgv);
      
      // Load data
      dgv.DataSource = thongTinHoaDon.Cthd;
    }

    private void Dgv_SelectionChanged(object sender, EventArgs e)
    {
      if (dgv.SelectedRows.Count > 0)
      {
        DataGridViewRow row = dgv.SelectedRows[0];
        selectedProduct = row.DataBoundItem as ChiTietHoaDonDTO;
        
        if (selectedProduct != null)
        {
          // Update product info panel
          UpdateProductInfoPanel(selectedProduct);
        }
      }
    }
    private void Dgv_CellFormating(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null && e.ColumnIndex >= 0)
      {
        string columnName = dgv.Columns[e.ColumnIndex].DataPropertyName;
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
    private void NudQuantity_ValueChanged(object sender, EventArgs e)
    {
      if (selectedProduct != null)
      {
        UpdateTotalInProductPanel();
      }
    }
    private void UpdateProductInfoPanel(ChiTietHoaDonDTO product)
    {
      if (product == null) return;

      // Update product info labels
      if (tlpProductInfo.Controls.Find("lblProductNameValue", true).FirstOrDefault() is Label lblName)
      {
        lblName.Text = product.TenSP;
      }
      
      if (tlpProductInfo.Controls.Find("lblProductPriceValue", true).FirstOrDefault() is Label lblPrice)
      {
        lblPrice.Text = string.Format("{0:N0} đ", product.Gia);
      }
      
      if (tlpProductInfo.Controls.Find("lblProductDiscountValue", true).FirstOrDefault() is Label lblDiscount)
      {
        lblDiscount.Text = product.KhuyenMai.ToString() + " %";
      }

      if (tlpProductInfo.Controls.Find("lblDiscountAmountValue", true).FirstOrDefault() is Label lblDiscountAmount)
      {
        lblDiscountAmount.Text = string.Format("{0:N0} đ", product.SoTienKm);
      }

      if (tlpProductInfo.Controls.Find("lblFinalPriceValue", true).FirstOrDefault() is Label lblFinalPrice)
      {
        lblFinalPrice.Text = string.Format("{0:N0} đ", product.GiaCuoiCung);
      }

      // Reset quantity to 1 and update total
      nudQuantity.Value = 1;
      UpdateTotalInProductPanel();
    }
    private void UpdateTotalInProductPanel()
    {
      if (selectedProduct == null) return;

      decimal quantity = nudQuantity.Value;
      decimal total = selectedProduct.GiaCuoiCung * quantity;

      if (tlpProductInfo.Controls.Find("lblTotalValue", true).FirstOrDefault() is Label lblTotal)
      {
        lblTotal.Text = string.Format("{0:N0} đ", total);
      }
    }
  }
}
