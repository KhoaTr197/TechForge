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
    private SanPhamBUS sanPhamBus { get; set; }
    private HangSanXuatBUS hangSanXuatBus { get; set; }
    private DanhMucBUS danhMucBus { get; set; }
    public ProductManagePageGUI()
    {
      InitializeComponent();
      InitializeBUS();
      LoadData();

      //dgvMainListRef.dgvList.SelectionChanged += dgvList_SelectionChanged;
      dgvMainListRef.dgvList.CellClick += dgvList_CellClick;
    }
    sealed protected override void InitializeBUS()
    {
      sanPhamBus = new SanPhamBUS(this.connStr);
      hangSanXuatBus = new HangSanXuatBUS(this.connStr);
      danhMucBus = new DanhMucBUS(this.connStr);
    }
    sealed protected override void LoadData()
    {
      DataSet ds = new DataSet();
      sanPhamBus.GetAllDisconnected(ds);
      hangSanXuatBus.GetAllDisconnected(ds);
      danhMucBus.GetAllDisconnected(ds);

      // Bind data to the DataGridView
      dgvMainListRef.BindingData(ds.Tables["SANPHAM"]);

      this.SuspendLayout();

      // Re-modify the column header text
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MASP",
        DataPropertyName = "MASP",
        HeaderText = "Mã",
        FillWeight = 48,
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TENSP",
        DataPropertyName = "TENSP",
        HeaderText = "Tên sản phẩm",
        FillWeight = 240,
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "GIANHAP",
        DataPropertyName = "GIANHAP",
        HeaderText = "Giá nhập"
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "GIA",
        DataPropertyName = "GIA",
        HeaderText = "Giá"
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "KHUYENMAI",
        DataPropertyName = "KHUYENMAI",
        HeaderText = "Khuyến mãi (%)",
        FillWeight = 64,
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MOTA",
        DataPropertyName = "MOTA",
        HeaderText = "Mô tả",
        FillWeight = 64,
        Visible = false,
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "SL",
        DataPropertyName = "SL",
        HeaderText = "Số lượng",
        FillWeight = 64,
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewComboBoxColumn
      {
        Name = "DANHMUC",
        DataPropertyName = "DANHMUC",
        HeaderText = "Danh mục",
        DataSource = ds.Tables["DANHMUC"],
        DisplayMember = "TENDM",
        ValueMember = "MADM",
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewComboBoxColumn
      {
        Name = "HANGSANXUAT",
        DataPropertyName = "HSX",
        HeaderText = "Hãng sản xuất",
        DataSource = ds.Tables["HANGSANXUAT"],
        DisplayMember = "TENHSX",
        ValueMember = "MAHSX",
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "NGSX",
        DataPropertyName = "NGSX",
        HeaderText = "Ngày sản xuất",
        Visible = false,
      });
      dgvMainListRef.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TRANGTHAI",
        DataPropertyName = "TRANGTHAI",
        HeaderText = "Trạng Thái"
      });

      dgvMainListRef.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainListRef.dgvList.Columns[e.ColumnIndex].DataPropertyName == "TRANGTHAI")
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
        else if (dgvMainListRef.dgvList.Columns[e.ColumnIndex].DataPropertyName == "GIANHAP" ||
                 dgvMainListRef.dgvList.Columns[e.ColumnIndex].DataPropertyName == "GIA")
        {
          decimal price = (decimal)e.Value;
          e.Value = price.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
          e.FormattingApplied = true;
        }
      }
    }
    protected void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        DataGridView dgvMainList = (DataGridView)sender;
        if (dgvMainList.SelectedRows.Count > 0)
        {
          using (OverlayFormGUI overlay = new OverlayFormGUI())
          {
            DataGridViewRow selectedRow = dgvMainList.Rows[e.RowIndex];
            SanPhamDTO sanPham = new SanPhamDTO() {
              MaSP = (int)selectedRow.Cells["MASP"].Value,
              TenSP = (string)selectedRow.Cells["TENSP"].Value,
              GiaNhap = (decimal)selectedRow.Cells["GIANHAP"].Value,
              Gia = (decimal)selectedRow.Cells["GIA"].Value,
              KhuyenMai = (decimal)selectedRow.Cells["KHUYENMAI"].Value,
              MoTa = (string)selectedRow.Cells["MOTA"].Value,
              SoLuong = (int)selectedRow.Cells["SL"].Value,
              DanhMuc = (int)selectedRow.Cells["DANHMUC"].Value,
              Hsx = (int)selectedRow.Cells["HANGSANXUAT"].Value,
              NgSx = (DateTime)selectedRow.Cells["NGSX"].Value,
              TrangThai = (bool)selectedRow.Cells["TRANGTHAI"].Value
            };

            overlay.Size = Form.ActiveForm.ClientSize;
            overlay.Location = Form.ActiveForm.PointToScreen(new Point(0, 0));
            ProductDetailFormGUI detailsForm = new ProductDetailFormGUI(sanPham);

            overlay.Show(Form.ActiveForm);

            detailsForm.ShowDialog(Form.ActiveForm);

            overlay.Close();
          }
        }
      }
    }
  }
}
