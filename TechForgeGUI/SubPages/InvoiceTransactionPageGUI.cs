using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;

namespace TechForgeGUI.SubPages
{
  public partial class InvoiceTransactionPageGUI : UserControl
  {
    // Table layout panel
    private TableLayoutPanel tlpMain;
    
    // Left panel - search & product list
    private TextBox txtProductSearch;
    private Button btnProductSearch;
    private DataGridView dgvProducts;
    private DataGridView dgvInvoiceItems;
    private Button btnAddToInvoice;
    
    // Right panel - customer info & invoice total
    private TextBox txtCustomerSearch;
    private ListBox lstCustomerResults;
    private TextBox txtCustomerName;
    private TextBox txtCustomerPhone;
    private TextBox txtCustomerAddress;
    private NumericUpDown nudCustomerCashPaid;

    // Invoice summary
    private Label lblSubtotalValue;
    private Label lblDiscountValue;
    private Label lblTotalValue;
    private Label lblCashTakenValue;
    private Label lblCustomerChangeGivenValue;
    private Button btnCreateInvoice;
    
    // BUS objects
    private SanPhamBUS sanPhamBUS;
    private HoiVienBUS hoiVienBUS;
    private HoaDonBUS hoaDonBUS;
    private LichSuHoatDongBUS lichSuHoatDongBUS;
    private readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";

    // Lists to store data
    private List<SanPhamDTO> dsSanPham;
    private List<HoiVienDTO> dsHoiVien;
        private HoiVienDTO selectedCustomer;
        private HoaDonDTO newHoaDon;
    private List<SanPhamDTO> dsCTHD; // Items added to invoice
        private List<ChiTietHoaDonDTO> dsChiTietHoaDon;
    
    private NguoiDungDTO currentUser;
    
    public InvoiceTransactionPageGUI(NguoiDungDTO _currentUser)
    {
      InitializeComponent();
      InitializeBUS();
      GetData();
      InitalizeLayout();
      InitializeProductPanel();
      InitializeCustomerPanel();
      InitializeInvoiceItemsGrid();
        
      this.Dock = DockStyle.Fill;
      this.Font = new Font("Segoe UI", 10);

      this.currentUser = _currentUser;
    }
    
    // Initialize BUS
    private void InitializeBUS()
    {
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
      selectedCustomer = new HoiVienDTO();
      dsCTHD = new List<SanPhamDTO>();
      newHoaDon = new HoaDonDTO();
      dsChiTietHoaDon = new List<ChiTietHoaDonDTO>();
    }

    private void InitalizeLayout()
    {
      tlpMain = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 1,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 70F),
          new ColumnStyle(SizeType.Percent, 30F)
        },
        RowStyles = {
          new RowStyle(SizeType.Percent, 100F)
        }
      };
      
      this.Controls.Add(tlpMain);
    }
    
    private void InitializeProductPanel()
    {
      // Create main panel for the left side
      TableLayoutPanel tlpLeft = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(8),
        ColumnCount = 1,
        RowCount = 2,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 100F),
        },
        RowStyles = {
          new RowStyle(SizeType.Percent, 40F),
          new RowStyle(SizeType.Percent, 60F),
        },
      };

      // Product search panel
      TableLayoutPanel tlpProductSearch = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 4,
        ColumnStyles = {
          new ColumnStyle(SizeType.AutoSize),
          new ColumnStyle(SizeType.AutoSize),
        },
        RowStyles = {
          new RowStyle(SizeType.Absolute, 32),
          new RowStyle(SizeType.Absolute, 48),
          new RowStyle(SizeType.Percent, 100F),
          new RowStyle(SizeType.Absolute, 48),
        },
      };

      Label lblProductSearch = new Label()
      {
        Text = "Tìm kiếm sản phẩm",
        Dock = DockStyle.Fill,
        Height = 30,
        Font = new Font(this.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Underline),
        TextAlign = ContentAlignment.MiddleLeft,
      };

      txtProductSearch = new TextBox()
      {
        Location = new Point(10, 20),
        Size = new Size(300, 48),
        Font = new Font(this.Font.FontFamily, 12),
      };
      
      btnProductSearch = new Button()
      {
        Text = "Tìm kiếm",
        Size = new Size(100, 32),
        Font = new Font(this.Font.FontFamily, 12),
      };
      btnProductSearch.Click += BtnProductSearch_Click;
      
      dgvProducts = new DataGridView()
      {
        Dock = DockStyle.Fill,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        ColumnHeadersHeight = 32,
        ReadOnly = true,
        AllowUserToAddRows = false,
        Font = new Font(this.Font.FontFamily, 10),
      };

      FlowLayoutPanel flpButtons = new FlowLayoutPanel()
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        FlowDirection = FlowDirection.LeftToRight,
      };

      btnAddToInvoice = new Button()
      {
        Size = new Size(160, 32),
        Margin = new Padding(0),
        Text = "Thêm vào hóa đơn",
        Font = new Font(this.Font.FontFamily, 12)
      };
      btnAddToInvoice.Click += btnAddToInvoice_Click;

      // Add to buttons panel
      flpButtons.Controls.Add(btnAddToInvoice);

      // Invoice items panel title
      TableLayoutPanel tlpInvoiceItems = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 1,
        RowCount = 2,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 100F),
        },
        RowStyles = {
          new RowStyle(SizeType.Absolute, 32),
          new RowStyle(SizeType.AutoSize, 100F),
        },
      };

      Label lblInvoiceItems = new Label()
      {
        Text = "Sản phẩm trong hóa đơn",
        Dock = DockStyle.Top,
        Height = 30,
        Font = new Font(this.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Underline),
        TextAlign = ContentAlignment.MiddleLeft,
      };
      
      // Invoice items grid
      dgvInvoiceItems = new DataGridView()
      {
        Dock = DockStyle.Fill,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        ColumnHeadersHeight = 32,
        RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders,
        ReadOnly = true,
        AllowUserToAddRows = false,
        Font = new Font(this.Font.FontFamily, 10),
      };

      // Add controls
      tlpProductSearch.Controls.Add(lblProductSearch, 0, 0);
      tlpProductSearch.SetColumnSpan(lblProductSearch, 2);
      tlpProductSearch.Controls.Add(txtProductSearch, 0, 1);
      tlpProductSearch.Controls.Add(btnProductSearch, 1, 1);

      tlpProductSearch.Controls.Add(dgvProducts, 0, 2);
      tlpProductSearch.SetColumnSpan(dgvProducts, 2);

      tlpProductSearch.Controls.Add(flpButtons, 0, 3);
      tlpProductSearch.SetColumnSpan(flpButtons, 2);

      tlpInvoiceItems.Controls.Add(lblInvoiceItems, 0, 0);
      tlpInvoiceItems.Controls.Add(dgvInvoiceItems, 0, 1);

      tlpLeft.Controls.Add(tlpProductSearch, 0, 0);
      tlpLeft.Controls.Add(tlpInvoiceItems, 0, 1);

      // Bind data to product grid
      BindProductsData(); 
      
      // Add to main layout
      tlpMain.Controls.Add(tlpLeft, 0, 0);
    }
    
    private void InitializeCustomerPanel()
    {
      // Customer info panel
      TableLayoutPanel pnlRight = new TableLayoutPanel()
      {
        AutoSize = true,
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(8),
        ColumnCount = 1,
        RowCount = 4,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 100),
        },
        RowStyles = {
          new RowStyle(SizeType.Absolute, 32),
          new RowStyle(SizeType.Percent, 25F),
          new RowStyle(SizeType.Percent, 37.5F),
          new RowStyle(SizeType.Percent, 37.5F),
        },
      };

      // Customer title
      Label lblCustomerTitle = new Label()
      {
        Text = "Thông tin hóa đơn",
        Dock = DockStyle.Top,
        Height = 30,
        Font = new Font(this.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Underline),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      // Customer search panel
      TableLayoutPanel tlpCustomerSearchPanel = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 1,
        RowCount = 3,
        ColumnStyles = {
          new ColumnStyle(SizeType.AutoSize),
        },
        RowStyles = {
          new RowStyle(SizeType.Absolute, 32),
          new RowStyle(SizeType.Absolute, 32),
          new RowStyle(SizeType.Percent, 100),
        },
      };

      Label lblCustomerSearch = new Label()
      {
        Text = "Tìm kiếm khách hàng:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      txtCustomerSearch = new TextBox()
      {
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        Text = "Nhập tên hoặc số điện thoại..."
      };
      txtCustomerSearch.TextChanged += txtCustomerSearch_TextChanged;

      lstCustomerResults = new ListBox()
      {
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        BorderStyle = BorderStyle.FixedSingle,
        BackColor = Color.LightGray,
        Visible = true
      };
      lstCustomerResults.SelectedIndexChanged += lstCustomerResults_SelectedIndexChanged;

      // Customer info 
      TableLayoutPanel tlpCustomerInfoPanel = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 5,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 30F),
          new ColumnStyle(SizeType.Percent, 70F),
        },
        RowStyles = {
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
        },
      };

      Label lblCustomerName = new Label()
      {
        Text = "Họ tên:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      txtCustomerName = new TextBox()
      {
        Dock = DockStyle.Fill,
        Margin = new Padding(0, 8, 0, 8),
        Font = new Font(this.Font.FontFamily, 12)
      };

      Label lblCustomerPhone = new Label()
      {
        Text = "Số điện thoại:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      txtCustomerPhone = new TextBox()
      {
        Dock = DockStyle.Fill,
        Margin = new Padding(0, 8, 0, 8),
        Font = new Font(this.Font.FontFamily, 12)
      };

      Label lblCustomerAddress = new Label()
      {
        Text = "Địa chỉ:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.TopLeft,
      };

      txtCustomerAddress = new TextBox()
      {
        Dock = DockStyle.Fill,
        Height = 64,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        Multiline = true,
      };

      Label lblCustomerCashPaid = new Label()
      {
        Text = "Khách đưa:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 10),
        Margin = new Padding(0, 10, 0, 8),
        TextAlign = ContentAlignment.TopLeft,
      };

      nudCustomerCashPaid = new NumericUpDown()
      {
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        Minimum = 0,
        ThousandsSeparator = true,
        DecimalPlaces = 0,
        Increment = 1000000,
        Maximum = 1000000000,
      };
      nudCustomerCashPaid.ValueChanged += nudCustomerCashPaid_ValueChanged;

      // Invoice info 
      TableLayoutPanel tlpInvoiceInfoPanel = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 6,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 40F),
          new ColumnStyle(SizeType.Percent, 60F),
        },
        RowStyles = {
          new RowStyle(SizeType.Percent, 100F),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.AutoSize),
        },
      };

      Label lblSubtotal = new Label()
      {
        Text = "Tổng tiền hàng:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      lblSubtotalValue = new Label()
      {
        Text = "0 đ",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 14),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleRight
      };

      Label lblDiscount = new Label()
      {
        Text = "Giảm giá:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      lblDiscountValue = new Label()
      {
        Text = "0 đ",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 14),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleRight
      };

      Label lblTotal = new Label()
      {
        Text = "Thành tiền:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleLeft
      };
      
      lblTotalValue = new Label()
      {
        Text = "0 đ",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 14),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleRight
      };

      Label lblCashTaken = new Label()
      {
        Text = "Tiền nhận:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.TopLeft,
      };

      lblCashTakenValue = new Label()
      {
        Text = "0 đ",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 14),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleRight
      };

      Label lblCustomerChangeGiven = new Label()
      {
        Text = "Tiền Thừa:",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 12),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleLeft
      };

      lblCustomerChangeGivenValue = new Label()
      {
        Text = "0 đ",
        Dock = DockStyle.Fill,
        Font = new Font(this.Font.FontFamily, 14),
        Margin = new Padding(0, 8, 0, 8),
        TextAlign = ContentAlignment.MiddleRight
      };

      btnCreateInvoice = new Button()
      {
        Text = "Tạo hóa đơn",
        Dock = DockStyle.Bottom,
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.FromArgb(0, 120, 215),
        ForeColor = Color.White,
        Height = 48,
        Font = new Font(this.Font.FontFamily, 14, FontStyle.Bold),
      };
      btnCreateInvoice.Click += BtnCreateInvoice_Click;

      // Add controls to customer panel
      pnlRight.Controls.Add(lblCustomerTitle, 0, 0);

      // Customer search
      tlpCustomerSearchPanel.Controls.Add(lblCustomerSearch, 0, 0);
      tlpCustomerSearchPanel.Controls.Add(txtCustomerSearch, 0, 1);
      tlpCustomerSearchPanel.Controls.Add(lstCustomerResults, 0, 2);

      pnlRight.Controls.Add(tlpCustomerSearchPanel, 0, 1);

      // Customer info
      tlpCustomerInfoPanel.Controls.Add(lblCustomerName, 0, 0);
      tlpCustomerInfoPanel.Controls.Add(txtCustomerName, 1, 0);

      tlpCustomerInfoPanel.Controls.Add(lblCustomerPhone, 0, 1);
      tlpCustomerInfoPanel.Controls.Add(txtCustomerPhone, 1, 1);

      tlpCustomerInfoPanel.Controls.Add(lblCustomerAddress, 0, 2);
      tlpCustomerInfoPanel.Controls.Add(txtCustomerAddress, 1, 2);

      tlpCustomerInfoPanel.Controls.Add(lblCustomerCashPaid, 0, 3);
      tlpCustomerInfoPanel.Controls.Add(nudCustomerCashPaid, 1, 3);

      pnlRight.Controls.Add(tlpCustomerInfoPanel, 0, 2);

      // Invoice info
      tlpInvoiceInfoPanel.Controls.Add(lblSubtotal, 0, 1);
      tlpInvoiceInfoPanel.Controls.Add(lblSubtotalValue, 1, 1);

      tlpInvoiceInfoPanel.Controls.Add(lblDiscount, 0, 2);
      tlpInvoiceInfoPanel.Controls.Add(lblDiscountValue, 1, 2);

      tlpInvoiceInfoPanel.Controls.Add(lblTotal, 0, 3);
      tlpInvoiceInfoPanel.Controls.Add(lblTotalValue, 1, 3);

      tlpInvoiceInfoPanel.Controls.Add(lblCashTaken, 0, 4);
      tlpInvoiceInfoPanel.Controls.Add(lblCashTakenValue, 1, 4);

      tlpInvoiceInfoPanel.Controls.Add(lblCustomerChangeGiven, 0, 5);
      tlpInvoiceInfoPanel.Controls.Add(lblCustomerChangeGivenValue, 1, 5);

      tlpInvoiceInfoPanel.Controls.Add(btnCreateInvoice, 0, 6);
      tlpInvoiceInfoPanel.SetColumnSpan(btnCreateInvoice, 2);

      pnlRight.Controls.Add(tlpInvoiceInfoPanel, 0, 3);

      // Add to main layout
      tlpMain.Controls.Add(pnlRight, 1, 0);
    }
    
    private void BindProductsData()
    {
      dgvProducts.DataSource = null;
      dgvProducts.Columns.Clear();

      dgvProducts.DataSource = dsSanPham.Select(p => new
      {
        MaSP = p.MaSP,
        TenSP = p.TenSP,
        Gia = p.Gia,
        KhuyenMai = p.KhuyenMai,
        SoLuong = p.SoLuong
      }).ToList();

      dgvProducts.DataBindingComplete += dgvProducts_DataBindingComplete;
    }
    private void dgvProducts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      dgvProducts.Columns["MaSP"].HeaderText = "Mã SP";
      dgvProducts.Columns["TenSP"].HeaderText = "Tên sản phẩm";
      dgvProducts.Columns["Gia"].HeaderText = "Giá";
      dgvProducts.Columns["Gia"].DefaultCellStyle.Format = "C0";
      dgvProducts.Columns["Gia"].DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
      dgvProducts.Columns["KhuyenMai"].HeaderText = "Khuyến mãi (%)";
      dgvProducts.Columns["SoLuong"].HeaderText = "Tồn kho";

    }
    private void InitializeInvoiceItemsGrid()
    {
      // Add columns
      dgvInvoiceItems.Columns.Add("MaSP", "Mã SP");
      dgvInvoiceItems.Columns.Add("TenSP", "Tên sản phẩm");
      dgvInvoiceItems.Columns.Add("SoLuong", "Số lượng");
      dgvInvoiceItems.Columns.Add("Gia", "Đơn giá");
      dgvInvoiceItems.Columns["Gia"].DefaultCellStyle.Format = "C0";
      dgvInvoiceItems.Columns["Gia"].DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
      dgvInvoiceItems.Columns.Add("KhuyenMai", "Khuyến mãi (%)");
      dgvInvoiceItems.Columns.Add("ThanhTien", "Thành tiền");
      dgvInvoiceItems.Columns["ThanhTien"].DefaultCellStyle.Format = "C0";
      dgvInvoiceItems.Columns["ThanhTien"].DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
      dgvInvoiceItems.Columns.Add(new DataGridViewButtonColumn()
      {
        Name = "Xoa",
        HeaderText = "Xóa",
        CellTemplate = new DataGridViewButtonCell()
        {
          FlatStyle = FlatStyle.Flat,
        },
        Text = "Xóa",
        
      });

      dgvInvoiceItems.CellContentClick += DgvInvoiceItems_CellContentClick;
    }   
    private void BtnProductSearch_Click(object sender, EventArgs e)
    {
      string searchText = txtProductSearch.Text.Trim().ToLower();
      
      if (string.IsNullOrEmpty(searchText))
      {
        return;
      }
      
      var filteredList = dsSanPham.Where(p => 
        p.TenSP.ToLower().Contains(searchText) || 
        p.MaSP.ToString().Contains(searchText)).ToList();
      
      dgvProducts.DataSource = filteredList.Select(p => new 
      {
        MaSP = p.MaSP,
        TenSP = p.TenSP,
        Gia = p.Gia,
        KhuyenMai = p.KhuyenMai,
        SoLuong = p.SoLuong
      }).ToList();
    }
    
    private void btnAddToInvoice_Click(object sender, EventArgs e)
    {
      if (dgvProducts.SelectedRows.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      
      // Get selected product
      int selectedIndex = dgvProducts.SelectedRows[0].Index;
      int productId = (int)dgvProducts.Rows[selectedIndex].Cells["MaSP"].Value;
      
      SanPhamDTO selectedProduct = dsSanPham.FirstOrDefault(p => p.MaSP == productId);
      
      if (selectedProduct == null) return;
      
      // Check inventory
      if (selectedProduct.SoLuong <= 0)
      {
        MessageBox.Show("Sản phẩm đã hết hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      
      // Check if product already exists in invoice
      int existingRowIndex = -1;
      foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
      {
        if ((int)row.Cells["MaSP"].Value == productId)
        {
          existingRowIndex = row.Index;
          break;
        }
      }
      
      if (existingRowIndex >= 0)
      {
        // Update quantity
        int currentQty = (int)dgvInvoiceItems.Rows[existingRowIndex].Cells["SoLuong"].Value;
        
        if (currentQty >= selectedProduct.SoLuong)
        {
          MessageBox.Show("Không đủ số lượng trong kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        
        dgvInvoiceItems.Rows[existingRowIndex].Cells["SoLuong"].Value = currentQty + 1;
        
        // Update total
        decimal price = (decimal)dgvInvoiceItems.Rows[existingRowIndex].Cells["Gia"].Value;
        decimal discount = (decimal)dgvInvoiceItems.Rows[existingRowIndex].Cells["KhuyenMai"].Value;
        decimal total = (currentQty + 1) * price * (1 - discount / 100);
        
        dgvInvoiceItems.Rows[existingRowIndex].Cells["ThanhTien"].Value = total;
      }
      else
      {
        // Add new row
        int rowIndex = dgvInvoiceItems.Rows.Add(
          selectedProduct.MaSP,
          selectedProduct.TenSP,
          1,
          selectedProduct.Gia,
          selectedProduct.KhuyenMai,
          selectedProduct.Gia * (1 - selectedProduct.KhuyenMai / 100)
        );
        
        // Add to invoice items list
        dsCTHD.Add(selectedProduct);
      }
      
      UpdateInvoiceSummary();
    }
    
    private void DgvInvoiceItems_CellContentClick(object sender, EventArgs e)
    {
      if (dgvInvoiceItems.CurrentCell.ColumnIndex != dgvInvoiceItems.Columns["Xoa"].Index)
      {
        return;
      }

      if (MessageBox.Show("Bạn có chắc chắn xóa sản phẩm khỏi hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      {
        return;
      } 

      int selectedIndex = dgvInvoiceItems.SelectedRows[0].Index;
      int productId = (int)dgvInvoiceItems.Rows[selectedIndex].Cells["MaSP"].Value;
      
      // Remove from list
      SanPhamDTO productToRemove = dsCTHD.FirstOrDefault(p => p.MaSP == productId);
      if (productToRemove != null)
      {
        dsCTHD.Remove(productToRemove);
      }
      
      // Remove from grid
      dgvInvoiceItems.Rows.RemoveAt(selectedIndex);
      
      UpdateInvoiceSummary();
    }
    
    private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
    {
      string searchText = txtCustomerSearch.Text.Trim().ToLower();
      
      if (string.IsNullOrEmpty(searchText))
      {
        lstCustomerResults.Items.Clear();
        return;
      }
      
      // Filter customers based on search text
      var filteredCustomers = dsHoiVien.Where(c => 
        c.HoTen.ToLower().Contains(searchText) || 
        c.Sdt.Contains(searchText)).ToList();
      
      // Update the listbox
      lstCustomerResults.Items.Clear();
      
      if (filteredCustomers.Count > 0)
      {
        foreach (var customer in filteredCustomers)
        {
          lstCustomerResults.Items.Add($"{customer.HoTen} - {customer.Sdt}");
        }
        
        lstCustomerResults.Height = Math.Min(80, filteredCustomers.Count * 20 + 10);
      }
    }
    
    private void lstCustomerResults_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstCustomerResults.SelectedIndex < 0) return;
      
      string selectedCustomerText = lstCustomerResults.SelectedItem.ToString();
      string phoneNumber = selectedCustomerText.Split('-').Last().Trim();
      
      // Find the customer with this phone number
      selectedCustomer = dsHoiVien.FirstOrDefault(c => c.Sdt == phoneNumber);
      
      if (selectedCustomer != null)
      {
        txtCustomerName.Text = selectedCustomer.HoTen;
        txtCustomerPhone.Text = selectedCustomer.Sdt;
        txtCustomerAddress.Text = selectedCustomer.Dchi;
                
        // Set the search text
        txtCustomerSearch.Text = selectedCustomerText;
      }
    }
    private void nudCustomerCashPaid_ValueChanged(object sender, EventArgs e)
    {
      lblCashTakenValue.Text = nudCustomerCashPaid.Value.ToString("C0", new CultureInfo("vi-VN"));

      if (dgvInvoiceItems.Rows.Count == 0)
      {
        return;
      } else
      {
        decimal cashPaid = nudCustomerCashPaid.Value;
        decimal total = 0;
        decimal.TryParse(lblTotalValue.Text, NumberStyles.Currency, new CultureInfo("vi-VN"), out total);

        lblCustomerChangeGivenValue.Text = (cashPaid - total).ToString("C0", new CultureInfo("vi-VN"));
      }
    }


    private void UpdateInvoiceSummary()
    {
      decimal cashPaid = nudCustomerCashPaid.Value;
      decimal subtotal = 0;
      decimal discount = 0;
      
      // Calculate from grid
      foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
      {
        int quantity = (int)row.Cells["SoLuong"].Value;
        decimal price = (decimal)row.Cells["Gia"].Value;
        decimal itemDiscount = (decimal)row.Cells["KhuyenMai"].Value;

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

      txtCustomerSearch.Text = "";
      txtCustomerName.Text = "";
      txtCustomerPhone.Text = "";
      txtCustomerAddress.Text = "";
      selectedCustomer = null;
      UpdateInvoiceSummary();
    }
  }
}
