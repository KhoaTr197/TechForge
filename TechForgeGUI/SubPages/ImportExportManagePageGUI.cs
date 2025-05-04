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
    private List<LichSuKhoDTO> DsLichSuKho { get; set; }
    private LichSuKhoBUS bus { get; set; }
    private NguoiDungBUS BusNguoiDung { get; set; }
    private List<NguoiDungDTO> DsNhanVienKho { get; set; }
    private RolePermissions permissions;
        private NguoiDungDTO currentUser {  get; set; }
    public ImportExportManagePageGUI(string role, NguoiDungDTO _currentUser)
    {
      InitializeComponent();

      // Initialize permissions
      permissions = RolePermissions.GetPermissions(role);
      currentUser = _currentUser;
      InitializeBUS();
      GetData();
      AddColumns();
      LoadData();
      SetUpFeature();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;

      btnAdd.Click += BtnAdd_Click;

      btnSearch.Click += btnSearch_Click;
    }
    private void SetUpFeature()
    {
      summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng phiếu nhập", DsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Tổng phiếu xuất", DsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
        });
    }
    private void GetData()
    {
      DsLichSuKho = bus.GetAllConnected().OrderByDescending(lsk => lsk.ThoiGian).ToList();
      DsNhanVienKho = BusNguoiDung.GetAllConnected().Select(nv => nv).Where(nv => nv.VaiTro == "Quản Lý Kho").ToList();
    }
    private void LoadData()
    {
      dgvMainList.Binding(DsLichSuKho);
    }
    // Add DataGridView columns
    private void AddColumns()
    {
      this.SuspendLayout();

      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MaLS",
        DataPropertyName = "MaLS",
        HeaderText = "Mã",
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
    private void InitializeBUS()
    {
      bus = new LichSuKhoBUS(this.connStr);
      BusNguoiDung = new NguoiDungBUS(this.connStr);
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      SanPhamBUS sanPhamBus = new SanPhamBUS(this.connStr);

      ImportExportDetailFormGUI DetailForm = new ImportExportDetailFormGUI(bus, sanPhamBus, currentUser, null);
      OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

      Overlay.Show(Form.ActiveForm);
      DetailForm.Show(Form.ActiveForm);

      DetailForm.AddSubmit += DetailsForm_AddSubmit;
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
          LichSuKhoDTO lichSuKho = DsLichSuKho.Find(hv => hv.MaLS == (int)selectedRow.Cells[0].Value);
          lichSuKho.Ctlsk = bus.GetDetail(lichSuKho.MaLS);
          SanPhamBUS sanPhamBus = new SanPhamBUS(this.connStr);

          ImportExportDetailFormGUI DetailForm = new ImportExportDetailFormGUI(bus, sanPhamBus, currentUser, lichSuKho);
          OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

          Overlay.Show(Form.ActiveForm);
          DetailForm.Show(Form.ActiveForm);

          //// Assign event handler for submits
          DetailForm.AddSubmit += DetailsForm_AddSubmit;
          DetailForm.EditSubmit += DetailsForm_EditSubmit;
          DetailForm.DeleteSubmit += DetailsForm_DeleteSubmit;
        }
      }
    }
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng phiếu nhập", DsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Tổng phiếu xuất", DsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
      });
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng phiếu nhập", DsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Tổng phiếu xuất", DsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng phiếu nhập", DsLichSuKho.Where(p => !p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Tổng phiếu xuất", DsLichSuKho.Where(p => p.HoatDong).Count().ToString(), "box_icon", Color.FromArgb(231, 76, 60)),
      });
    }
    private void btnSearch_Click(object sender, EventArgs e)
    {
      List<LichSuKhoDTO> newDsLichSuKho = bus.FindByAnyProperty(txtSearch.Text.Trim().ToLower());
      if (newDsLichSuKho.Count == 0)
      {
        MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      DsLichSuKho = newDsLichSuKho;

      LoadData();
    }
  }
}
