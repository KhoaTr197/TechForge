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
    private LichSuKhoDTO thongTinLichSu { get; set; }
    private LichSuKhoBUS bus { get; set; }
    private SanPhamBUS busSanPham { get; set; }
    private List<SanPhamDTO> dsSanPham { get; set; }
    private TableLayoutPanel mainLayout;
    private TableLayoutPanel tlpInfo;
    private Panel pnlGrid;
    private Panel pnlRight;
    private DataGridView dgv;
    private TextBox txtSearch;
    private TableLayoutPanel tlpProductInfo;
    private Label lblProductName;
    private Label lblProductNameValue;
    private Label lblProductPrice;
    private Label lblProductPriceValue;
    private Label lblProductTotal;
    private Label lblProductTotalValue;
    private Label lblProductStock;
    private NumericUpDown nudQuantity;
    private Button btnAddToLog;
    private Button btnUpdateToLog;
    private ChiTietLichSuKhoDTO selectedProduct;
    private ListBox lstSearchResults;

    public ImportExportDetailFormGUI(LichSuKhoBUS _bus, SanPhamBUS _busSanPham, LichSuKhoDTO _thongTinLichSu = null)
    {
      InitializeComponent();

      this.thongTinLichSu = _thongTinLichSu;
      this.bus = _bus;
      this.busSanPham = _busSanPham;
      this.Text = "Chi tiết lịch sử";
      this.Size = new Size(1150, 700);
      this.MinimumSize = new Size(1000, 600);

      InitializeMainLayout();
      GetData();
      InitializeDetailList();
      InitializeDataGridView();
      InitializeInfoPanel();

      // Add panels to main layout with new structure
      mainLayout.Controls.Add(tlpInfo, 0, 0);
      mainLayout.Controls.Add(pnlGrid, 0, 1);
      mainLayout.Controls.Add(pnlRight, 1, 0);
      mainLayout.SetRowSpan(pnlRight, 2);
      this.Controls.Add(mainLayout);

      Dictionary<string, string> inputLabels = new Dictionary<string, string>
        {
        { "MaLS", "Mã LS" },
        { "TongTien", "Tổng Tiền" },
        { "ThoiGian", "Thời Gian" },
        { "MaND", "Nhân Viên Phụ Trách" },
        { "HoatDong", "Hoạt Động" },
      };

      if (thongTinLichSu == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;

        thongTinLichSu = new LichSuKhoDTO
        {
          MaLS = bus.GetNextId(),
          HoatDong = true,
          TongTien = 0,
          ThoiGian = DateTime.Now,
          Ctlsk = new List<ChiTietLichSuKhoDTO>()
        };

        LoadAddForm(inputLabels);

        btnAdd.Click += btnAdd_Click;
      }
      else
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;

        LoadDetailForm(inputLabels);
      }

      dgv.DataSource = thongTinLichSu.Ctlsk;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      thongTinLichSu.TongTien = ((NumericUpDown)GetControlByName(tlpInfo, "nudTongTien")).Value;
      thongTinLichSu.MaND = ((TextBox)GetControlByName(tlpInfo, "txtMaND")).Text;
      thongTinLichSu.HoatDong = ((ComboBox)GetControlByName(tlpInfo, "cboHoatDong")).SelectedItem == "Xuất";

      if (bus.Add(thongTinLichSu) != -1)
        OnAddSubmit(new DetailFormAddSubmitEventArgs(this));
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
    private void InitializeMainLayout()
    {
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
      btnAddToLog.Enabled = true;
      btnAddToLog.BackColor = Color.FromArgb(0, 123, 255);

      btnUpdateToLog.Enabled = false;
      btnUpdateToLog.BackColor = Color.Gray;

      var selectedItem = lstSearchResults.SelectedItems[0].ToString().ToLower();

      var filteredResult = dsSanPham
       .Find(sp => selectedItem.Contains(sp.MaSP.ToString()) && selectedItem.Contains(sp.TenSP.ToString().ToLower()));

      selectedProduct = new ChiTietLichSuKhoDTO
      {
        MaSP = filteredResult.MaSP,
        HinhAnh = filteredResult.HinhAnh,
        TenSP = filteredResult.TenSP,
        Gia = filteredResult.Gia,
        SoLuong = 1,
        HoatDong = thongTinLichSu.HoatDong,
        ThanhTien = filteredResult.Gia * 1
      };

      nudQuantity.Value = 1;

      UpdateProductInfoPanel(selectedProduct);
    }

    private void btnAddToLog_Click(object sender, EventArgs e)
    {
      if (selectedProduct != null)
      {
        var newDetailList = thongTinLichSu.Ctlsk.ToList();

        if (newDetailList.Any(sp => sp.MaSP == selectedProduct.MaSP))
        {
          MessageBox.Show("Sản phẩm đã có trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        selectedProduct.SoLuong = (int)nudQuantity.Value;
        selectedProduct.ThanhTien = selectedProduct.Gia * (int)nudQuantity.Value;
        newDetailList.Add(selectedProduct);

        ((NumericUpDown)GetControlByName(tlpInfo, "nudTongTien")).Value += (decimal)selectedProduct.ThanhTien;

        dgv.DataSource = newDetailList;
        thongTinLichSu.Ctlsk = newDetailList;

        btnAddToLog.Enabled = false;
        btnAddToLog.BackColor = Color.Gray;
        btnUpdateToLog.Enabled = true;
        btnUpdateToLog.BackColor = Color.Orange;
      }
    }
    private void btnUpdateToLog_Click(object sender, EventArgs e)
    {
      var newDetailList = thongTinLichSu.Ctlsk.ToList();
      newDetailList.ForEach(sp =>
      {
        if (sp.MaSP == selectedProduct.MaSP)
        {
          sp.SoLuong = (int)nudQuantity.Value;
          sp.ThanhTien = sp.Gia * sp.SoLuong;
        }
      });
      dgv.DataSource = newDetailList;

      btnUpdateToLog.Enabled = false;
      btnUpdateToLog.BackColor = Color.Gray;
    }
    private void LoadAddForm(Dictionary<string, string> inputLabels)
    {
      int row = 0;
      int col = 0;

      foreach (KeyValuePair<string, string> kvp in inputLabels)
      {
        string propName = kvp.Key;
        string inputLabel = kvp.Value;

        Label lbl = new Label
        {
          Text = inputLabel + ":",
          AutoSize = true,
          Anchor = AnchorStyles.Left | AnchorStyles.Top,
          Margin = new Padding(3, 8, 3, 3),
          Font = new Font(DefaultFontName, 12)
        };

        Control control;
        if (propName == "Ctlsk") continue;
        else if (propName == "MaLS")
        {
          control = new TextBox
          {
            Name = "txt" + propName,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Text = thongTinLichSu?.GetType().GetProperty(propName)?.GetValue(thongTinLichSu)?.ToString(),
            Enabled = false,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else if (propName == "TongTien")
        {
          control = new NumericUpDown
          {
            Name = "nud" + propName,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            ThousandsSeparator = true,
            Minimum = 0,
            Maximum = 1000000000,
            Value = 0,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30,
            ReadOnly = true,
            Increment = 0,
          };
        }
        else if (propName == "HoatDong")
        {
          ComboBox comboBox = new ComboBox
          {
            Name = "cbo" + propName,
            Font = new Font(DefaultFontName, 12),
            Width = 320,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30,
            DropDownStyle = ComboBoxStyle.DropDownList
          };

          comboBox.Items.AddRange(new string[] { "Nhập", "Xuất" });
          comboBox.SelectedItem = thongTinLichSu != null && (bool)thongTinLichSu.GetType().GetProperty(propName)?.GetValue(thongTinLichSu) ? "Xuất" : "Nhập";

          comboBox.ValueMemberChanged += (s, e) =>
          {
            if (comboBox.SelectedItem != null)
            {
              bool isExport = comboBox.SelectedItem.ToString() == "Xuất";
              thongTinLichSu.HoatDong = isExport;
            }
          };

          control = comboBox;
        }
        else if (propName == "ThoiGian")
        {
          control = new DateTimePicker
          {
            Name = "dtp" + propName,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = DateTime.Today,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else if (propName == "ThoiGian")
        {
          control = new NumericUpDown
          {
            Name = "nud" + propName,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            ThousandsSeparator = true,
            Minimum = 0,
            Maximum = 1000000000,
            Value = 0,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + propName,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Text = thongTinLichSu?.GetType().GetProperty(propName)?.GetValue(thongTinLichSu)?.ToString(),
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }

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

      while (tlpInfo.RowStyles.Count < tlpInfo.RowCount)
      {
        tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
      }
    }
    private void LoadDetailForm(Dictionary<string, string> inputLabels)
    {
      int row = 0;
      int col = 0;

      foreach (var prop in thongTinLichSu.GetType().GetProperties())
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
        if (prop.Name == "Ctlsk") continue;
        else if (prop.Name == "MaLS")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinLichSu)?.ToString(),
            Enabled = false,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30
          };
        }
        else if (prop.Name == "TongTien")
        {
          control = new NumericUpDown
          {
            Name = "nud" + prop.Name,
            Dock = DockStyle.Fill,
            Font = new Font(DefaultFontName, 12),
            ThousandsSeparator = true,
            Minimum = 0,
            Maximum = 1000000000,
            Value = Convert.ToDecimal(prop.GetValue(thongTinLichSu)),
            Margin = new Padding(3, 5, 15, 3),
            Height = 30,
            ReadOnly = true,
            Increment = 0,
          };
        }
        else if (prop.Name == "HoatDong")
        {
          ComboBox comboBox = new ComboBox
          {
            Name = "cbo" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Width = 320,
            Margin = new Padding(3, 5, 15, 3),
            Height = 30,
            DropDownStyle = ComboBoxStyle.DropDownList
          };

          comboBox.Items.AddRange(new string[] { "Nhập", "Xuất" });
          comboBox.SelectedItem = (bool)prop.GetValue(thongTinLichSu) ? "Xuất" : "Nhập";

          control = comboBox;
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
            Value = (DateTime)prop.GetValue(thongTinLichSu),
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
            Value = Convert.ToDecimal(prop.GetValue(thongTinLichSu)),
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
            Text = prop.GetValue(thongTinLichSu)?.ToString(),
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
      // Grid panel
      pnlGrid = new Panel
      {
        Dock = DockStyle.Fill,
        Margin = new Padding(5)
      };

      dgv = new DataGridView
      {
        Dock = DockStyle.Fill,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        AutoGenerateColumns = false,
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
        ColumnHeadersHeight = 64
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
          DataPropertyName =
          "TenSP",
          HeaderText = "Tên SP",
          Width = 75
        },
        new DataGridViewTextBoxColumn {
          Name = "Gia",
          DataPropertyName = "Gia",
          HeaderText = "Giá",
          Width = 100,
        },
        new DataGridViewTextBoxColumn {
          Name = "SoLuong",
          DataPropertyName = "SoLuong",
          HeaderText = "Số Lượng",
          Width = 190
        },
        new DataGridViewTextBoxColumn {
          Name = "ThanhTien",
          DataPropertyName = "ThanhTien",
          HeaderText = "Thành Tiền",
          Width = 110
        },
      });

      // Format currency columns
      dgv.CellFormatting += Dgv_CellFormating;

      // Handle row selection
      dgv.SelectionChanged += Dgv_SelectionChanged;

      pnlGrid.Controls.Add(dgv);

    }
    private void nudQuantity_ValueChanged(object sender, EventArgs e)
    {
      UpdateProductInfoPanel(selectedProduct);
    }

    private void Dgv_SelectionChanged(object sender, EventArgs e)
    {
      if (dgv.SelectedRows.Count > 0)
      {
        DataGridViewRow row = dgv.SelectedRows[0];
        selectedProduct = row.DataBoundItem as ChiTietLichSuKhoDTO;

        if (selectedProduct != null)
        {
          btnAddToLog.Enabled = false;
          btnAddToLog.BackColor = Color.Gray;

          btnUpdateToLog.Enabled = true;
          btnUpdateToLog.BackColor = Color.Orange;

          // Update product info panel
          UpdateProductInfoPanel(selectedProduct);

          if (nudQuantity != null)
          {
            nudQuantity.Value = Convert.ToDecimal(selectedProduct.SoLuong);
          }
        }
      }
    }
    private void Dgv_CellFormating(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null && e.ColumnIndex >= 0)
      {
        string columnName = dgv.Columns[e.ColumnIndex].DataPropertyName;
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
    private void UpdateProductInfoPanel(ChiTietLichSuKhoDTO product)
    {
      if (product == null) return;

      // Update product info labels
      if (lblProductNameValue != null)
      {
        lblProductNameValue.Text = product.TenSP;
      }

      if (lblProductPriceValue != null)
      {
        lblProductPriceValue.Text = string.Format("{0:N0} đ", product.Gia);
      }

      if (lblProductTotalValue != null)
      {
        lblProductTotalValue.Text = string.Format("{0:N0} đ", product.Gia * nudQuantity.Value);
      }

    }
  }
}
