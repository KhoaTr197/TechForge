using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeGUI.BaseForms;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseControls;
using TechForgeGUI.SubPages;

namespace TechForgeGUI
{
  public partial class ProductManagePageGUI : ManagePage
  {
    private DataSet ds = new DataSet();
    private List<SanPhamDTO> dsSanPham { get; set; }
    private List<DanhMucDTO> dsDanhMuc { get; set; }
    private List<HangSanXuatDTO> dsHangSanXuat { get; set; }
    private List<NhaCungCapDTO>  dsNhaCungCap { get; set; }
    private SanPhamBUS sanPhamBus { get; set; }
    private HangSanXuatBUS hangSanXuatBus { get; set; }
    private DanhMucBUS danhMucBus { get; set; }
    private NhaCungCapBUS nhaCungCapBus { get; set; }

    // Constructor
    public ProductManagePageGUI()
    {
      InitializeComponent();
      InitializeBUS();
      GetData();
      LoadData();
      ModifyData();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;
    }

    // Initialize business logic components
    sealed protected override void InitializeBUS()
    {
      sanPhamBus = new SanPhamBUS(this.connStr);
      hangSanXuatBus = new HangSanXuatBUS(this.connStr);
      danhMucBus = new DanhMucBUS(this.connStr);
      nhaCungCapBus = new NhaCungCapBUS(this.connStr);
    }

    // Retrieve data from the database
    protected void GetData()
    {
      ds = new DataSet();

      sanPhamBus.GetAllDisconnected(ds);
      hangSanXuatBus.GetAllDisconnected(ds);
      danhMucBus.GetAllDisconnected(ds);
      nhaCungCapBus.GetAllDisconnected(ds);

      dsSanPham = new List<SanPhamDTO>();
      dsDanhMuc = new List<DanhMucDTO>();
      dsHangSanXuat = new List<HangSanXuatDTO>();
            dsNhaCungCap = new List<NhaCungCapDTO>();

      // Map data to DTOs
      dsSanPham = ds.Tables["SANPHAM"].AsEnumerable().Select(row => new SanPhamDTO()
      {
        MaSP = row.Field<int>("MASP"),
        TenSP = row.Field<string>("TENSP"),
        GiaNhap = row.Field<decimal>("GIANHAP"),
        Gia = row.Field<decimal>("GIA"),
        KhuyenMai = row.Field<decimal>("KHUYENMAI"),
        MoTa = row.Field<string>("MOTA"),
        SoLuong = row.Field<int>("SL"),
        DonViTinh = row.Field<string>("DONVITINH"),
        HinhAnh = row.Field<string>("HINHANH"),
        DanhMuc = row.Field<int>("DANHMUC"),
        Hsx = row.Field<int>("HSX"),
        Ncc = row.Field<int>("NCC"),
        NgSx = row.Field<DateTime>("NGSX"),
        TrangThai = row.Field<bool>("TRANGTHAI")
      }).ToList();

      dsDanhMuc = ds.Tables["DANHMUC"].AsEnumerable().Select(row => new DanhMucDTO()
      {
        MaDM = row.Field<int>("MADM"),
        TenDM = row.Field<string>("TENDM")
      }).ToList();

      dsHangSanXuat = ds.Tables["HANGSANXUAT"].AsEnumerable().Select(row => new HangSanXuatDTO()
      {
        MaHSX = row.Field<int>("MAHSX"),
        TenHSX = row.Field<string>("TENHSX")
      }).ToList();
        dsNhaCungCap = ds.Tables["NHACUNGCAP"].AsEnumerable().Select(row => new NhaCungCapDTO()
        {
            MaNCC = row.Field<int>("MANCC"),
            TenNCC = row.Field<string>("TENNCC"),
            Ndd = row.Field<string>("NDD"),
            Sdt = row.Field<string>("SDT"),
            Email = row.Field<string>("EMAIL"),
            TrangThai = row.Field<bool>("TRANGTHAI")
        }).ToList();
    }

    // Load data into the DataGridView
    sealed protected override void LoadData()
    {
      dgvMainList.BindingData(dsSanPham);
      
      // Add summary cards with product statistics
      AddSummaryCards(new SummaryCard[] { 
        new SummaryCard("Tổng sản phẩm", dsSanPham.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Sắp hết hàng", GetLowStockCount().ToString(), "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Giá trị kho", GetTotalInventoryValue().ToString("N0") + " đ", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
    
    // Calculate number of products with low stock (less than 10 items)
    private int GetLowStockCount()
    {
      return dsSanPham.Count(p => p.SoLuong < 10 && p.TrangThai);
    }
    
    // Calculate total inventory value (quantity * price for all products)
    private decimal GetTotalInventoryValue()
    {
      return dsSanPham.Where(p => p.TrangThai)
                       .Sum(p => p.SoLuong * p.Gia);
    }

    // Modify DataGridView columns
    private void ModifyData()
    {
      this.SuspendLayout();

      // Add columns to DataGridView
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MASP",
        DataPropertyName = "MaSP",
        HeaderText = "Mã",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TENSP",
        DataPropertyName = "TenSP",
        HeaderText = "Tên sản phẩm",
        FillWeight = 240,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "GIANHAP",
        DataPropertyName = "GiaNhap",
        HeaderText = "Giá nhập"
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "GIA",
        DataPropertyName = "Gia",
        HeaderText = "Giá"
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "KHUYENMAI",
        DataPropertyName = "KhuyenMai",
        HeaderText = "Khuyến mãi (%)",
        FillWeight = 64,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MOTA",
        DataPropertyName = "MoTa",
        HeaderText = "Mô tả",
        FillWeight = 64,
        Visible = false,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "SL",
        DataPropertyName = "SoLuong",
        HeaderText = "Số lượng",
        FillWeight = 64,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
          Name = "DONVITINH",
          DataPropertyName = "DonViTinh",
          HeaderText = "Đơn vị tính",
          FillWeight = 64,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewComboBoxColumn
      {
        Name = "DANHMUC",
        DataPropertyName = "DanhMuc",
        HeaderText = "Danh mục",
        DataSource = dsDanhMuc,
        DisplayMember = "TENDM",
        ValueMember = "MADM",
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewComboBoxColumn
      {
        Name = "HANGSANXUAT",
        DataPropertyName = "Hsx",
        HeaderText = "Hãng sản xuất",
        DataSource = dsHangSanXuat,
        DisplayMember = "TENHSX",
        ValueMember = "MAHSX",
      });
        dgvMainList.dgvList.Columns.Add(new DataGridViewComboBoxColumn
        {
            Name = "NHACUNGCAP",
            DataPropertyName = "Ncc",
            HeaderText = "Nhà cung cấp",
            DataSource = dsNhaCungCap,
            DisplayMember = "TENNCC",
            ValueMember = "MANCC",
        });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "NGSX",
        DataPropertyName = "Ngsx",
        HeaderText = "Ngày sản xuất",
        Visible = false,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TRANGTHAI",
        DataPropertyName = "TrangThai",
        HeaderText = "Trạng Thái"
      });

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }

    // Format DataGridView cells
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TRANGTHAI")
        {
          bool status = (bool)e.Value;
          if (status)
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Green;
            e.Value = "Đang kinh doanh";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Red;
            e.Value = "Ngừng kinh doanh";
          }
        }
        else if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "GIANHAP" ||
                 dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "GIA")
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
          SanPhamDTO sanPham = dsSanPham.Find(sp => sp.MaSP == (int)selectedRow.Cells[0].Value);

          ProductDetailFormGUI detailsForm = new ProductDetailFormGUI(sanPham, dsDanhMuc, dsHangSanXuat, dsNhaCungCap, sanPhamBus);

          detailsForm.Show(Form.ActiveForm);

          // Assign event handler for submits
          detailsForm.AddSubmit += DetailsForm_AddSubmit;
          detailsForm.EditSubmit += DetailsForm_EditSubmit;
        }
      }
    }
    // Handle add submit event
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when new products are added
      UpdateSummaryCards(new SummaryCard[]
      {
        new SummaryCard("Tổng sản phẩm", dsSanPham.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Sắp hết hàng", GetLowStockCount().ToString(), "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Giá trị kho", GetTotalInventoryValue().ToString("N0") + " đ", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
    // Handle edit submit event
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when products are edited
      UpdateSummaryCards(new SummaryCard[]
      {
        new SummaryCard("Tổng sản phẩm", dsSanPham.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Sắp hết hàng", GetLowStockCount().ToString(), "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Giá trị kho", GetTotalInventoryValue().ToString("N0") + " đ", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
  }
}
