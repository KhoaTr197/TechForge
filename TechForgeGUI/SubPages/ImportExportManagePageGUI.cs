using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
  public partial class ImportExportManagePageGUI : ManagePage
  {
    private List<LichSuKhoDTO> dsLichSuKho { get; set; }
    private LichSuKhoBUS bus { get; set; }
    private RolePermissions permissions;
    public ImportExportManagePageGUI(string role)
    {
      InitializeComponent();

      // Initialize permissions
      permissions = RolePermissions.GetPermissions(role);

      InitializeBUS();
      GetData();
      LoadData();
      ModifyData();
      SetUpFeature();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;

      btnAdd.Click += BtnAdd_Click;
    }
    private void SetUpFeature()
    {
      summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng phiếu nhập", dsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Tổng phiếu xuất", dsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
        });
    }
    protected void GetData()
    {
      dsLichSuKho = bus.GetAllConnected();
    }
    protected override void LoadData()
    {
      dgvMainList.BindingData(dsLichSuKho);
    }
    // Modify DataGridView columns
    private void ModifyData()
    {
      this.SuspendLayout();

      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MaLS",
        DataPropertyName = "MaLS",
        HeaderText = "Mã Lịch Sử",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TongTien",
        DataPropertyName = "TongTien",
        HeaderText = "Tổng Tiền",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "ThoiGian",
        DataPropertyName = "ThoiGian",
        HeaderText = "Thời Gian",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MaND",
        DataPropertyName = "MaND",
        HeaderText = "Mã Người Dùng",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "HoatDong",
        DataPropertyName = "HoatDong",
        HeaderText = "Hoạt Động",
        FillWeight = 48,
      });

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "HoatDong")
        {
          bool status = (bool)e.Value;
          if (status)
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
            e.Value = "Xuất";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Green;
            e.Value = "Nhập";
          }
        }
        else if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TongTien")
        {
          decimal price = (decimal)e.Value;
          e.Value = price.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
          e.FormattingApplied = true;
        }
      }
    }
    sealed protected override void InitializeBUS()
    {
      bus = new LichSuKhoBUS(this.connStr);
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      SanPhamBUS busSanPham = new SanPhamBUS(this.connStr);

      ImportExportDetailFormGUI detailsForm = new ImportExportDetailFormGUI(bus, busSanPham);

      detailsForm.Show();

      detailsForm.AddSubmit += DetailsForm_AddSubmit;
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
          LichSuKhoDTO lichSuKho = dsLichSuKho.Find(hv => hv.MaLS == (int)selectedRow.Cells[0].Value);
          lichSuKho.Ctlsk = bus.GetDetail(lichSuKho.MaLS);
          SanPhamBUS busSanPham = new SanPhamBUS(this.connStr);

          ImportExportDetailFormGUI detailsForm = new ImportExportDetailFormGUI(bus, busSanPham, lichSuKho);

          detailsForm.Show(Form.ActiveForm);

          //// Assign event handler for submits
          detailsForm.AddSubmit += DetailsForm_AddSubmit;
          detailsForm.EditSubmit += DetailsForm_EditSubmit;
          detailsForm.DeleteSubmit += DetailsForm_DeleteSubmit;
        }
      }
    }
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng phiếu nhập", dsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Tổng phiếu xuất", dsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
      });
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng phiếu nhập", dsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Tổng phiếu xuất", dsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng phiếu nhập", dsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Tổng phiếu xuất", dsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
      });
    }
  }
}
