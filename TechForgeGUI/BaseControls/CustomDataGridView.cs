using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TechForgeDTO;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.BaseControls
{
  public partial class CustomDataGridView : UserControl
  {
    //Properties
    private int CurrentPage;
    private int ItemPerPage;
    private int TotalPages;
    private int TotalItems;
    private object OriginalDataSrc;
    public CustomDataGridView()
    {
      InitializeComponent();

      CurrentPage = 1;
      ItemPerPage = 5;
      TotalItems = 0;
      TotalPages = 0;

      dgvList.RowPrePaint += dgvList_RowPrePaint;

      //Assign btnPrev, btnNext event handlers
      btnPrev.Click += PrevButton_Click;
      btnNext.Click += NextButton_Click;

      //Add these controls to this CustomDataGridView
      this.Controls.Add(dgvList);
    }
    //Event Handlers when Dock changed
    private void CustomDataGridView_DockChanged(object sender, EventArgs e)
    {
      dgvList.Size = Size = new Size(this.Width, this.Height - flpPagination.Height);
      flpPagination.Size = new Size(this.Width, flpPagination.Height);
    }
    public void Binding<T>(List<T> dataList, int _ItemPerPage = 5) where T : class
    {
      this.OriginalDataSrc = dataList.ToList();

      dgvList.DataSource = OriginalDataSrc;

      SetUpPagination(dataList.Count, ItemPerPage);

      PaginateData(dataList);
    }
    public void Binding(DataTable dataTable, int _ItemPerPage = 5)
    {
      this.OriginalDataSrc = dataTable.Copy();

      dgvList.DataSource = OriginalDataSrc;

      SetUpPagination(dataTable.Rows.Count, _ItemPerPage);

      PaginateData(dataTable);
    }
    public void Binding(DataView dataView, int _ItemPerPage=5)
    {
      this.OriginalDataSrc = dataView.ToTable().Copy();

      dgvList.DataSource = OriginalDataSrc;

      SetUpPagination(dataView.Count, _ItemPerPage);

      PaginateData(dataView);
    }
    //Event Handlers when PrevButton and NextButton clicked
    private void PrevButton_Click(object sender, EventArgs e)
    {
      if (CurrentPage > 1)
      {
        CurrentPage--;

        if (OriginalDataSrc is List<SanPhamDTO> list1)
        {
          PaginateData(list1);
        }
        else if (OriginalDataSrc is List<HangSanXuatDTO> list2)
        {
          PaginateData(list2);
        }
        else if (OriginalDataSrc is List<NhaCungCapDTO> list3)
        {
          PaginateData(list3);
        }
        else if (OriginalDataSrc is List<DanhMucDTO> list4)
        {
          PaginateData(list4);
        }
        else if (OriginalDataSrc is List<HoiVienDTO> list5)
        {
          PaginateData(list5);
        }
        else if (OriginalDataSrc is List<LichSuKhoDTO> list6)
        {
          PaginateData(list6);
        }
        else if (OriginalDataSrc is List<HoaDonDTO> list7)
        {
          PaginateData(list7);
        }
        else if (OriginalDataSrc is List<NguoiDungDTO> list8)
        {
          PaginateData(list8);
        }
        else if (OriginalDataSrc is List<TaiKhoanDTO> list9)
        {
          PaginateData(list9);
        }
        else if (OriginalDataSrc is DataTable table)
        {
          PaginateData(table);
        }
        else if (OriginalDataSrc is DataView view)
        {
          PaginateData(view);
        }
      }
    }
    private void NextButton_Click(object sender, EventArgs e)
    {
      if (CurrentPage < TotalPages)
      {
        CurrentPage++;

        if (OriginalDataSrc is List<SanPhamDTO> list1)
        {
          PaginateData(list1);
        }
        else if (OriginalDataSrc is List<HangSanXuatDTO> list2)
        {
          PaginateData(list2);
        }
        else if (OriginalDataSrc is List<NhaCungCapDTO> list3)
        {
          PaginateData(list3);
        }
        else if (OriginalDataSrc is List<DanhMucDTO> list4)
        {
          PaginateData(list4);
        }
        else if (OriginalDataSrc is List<HoiVienDTO> list5)
        {
          PaginateData(list5);
        }
        else if (OriginalDataSrc is List<LichSuKhoDTO> list6)
        {
          PaginateData(list6);
        }
        else if (OriginalDataSrc is List<HoaDonDTO> list7)
        {
          PaginateData(list7);
        }
        else if (OriginalDataSrc is List<NguoiDungDTO> list8)
        {
          PaginateData(list8);
        }
        else if (OriginalDataSrc is List<TaiKhoanDTO> list9)
        {
          PaginateData(list9);
        }
        else if (OriginalDataSrc is DataTable table)
        {
          PaginateData(table);
        }
        else if(OriginalDataSrc is DataView view)
        {
          PaginateData(view);
        }
      }
    }
    //Set up Pagination Properties
    public void SetUpPagination(int _totalItems, int _itemPerPage = 5)
    {
      ItemPerPage = _itemPerPage;
      TotalPages = (int)Math.Ceiling((double)_totalItems / _itemPerPage);
      CurrentPage = 1;
    }
    private void PaginateData<T>(List<T> dataList) where T : class
    {
      if (dataList == null || dataList.Count == 0)
      {
        dgvList.DataSource = null;
        lblCurentPage.Text = "No data available";
        return;
      }

      int startIndex = (CurrentPage - 1) * ItemPerPage;
      if (dataList != null)
      {
        List<T> newDataList = dataList.Skip(startIndex).Take(ItemPerPage).ToList();
        this.dgvList.DataSource = newDataList;
      }
      lblCurentPage.Text = $"Page {CurrentPage} of {TotalPages}";
    }
    private void PaginateData(DataTable table)
    {
      if (table == null || table.Rows.Count == 0)
      {
        dgvList.DataSource = null;
        lblCurentPage.Text = "No data available";
        return;
      }

      int startIndex = (CurrentPage - 1) * ItemPerPage;
      if (table != null)
      {
        DataTable newDataTable = table.Clone();
        var rows = table.AsEnumerable().Skip(startIndex).Take(ItemPerPage);

        foreach (var row in rows)
        {
          newDataTable.ImportRow(row);
        }

        this.dgvList.DataSource = newDataTable;
      }
      lblCurentPage.Text = $"Page {CurrentPage} of {TotalPages}";
    }
    private void PaginateData(DataView view)
    {
      DataTable table = view.ToTable();
      if (table == null || table.Rows.Count == 0)
      {
        dgvList.DataSource = null;
        lblCurentPage.Text = "No data available";
        return;
      }

      int startIndex = (CurrentPage - 1) * ItemPerPage;
      if (table != null)
      {
        DataTable newDataTable = table.Clone();
        var rows = table.AsEnumerable().Skip(startIndex).Take(ItemPerPage);

        foreach (var row in rows)
        {
          newDataTable.ImportRow(row);
        }

        this.dgvList.DataSource = newDataTable;
      }
      lblCurentPage.Text = $"Page {CurrentPage} of {TotalPages}";
    }
    //Event Handlers when Row Pre Paint
    private void dgvList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
      int rowIndex = e.RowIndex;
      var row = dgvList.Rows[rowIndex];

      if ((rowIndex & 1) == 0) //bitwise AND operator out performs % operator in large data scenarios
      {
        row.DefaultCellStyle.BackColor = Color.LightGray;
      }
      else
      {
        row.DefaultCellStyle.BackColor = Color.White;
      }
    }
  }
}
