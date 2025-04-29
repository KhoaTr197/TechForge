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
using TechForgeGUI.SubPages;

namespace TechForgeGUI.SubForms
{
  public partial class SupplierManagePageGUI : ManagePage
  {
    private List<NhaCungCapDTO> dsNhaCungCap { get; set; }
    private NhaCungCapBUS bus { get; set; }
    private RolePermissions permissions;
    public SupplierManagePageGUI(string role)
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
        new SummaryCard("Tổng nhà cung cấp", dsNhaCungCap.Count.ToString(), "supplier_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    sealed protected override void InitializeBUS()
    {
      bus = new NhaCungCapBUS(this.connStr);
    }
    private void GetData()
    {
      // Map data to DTOs
      dsNhaCungCap = bus.GetAllConnected();
    }
    sealed protected override void LoadData()
    {
      dgvMainList.BindingData(dsNhaCungCap);
    }
    private void ModifyData()
    {
      this.SuspendLayout();

      // Add columns to DataGridView
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MANCC",
        DataPropertyName = "MaNCC",
        HeaderText = "Mã NCC",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TENNCC",
        DataPropertyName = "TenNCC",
        HeaderText = "Tên NCC",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "NDD",
        DataPropertyName = "Ndd",
        HeaderText = "Người ĐD",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "SDT",
        DataPropertyName = "Sdt",
        HeaderText = "SĐT",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "EMAIL",
        DataPropertyName = "Email",
        HeaderText = "Email",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TRANGTHAI",
        DataPropertyName = "TrangThai",
        HeaderText = "Trạng thái",
        FillWeight = 160,
      });

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
            e.Value = "Đang hợp tác";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Red;
            e.Value = "Ngừng hợp tác";
          }
        }
      }
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      SupplierDetailFormGUI DetailForm = new SupplierDetailFormGUI(permissions, bus);
      OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

      Overlay.Show(Form.ActiveForm);
      DetailForm.Show(Form.ActiveForm);

      DetailForm.AddSubmit += DetailsForm_AddSubmit;
    }
    private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        DataGridView dgvMainList = (DataGridView)sender;

        if (dgvMainList.SelectedRows.Count > 0)
        {
          DataGridViewRow selectedRow = dgvMainList.SelectedRows[0];
          NhaCungCapDTO selectedNcc = dsNhaCungCap.Find(ncc => ncc.MaNCC == (int)selectedRow.Cells[0].Value);

          SupplierDetailFormGUI DetailForm = new SupplierDetailFormGUI(permissions, bus, selectedNcc);
          OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

          Overlay.Show(Form.ActiveForm);
          DetailForm.Show(Form.ActiveForm);

          // Assign event handler for submits
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

      // Update summary cards when new categories are added
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng nhà cung cấp", dsNhaCungCap.Count.ToString(), "supplier_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when categories are edited
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng nhà cung cấp", dsNhaCungCap.Count.ToString(), "supplier_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when categories are edited
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng nhà cung cấp", dsNhaCungCap.Count.ToString(), "supplier_icon", Color.FromArgb(52, 152, 219)),
      });
    }
  }
}