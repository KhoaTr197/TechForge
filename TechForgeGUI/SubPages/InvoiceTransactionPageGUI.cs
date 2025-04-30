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
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class InvoiceTransactionPageGUI : Page
  {
    // BUS objects
    private SanPhamBUS sanPhamBUS;
    private HoiVienBUS hoiVienBUS;

    // Lists to store data
    private List<SanPhamDTO> dsSanPham;
    private List<HoiVienDTO> dsHoiVien;
    private List<SanPhamDTO> dsCTHD; // Items added to invoice
    
    public InvoiceTransactionPageGUI()
    {
      InitializeComponent();
      InitializeBUS();
      GetData();

      this.Dock = DockStyle.Fill;
      this.Font = new Font("Segoe UI", 10);
    }
    
    // Initialize BUS
    private void InitializeBUS()
    {
      sanPhamBUS = new SanPhamBUS(connStr);
      hoiVienBUS = new HoiVienBUS(connStr);
    }
    // Get data
    private void GetData()
    {
      dsSanPham = sanPhamBUS.GetAllConnected();
      dsHoiVien = hoiVienBUS.GetAllConnected();
      dsCTHD = new List<SanPhamDTO>();
    }
    private void BtnProductSearch_Click(object sender, EventArgs e)
    {
      //string searchText = txtProductSearch.Text.Trim().ToLower();
      
      //if (string.IsNullOrEmpty(searchText))
      //{
      //  return;
      //}
      
      //var filteredList = dsSanPham.Where(p => 
      //  p.TenSP.ToLower().Contains(searchText) || 
      //  p.MaSP.ToString().Contains(searchText)).ToList();
      
      //dgvProducts.DataSource = filteredList.Select(p => new 
      //{
      //  MaSP = p.MaSP,
      //  TenSP = p.TenSP,
      //  Gia = p.Gia,
      //  KhuyenMai = p.KhuyenMai,
      //  SoLuong = p.SoLuong
      //}).ToList();
    }
    private void DgvInvoiceItems_CellContentClick(object sender, EventArgs e)
    {
      //if (dgvInvoiceItems.CurrentCell.ColumnIndex != dgvInvoiceItems.Columns["Xoa"].Index)
      //{
      //  return;
      //}

      //if (MessageBox.Show("Bạn có chắc chắn xóa sản phẩm khỏi hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      //{
      //  return;
      //} 

      //int selectedIndex = dgvInvoiceItems.SelectedRows[0].Index;
      //int productId = (int)dgvInvoiceItems.Rows[selectedIndex].Cells["MaSP"].Value;
      
      //// Remove from list
      //SanPhamDTO productToRemove = dsCTHD.FirstOrDefault(p => p.MaSP == productId);
      //if (productToRemove != null)
      //{
      //  dsCTHD.Remove(productToRemove);
      //}
      
      //// Remove from grid
      //dgvInvoiceItems.Rows.RemoveAt(selectedIndex);
      
      //UpdateInvoiceSummary();
    }    
  }
}
